using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TurretSkill : MonoBehaviour
{
    [Inject(Id = "TurretPrefab")] readonly GameObject turretPrefab;
    [Inject] public DiContainer container;

    const string ACTIVE_TRIGGER = "Active";
    const string COOLDOWN_TRIGGER = "Cooldown";
    const string OFF_COOLDOWN_TRIGGER = "Off Cooldown";

    [Inject] EventsManager eventsManager;
    [Inject] public IPlayerShopViewModel playerShopViewModel; 

    public GameObject turretSprite;
    public Button turretButton;
    Animator animator;

    public GameObject turretSpawn;

    void OnEnable() 
    {
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, UpdateUI);
    }

    private void UpdateUI()
    {
        var skill = playerShopViewModel.playerSkills.CurrentValue.Find(skill => skill.id == "turret_id");

        playerShopViewModel.playerSkills.Subscribe(skills => 
        {
            var skill = skills.Find(skill => skill.id == "turret_id");  
            if(skill.isUnlocked)
            {
                turretSprite.SetActive(true);
                turretButton.gameObject.SetActive(true);

            }
        });

        turretButton.onClick.AddListener(() => OnSkillClicked(skill.coolDown));
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
