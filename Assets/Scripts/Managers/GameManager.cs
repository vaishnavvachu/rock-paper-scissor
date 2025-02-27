using System.Collections;
using Animation;
using HandStrategy;
using Player;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
   [SerializeField] private TextMeshProUGUI scoreText;

    private int _score = 0;
    private Coroutine _selectionTimerCoroutine;
    private AIPlayer _aiPlayer;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _aiPlayer = FindFirstObjectByType<AIPlayer>();
    }
    public void StartRoundTimer()
    {
        CancelRoundTimer();
        UIManager.Instance.StartTimerAnimation();
        _selectionTimerCoroutine = StartCoroutine(PlayerSelectionTimer());
        UIManager.Instance.ResetHandImages();
    }
    public void CancelRoundTimer()
    {
        if (_selectionTimerCoroutine != null)
        {
            StopCoroutine(_selectionTimerCoroutine);
            _selectionTimerCoroutine = null;
        }
        UIManager.Instance.ResetTimerAnimation();
    }
    private IEnumerator PlayerSelectionTimer()
    {
        yield return new WaitForSeconds(2f);
        IHandStrategy aiHand = _aiPlayer.GetRandomHand();
        EvaluateRound(null, aiHand);
    }


    private string _message = "";
    public void EvaluateRound(IHandStrategy playerHand, IHandStrategy aiHand)
    {
        bool isDraw = false;
        if (playerHand == null)
        {
            _message = "Time's up! You lose!";
            _score = 0; 
            scoreText.text = _score.ToString();
            AnimationEvents.RoundResultPopup(_message);
            Invoke(nameof(ReturnToMainMenu), 2f);
            return;
        }
        
        HandType playerChoice = playerHand.GetHandType();
        HandType aiChoice = aiHand.GetHandType();
        isDraw = !playerHand.Beats(aiHand) && !aiHand.Beats(playerHand);
        
        if (playerHand.Beats(aiHand))
        {
            _score++;
            AnimationEvents.ScoreIncreased(Vector3.zero, "+1");
            _message = $"You Win! {playerChoice} beats {aiChoice}";
            scoreText.text = "Score: " + _score;
            Invoke(nameof(RestartRound), 2f);
        }
        else if (aiHand.Beats(playerHand))
        {
            _message = $"You Lose! {aiChoice} beats {playerChoice}";
            Invoke(nameof(ReturnToMainMenu), 2f);
        }
        else
        {
            _message = "It's a Draw!";
            Invoke(nameof(RestartRound), 2f);
        }
        AnimationEvents.RoundResultPopup(_message);
        
        HighScoreManager.Instance.UpdateScore(_score);
    }
    private void RestartRound()
    {
        StartRoundTimer();
    }
    private void ReturnToMainMenu()
    {
        UIManager.Instance.ShowMainMenu();
        ResetScore();
    }

    void ResetScore()
    {
        _score = 0;
        scoreText.text = _score.ToString();
    }
}