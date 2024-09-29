using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;

public class BigBettySkill : MonoBehaviour
{
    [Inject(Id = "BigBettyPrefab")] private GameObject bigBettyPrefab;
    [Inject] public DiContainer container;

    const string ACTIVE_TRIGGER = "Active";
    const string COOLDOWN_TRIGGER = "Cooldown";
    const string OFF_COOLDOWN_TRIGGER = "Off Cooldown";

    [Inject] EventsManager eventsManager;
    [Inject] public IPlayerSkillsViewModel playerSkillViewModel;

    public GameObject bigBettySprite;
    public Button bigBettyButton;
    Animator animator;
    public GameObject spawnPoint;

    void OnEnable() 
    {
        eventsManager.StartListening(GameEvent.PlayerSkillViewModelEvent.PLAYER_SKILL_VM_SETUP_COMPLETE, UpdateUI);
    }

    private void UpdateUI()
    {
        var bigBetty = playerSkillViewModel.bigBetty.CurrentValue;

        playerSkillViewModel.bigBetty.Subscribe(bigBetty => 
        {
            if(bigBetty.isUnlocked)
            {
                bigBettySprite.SetActive(true);
                bigBettyButton.gameObject.SetActive(true);
            }
        });

        bigBettyButton.onClick.AddListener(() => OnSkillClicked(bigBetty.coolDown));
    }

    void OnSkillClicked(int cooldownTimer)
    {
        bigBettyButton.gameObject.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger(ACTIVE_TRIGGER);
        container.InstantiatePrefab(bigBettyPrefab, spawnPoint.transform.position, Quaternion.identity, null);
        StartCoroutine(Cooldown(cooldownTimer));
    }

    IEnumerator Cooldown(int cooldownTimer)
    {
        yield return new WaitForSeconds(3);
        animator.SetTrigger(COOLDOWN_TRIGGER);
        bigBettyButton.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        eventsManager.StopListening(GameEvent.PlayerSkillViewModelEvent.PLAYER_SKILL_VM_SETUP_COMPLETE, UpdateUI);
    }
}
