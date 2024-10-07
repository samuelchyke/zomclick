using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;

public class RallyAlliesSkill : MonoBehaviour
{
    const string ACTIVE_TRIGGER = "Active";
    const string COOLDOWN_TRIGGER = "Cooldown";
    const string OFF_COOLDOWN_TRIGGER = "Off Cooldown";

    [Inject] EventsManager eventsManager;
    [Inject] public IPlayerSkillsViewModel playerSkillsViewModel; 
    PlayerSkill rallyAllies;

    public GameObject rallyAlliesSprite;
    public Button rallyAlliesButton;
    Animator animator;

    void OnEnable() 
    {
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UNLOCK_PLAYER_SKILL, UnlockSkill);
    }

    private void UnlockSkill(string playerSkillId)
    {
        var rallyAllies = playerSkillsViewModel.rallyAllies.CurrentValue;
        if(rallyAllies.id == playerSkillId)
        {
            rallyAlliesSprite.SetActive(true);
            rallyAlliesButton.gameObject.SetActive(true);
            rallyAlliesButton.onClick.AddListener(() => OnSkillClicked(rallyAllies.coolDown));
        }
    }

    private void UpdateUI()
    {
        playerSkillsViewModel.rallyAllies.Subscribe(rallyAllies => 
        {
            if(rallyAllies.isUnlocked)
            {
                rallyAlliesSprite.SetActive(true);
                rallyAlliesButton.gameObject.SetActive(true);
            }
        });

        rallyAlliesButton.onClick.AddListener(() => 
            OnSkillClicked(playerSkillsViewModel.rallyAllies.CurrentValue.coolDown)
        );
    }

    void OnSkillClicked(int cooldownTimer)
    {
        rallyAlliesButton.gameObject.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger(ACTIVE_TRIGGER);
        playerSkillsViewModel.ToggleIsSkillActive("rally_allies_id");
        StartCoroutine(CooldownTimer(cooldownTimer));
    }

    IEnumerator CooldownTimer(int cooldownTimer)
    {
        yield return new WaitForSeconds(30);
        animator.SetTrigger(COOLDOWN_TRIGGER);
        playerSkillsViewModel.ToggleIsSkillActive("rally_allies_id");
        StartCoroutine(OffCoolDownTimer(cooldownTimer));
    }

    IEnumerator OffCoolDownTimer(int coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        animator.SetTrigger(OFF_COOLDOWN_TRIGGER);
        rallyAlliesButton.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        eventsManager.StopListening(GameEvent.PlayerShopViewModelEvent.UNLOCK_PLAYER_SKILL, UnlockSkill);
    }
}
