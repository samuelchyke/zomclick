using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;
// using Unity.VisualScripting;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;
using Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Player.Skills;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.State.PlayerShop.Skills {
    public class TurretSkill : MonoBehaviour
    {
        [Inject(Id = "TurretPrefab")] readonly GameObject turretPrefab;
        [Inject] public DiContainer container;

        const string ACTIVE_TRIGGER = "Active";
        const string COOLDOWN_TRIGGER = "Cooldown";
        const string OFF_COOLDOWN_TRIGGER = "Off Cooldown";

        [Inject] EventsManager eventsManager;
        [Inject] public IPlayerSkillsViewModel playerSkillsViewModel; 
        PlayerSkill turret;

        public GameObject turretSprite;
        public Button turretButton;
        Animator animator;

        public GameObject turretSpawn;

        void OnEnable() 
        {
            eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UNLOCK_PLAYER_SKILL, UnlockSkill);
        }

        private void UnlockSkill(string playerSkillId)
        {
            turret = playerSkillsViewModel.turret.CurrentValue;
            if(turret.id == playerSkillId) 
            {
                turretSprite.SetActive(true);
                turretButton.gameObject.SetActive(true);
                turretButton.onClick.AddListener(() => OnSkillClicked(turret.coolDown));
            }
        }

        private void UpdateUI()
        {
            turret = playerSkillsViewModel.turret.CurrentValue;

            playerSkillsViewModel.turret.Subscribe(turret => 
            {
                if(turret.isUnlocked)
                {
                    turretSprite.SetActive(true);
                    turretButton.gameObject.SetActive(true);
                }
            });

            turretButton.onClick.AddListener(() => OnSkillClicked(turret.coolDown));
        }

        void OnSkillClicked(int coolDown)
        {
            animator = GetComponentInChildren<Animator>();
            animator.SetTrigger(ACTIVE_TRIGGER);
            var turret = container.InstantiatePrefab(turretPrefab, turretSpawn.transform.position, Quaternion.identity, null).GetComponent<Turret>();
            StartCoroutine(Cooldown(turret, coolDown));
        }

        IEnumerator Cooldown(Turret turret, int coolDown)
        {
            yield return new WaitForSeconds(30);
            animator.SetTrigger(COOLDOWN_TRIGGER);
            turretButton.gameObject.SetActive(false);
            Destroy(turret.gameObject);
            StartCoroutine(Cooldown2(coolDown));
        }

        IEnumerator Cooldown2(int coolDown)
        {
            yield return new WaitForSeconds(coolDown);
            animator.SetTrigger(OFF_COOLDOWN_TRIGGER);
            turretButton.gameObject.SetActive(true);
        }

        void OnDisable()
        {
            eventsManager.StopListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, UpdateUI);
        }
    }
}