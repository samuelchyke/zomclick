using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;

public class MidasRoundsSkill : MonoBehaviour
{
    const string ACTIVE_TRIGGER = "Active";
    const string COOLDOWN_TRIGGER = "Cooldown";
    const string OFF_COOLDOWN_TRIGGER = "Off Cooldown";

    [Inject] EventsManager eventsManager;
    [Inject] public IPlayerSkillsViewModel playerSkillsViewModel; 

    public GameObject midasShotSprite;
    public Button midasRoundsButton;
    Animator animator;

    void OnEnable() 
    {
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UNLOCK_PLAYER_SKILL, UnlockSkill);
    }

    private void UnlockSkill(string playerSkillId)
    {
        var midasRounds = playerSkillsViewModel.midasRounds.CurrentValue;
        if(midasRounds.id == playerSkillId)
        {
            midasShotSprite.SetActive(true);
            midasRoundsButton.gameObject.SetActive(true);
            midasRoundsButton.onClick.AddListener(() => OnSkillClicked(midasRounds.coolDown));
        }
    }

    private void UpdateUI()
    {
        playerSkillsViewModel.midasRounds.Subscribe(midasRounds => 
        {
            if(midasRounds.isUnlocked && midasRounds.isActive == false)
            {
                midasShotSprite.SetActive(true);
                midasRoundsButton.gameObject.SetActive(true);
            }
        });

        midasRoundsButton.onClick.AddListener(() => 
            OnSkillClicked(playerSkillsViewModel.midasRounds.CurrentValue.coolDown)
        );
    }

    void OnSkillClicked(int cooldownTimer)
    {
        midasRoundsButton.gameObject.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger(ACTIVE_TRIGGER);
        StartCoroutine(ActivateSkill(cooldownTimer));
    }

    IEnumerator ActivateSkill(int cooldownTimer)
    {
        playerSkillsViewModel.ToggleIsSkillActive(Skill.MidasRounds.id());
        yield return new WaitForSeconds(30);
        animator.SetTrigger(COOLDOWN_TRIGGER);
        playerSkillsViewModel.ToggleIsSkillActive(Skill.MidasRounds.id());
        StartCoroutine(OffCoolDownTimer(cooldownTimer));
    }

    IEnumerator OffCoolDownTimer(int coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        animator.SetTrigger(OFF_COOLDOWN_TRIGGER);
        midasRoundsButton.gameObject.SetActive(true);
    }

    // IEnumerator Cooldown()
    // {
    //     yield return new WaitForSeconds(3);
    //     playerShopViewModel.ToggleIsSkillActive("midas_rounds_id");
    //     animator.SetTrigger(COOLDOWN_TRIGGER);
    // }

    void OnDisable()
    {
        eventsManager.StopListening(GameEvent.PlayerShopViewModelEvent.UNLOCK_PLAYER_SKILL, UnlockSkill);
    }
}
