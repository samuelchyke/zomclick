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
    [Inject] public IPlayerSkillsViewModel playerSkillsViewModel; 

    public GameObject incendiaryRoundsSprite;
    public Button incendiaryRoundsButton;
    Animator animator;

    void OnEnable() 
    {
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UNLOCK_PLAYER_SKILL, UnlockSkill);
    }


    private void UnlockSkill(string playerSkillId)
    {
        var incendiaryRounds = playerSkillsViewModel.incendiaryRounds.CurrentValue;
        Debug.Log("incendiaryRounds.id: " + incendiaryRounds.id);
        Debug.Log("playerSkillId: " + playerSkillId);
        // incendiaryRounds.id == playerSkillId;
        if(incendiaryRounds.id == playerSkillId)
        {
            incendiaryRoundsSprite.SetActive(true);
            incendiaryRoundsButton.gameObject.SetActive(true);
            incendiaryRoundsButton.onClick.AddListener(() => OnSkillClicked(incendiaryRounds.coolDown));
        }
    }

    private void UpdateUI()
    {
        playerSkillsViewModel.incendiaryRounds.Subscribe(incendiaryRounds => 
        {
            if(incendiaryRounds.isUnlocked && incendiaryRounds.isActive == false)
            {
                incendiaryRoundsSprite.SetActive(true);
                incendiaryRoundsButton.gameObject.SetActive(true);
            }
        });

        incendiaryRoundsButton.onClick.AddListener(() => 
            OnSkillClicked(playerSkillsViewModel.incendiaryRounds.CurrentValue.coolDown)
        );
    }

    void OnSkillClicked(int cooldownTimer)
    {
        incendiaryRoundsButton.gameObject.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger(ACTIVE_TRIGGER);
        playerSkillsViewModel.ToggleIsSkillActive("incendiary_rounds_id");
        StartCoroutine(ActivateSkill(cooldownTimer));
    }

    IEnumerator Cooldown()
    {
        incendiaryRoundsButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(10);
        animator.SetTrigger(COOLDOWN_TRIGGER);
        playerSkillsViewModel.ToggleIsSkillActive("incendiary_rounds_id");
    }

    IEnumerator ActivateSkill(int cooldownTimer)
    {
        playerSkillsViewModel.ToggleIsSkillActive("incendiary_rounds_id");
        yield return new WaitForSeconds(30);
        animator.SetTrigger(COOLDOWN_TRIGGER);
        playerSkillsViewModel.ToggleIsSkillActive("incendiary_rounds_id");
        StartCoroutine(OffCoolDownTimer(cooldownTimer));
    }

    IEnumerator OffCoolDownTimer(int coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        animator.SetTrigger(OFF_COOLDOWN_TRIGGER);
        incendiaryRoundsButton.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        eventsManager.StopListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, UnlockSkill);
    }
}
