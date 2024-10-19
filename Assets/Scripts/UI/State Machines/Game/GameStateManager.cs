using UnityEngine;
using Zenject;
using TMPro;
using System.Collections;
using Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Game {
    public class GameStateManager : MonoBehaviour
    {
        public GameObject nextRoundScreen;
        public GameObject gameOverScreen;
        public TextMeshProUGUI roundText;

        [Inject] EventsManager eventsManager;
        [Inject] IGameViewModel gameViewModel;
        EnemyWaveDetails enemyWaveDetails;

        public int round = 1;
        float timeElapsed = 0;
        [SerializeField] int nextRoundScreenDisplayTime = 3;
        public const float ROUND_TEXT_TIMER = 2;

        bool incrementRound;
        
        void Awake() 
        {
            DontDestroyOnLoad(gameObject);
        }

        void OnEnable()
        {
            eventsManager.StartListening(GameEvent.GameViewModelEvent.GAME_OVER, DisplayGameOverScreen);
            eventsManager.StartListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, NextRound);
        }

        void SetRoundText(){
            enemyWaveDetails = gameViewModel.enemyWaveDetails;

            roundText.text = "Round " + enemyWaveDetails.round.ToString();
        }

        void Start()
        {
            enemyWaveDetails = gameViewModel.enemyWaveDetails;
                // roundText = GameObject.Find("round_text").GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            // if (!incrementRound) return;
        
            // if (timeElapsed < nextRoundScreenDisplayTime)
            // {
            //     timeElapsed += Time.deltaTime;
            // }
            // else
            // {
            //     timeElapsed = 0;
            //     incrementRound = false;
            //     nextRoundScreen.SetActive(false);
            // }
        }

        public void NextWave ()
        {
            incrementRound = true;
        }

        public void NextRound()
        {
            // nextRoundScreen.SetActive(true);
            // roundText = GameObject.Find("round_text").GetComponent<TextMeshProUGUI>();
            // SetRoundText();
            // incrementRound = true;
            nextRoundScreen.SetActive(true);
            SetRoundText();
            StartCoroutine(DisplayNextRoundScreen());
        }

        IEnumerator DisplayNextRoundScreen()
        {
            yield return new WaitForSeconds(3);
            nextRoundScreen.SetActive(false);
        }

        public void DisplayGameOverScreen()
        {
            gameOverScreen.SetActive(true);
        }

        void OnDisable()
        {
            eventsManager.StopListening(GameEvent.GameViewModelEvent.GAME_OVER, DisplayGameOverScreen);
            eventsManager.StopListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, NextRound);
        }
    }
}