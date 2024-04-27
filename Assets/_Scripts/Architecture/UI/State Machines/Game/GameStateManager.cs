using UnityEngine;
using Zenject;
using TMPro;

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
    [SerializeField] int nextRoundScreenDisplayTime = 50;
    bool incrementRound;
    
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        eventsManager.StartListening(GameEvent.GameViewModelEvent.GAME_OVER, DisplayGameOverScreen);
        eventsManager.StartListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, DisplayNextRoundScreen);
    }

    void SetRoundText(){
        enemyWaveDetails = gameViewModel.enemyWaveDetails;

        roundText.text = "Round " + enemyWaveDetails.round.ToString();
    }

    void Start()
    {
    }

    void Update()
    {
        if (!incrementRound) return;
    
        if (timeElapsed < nextRoundScreenDisplayTime)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed = 0;
            incrementRound = false;
            nextRoundScreen.SetActive(false);
        }
    }

    public void NextWave ()
    {
        incrementRound = true;
    }

    public void DisplayNextRoundScreen()
    {
        nextRoundScreen.SetActive(true);
        roundText = GameObject.Find("round_text").GetComponent<TextMeshProUGUI>();
        SetRoundText();
        incrementRound = true;
    }

    public void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    void OnDisable()
    {
        eventsManager.StopListening(GameEvent.GameViewModelEvent.GAME_OVER, DisplayGameOverScreen);
        eventsManager.StopListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, DisplayNextRoundScreen);
    }
}
