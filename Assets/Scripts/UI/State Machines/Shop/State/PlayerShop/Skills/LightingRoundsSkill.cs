using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;
using Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.State.PlayerShop.Skills {
    public class LightingRoundsSkill : MonoBehaviour
    {
        const string ACTIVE_TRIGGER = "Active";
        const string COOLDOWN_TRIGGER = "Cooldown";
        const string OFF_COOLDOWN_TRIGGER = "Off Cooldown";

        [Inject] EventsManager eventsManager;
        [Inject] public IPlayerShopViewModel playerShopViewModel; 
        [Inject] public IPlayerSkillsViewModel playerSkillsViewModel; 


        public GameObject lightingRoundsSprite;
        public Button lightingRoundsButton;
        Animator animator;
        PlayerSkill lightningRounds;

        void OnEnable() 
        {
            eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UNLOCK_PLAYER_SKILL, UnlockSkill);
        }

        private void UnlockSkill(string playerSkillId)
        {
            var lightningRounds = playerSkillsViewModel.lightningRounds.CurrentValue;
            if (lightningRounds.id == playerSkillId)
            {
                lightingRoundsSprite.SetActive(true);
                lightingRoundsButton.gameObject.SetActive(true);
                lightingRoundsButton.onClick.AddListener(() => OnSkillClicked(lightningRounds.coolDown));
            }
        }

        private void UpdateUI()
        {
            playerSkillsViewModel.lightningRounds.Subscribe(lightningRounds => 
            {
                if(lightningRounds.isUnlocked && lightningRounds.isActive == false)
                {
                    lightingRoundsSprite.SetActive(true);
                    lightingRoundsButton.gameObject.SetActive(true);
                }
            });

            lightingRoundsButton.onClick.AddListener(() => 
                OnSkillClicked(playerSkillsViewModel.lightningRounds.CurrentValue.coolDown)
            );
        }

        void OnSkillClicked (int cooldownTimer)
        {
            lightingRoundsButton.gameObject.SetActive(false);
            animator = GetComponentInChildren<Animator>();
            animator.SetTrigger(ACTIVE_TRIGGER);
            playerSkillsViewModel.ToggleIsSkillActive(Skill.LightningRounds.id());
            StartCoroutine(ActivateSkill(cooldownTimer));
        }

        IEnumerator Cooldown(int cooldownTimer)
        {
            yield return new WaitForSeconds(10);
            animator.SetTrigger(COOLDOWN_TRIGGER);
            playerSkillsViewModel.ToggleIsSkillActive(Skill.LightningRounds.id());
            lightingRoundsButton.gameObject.SetActive(true);
        }

        IEnumerator ActivateSkill(int cooldownTimer)
        {
            playerSkillsViewModel.ToggleIsSkillActive(Skill.LightningRounds.id());
            yield return new WaitForSeconds(30);
            animator.SetTrigger(COOLDOWN_TRIGGER);
            playerSkillsViewModel.ToggleIsSkillActive(Skill.LightningRounds.id());
            StartCoroutine(OffCoolDownTimer(cooldownTimer));
        }

        IEnumerator OffCoolDownTimer(int coolDown)
        {
            yield return new WaitForSeconds(coolDown);
            animator.SetTrigger(OFF_COOLDOWN_TRIGGER);
            lightingRoundsButton.gameObject.SetActive(true);
        }

        void OnDisable()
        {
            eventsManager.StopListening(GameEvent.PlayerShopViewModelEvent.UNLOCK_PLAYER_SKILL, UnlockSkill);
        }
    }
}