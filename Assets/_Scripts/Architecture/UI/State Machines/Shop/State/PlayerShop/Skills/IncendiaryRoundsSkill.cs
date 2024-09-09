using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;

public class IncendiaryRoundsSkill : MonoBehaviour
{
    const string ACTIVE_TRIGGER = "Active";
    const string COOLDOWN_TRIGGER = "Cooldown";
    const string OFF_COOLDOWN_TRIGGER = "Off Cooldown";

    [Inject] EventsManager eventsManager;
    [Inject] public IPlayerShopViewModel playerShopViewModel; 

    public GameObject incendiaryRoundsSprite;
    public Button incendiaryRoundsButton;
    Animator animator;

    void OnEnable() 
    {
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, UpdateUI);
    }

    private void UpdateUI()
    {
        playerShopViewModel.playerSkills.Subscribe(skills => 
        {
            if(skills.Find(skill => skill.id == "incendiary_rounds_id").isUnlocked)
            {
                incendiaryRoundsSprite.SetActive(true);
                incendiaryRoundsButton.gameObject.SetActive(true);
            }
        });

        incendiaryRoundsButton.onClick.AddListener(() => OnSkillClicked());
    }

    void OnSkillClicked ()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger(ACTIVE_TRIGGER);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3);
        animator.SetTrigger(COOLDOWN_TRIGGER);
    }

    void OnDisable()
    {
        eventsManager.StopListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, UpdateUI);
    }
}
