using System.Collections;
using Animation;
using HandStrategy;
using Player;
using TMPro;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] private TextMeshProUGUI scoreText;

        private int _score;
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
            if (playerHand == null)
            {
                _message = "Time's up! You lose!";
                OnTimeUp();
                return;
            }
        
            HandType playerChoice = playerHand.GetHandType();
            HandType aiChoice = aiHand.GetHandType();
            _ = !playerHand.Beats(aiHand) && !aiHand.Beats(playerHand);
        
            if (playerHand.Beats(aiHand))
            {
                OnPlayerWin();
                _message = $"You Win! {playerChoice} beats {aiChoice}";
            }
            else if (aiHand.Beats(playerHand))
            {
                _message = $"You Lose! {aiChoice} beats {playerChoice}";
                OnPlayerLose();
            }
            else
            {
                _message = "It's a Draw!";
                OnPlayerDraw();
            }
            AnimationEvents.RoundResultPopup(_message);
            HighScoreManager.Instance.UpdateScore(_score);
        }

        void OnTimeUp()
        {
            _score = 0; 
            scoreText.text = _score.ToString();
            AnimationEvents.RoundResultPopup(_message);
            Invoke(nameof(OnGameOver), 2f);
        }
        void OnPlayerWin()
        {
            _score++;
            scoreText.text = _score.ToString();
            AnimationEvents.ScoreIncreased(Vector3.zero, "+1");
            AudioManager.Instance.PlayWinSFX();
            Invoke(nameof(RestartRound), 2f);
        }

        void OnPlayerLose()
        {
            AudioManager.Instance.PlayLoseSFX();
            Invoke(nameof(OnGameOver), 2f);
        }

        void OnPlayerDraw()
        {
            AudioManager.Instance.PlayDrawSFX();
            Invoke(nameof(RestartRound), 2f);
        }
        private void RestartRound()
        {
            StartRoundTimer();
        }
        private void OnGameOver()
        {
            HighScoreManager.Instance.LoadHighScore();
            UIManager.Instance.ShowGameOver(_score);
            ResetScore();
        }

        void ResetScore()
        {
            _score = 0;
            scoreText.text = _score.ToString();
        }
    }
}