using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;

public class BigBettySkill : MonoBehaviour
{
    const string ACTIVE_TRIGGER = "Active";
    const string COOLDOWN_TRIGGER = "Cooldown";
    const string OFF_COOLDOWN_TRIGGER = "Off Cooldown";

    [Inject] EventsManager eventsManager;
    [Inject] public IPlayerShopViewModel playerShopViewModel; 

    public GameObject bigBettySprite;
    public Button bigBettyButton;
    Animator animator;

    void OnEnable() 
    {
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, UpdateUI);
    }

    private void UpdateUI()
    {
        playerShopViewModel.playerSkills.Subscribe(skills => 
        {
            if(skills.Find(skill => skill.id == "big_betty_id").isUnlocked)
            {
                bigBettySprite.SetActive(true);
                bigBettyButton.gameObject.SetActive(true);
            }
        });

        bigBettyButton.onClick.AddListener(() => OnSkillClicked());
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
