using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;

public class BigBetty : MonoBehaviour
{
    [Inject] EventsManager eventsManager;

    [Inject] public IPlayerShopViewModel playerShopViewModel; 

    public GameObject bigBettySprite;
    public GameObject bigBettyButton;

    // Start is called before the first frame update
    void Start()
    {
        // playerShopViewModel.playerSkills.Subscribe(allies => UpdateUI(allies));
    }

    void OnEnable() 
    {
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, UpdateUI);
        // eventsManager.StartListening(GameEvent.ShopViewModelEvent.UPDATE_SHOP_DETAILS, SetTexts);
    }

    private void UpdateUI(
        // List<PlayerSkill> skills
        )
    {
        // var skills = playerShopViewModel.playerSkills.CurrentValue;

        // foreach (var skill in skills){
        //     Debug.Log(skill.id);
        // }
        // var gg = playerShopViewModel.playerSkills.CurrentValue.FindIndex(skill => skill.id == "big_betty_id");
        // var bbIndex = skills.Find(skill => skill.id == "big_betty_id");
        // // var skill = skills[gg];
        playerShopViewModel.playerSkills.Subscribe(skills => 
        {
            // var bbIndex = skills.Find(skill => skill.id == "big_betty_id")

            if(skills.Find(skill => skill.id == "big_betty_id").isUnlocked)
            {
                bigBettySprite.SetActive(true);
                bigBettyButton.SetActive(true);
                // gameObject.GetComponentInChildren<Button>().enabled = true;
            }
        });

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
