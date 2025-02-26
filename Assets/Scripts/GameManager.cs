using System.Collections;
using HandStrategy;
using Player;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

   [SerializeField] private TextMeshProUGUI resultText;
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
    
    public void EvaluateRound(IHandStrategy playerHand, IHandStrategy aiHand)
    {
        if (playerHand == null)
        {
            resultText.text = "Time's up! You lose!";
            _score = 0; 
            scoreText.text = "Score: " + _score;
            Invoke(nameof(ReturnToMainMenu), 1.5f);
            return;
        }
        HandType playerChoice = playerHand.GetHandType();
        HandType aiChoice = aiHand.GetHandType();

        if (playerHand.Beats(aiHand))
        {
            _score++;
            resultText.text = $"You Win! {playerChoice} beats {aiChoice}";
            scoreText.text = "Score: " + _score;
            Invoke(nameof(RestartRound), 0.5f);
        }
        else if (aiHand.Beats(playerHand))
        {
            resultText.text = $"You Lose! {aiChoice} beats {playerChoice}";
            _score = 0; 
            scoreText.text = "Score: " + _score;
            Invoke(nameof(ReturnToMainMenu), 1.5f);
        }
        else
        {
            resultText.text = "It's a Draw!";
            Invoke(nameof(RestartRound), 0.5f);
        }
    }
    private void RestartRound()
    {
       
        StartRoundTimer();
    }
    private void ReturnToMainMenu()
    {
        UIManager.Instance.ShowMainMenu();
    }
}