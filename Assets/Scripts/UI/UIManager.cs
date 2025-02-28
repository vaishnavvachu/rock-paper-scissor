using System.Collections.Generic;
using DG.Tweening;
using HandStrategy;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        
        #region Public References
        [Header("Canvas References")]
        public GameObject mainMenuPanel;
        public GameObject gamePanel;
        public GameObject gameOverPanel;
        
        [Header("GameOver UI Elements")]
        public TextMeshProUGUI gameOverScoreText;
        
        [Header("Hand UI References")]
        [SerializeField] private Image playerHandImage;
        [SerializeField] private Image aiHandImage;
        [SerializeField] private Button[] handButtons;
        
        [Header("Timer UI References")]
        [SerializeField] private Image timerProgressBar; 
        
        [Header("Sprite References")]
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite rockSprite;
        [SerializeField] private Sprite paperSprite;
        [SerializeField] private Sprite scissorsSprite;
        [SerializeField] private Sprite lizardSprite;
        [SerializeField] private Sprite spockSprite;
        #endregion
        
        #region Private References
        private UIState _currentState;
        private Dictionary<HandType, Sprite> _handSprites;
        #endregion

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
                return;
            }
            
            _handSprites = new Dictionary<HandType, Sprite>
            {
                { HandType.Rock, rockSprite },
                { HandType.Paper, paperSprite },
                { HandType.Scissors, scissorsSprite },
                { HandType.Lizard, lizardSprite },
                { HandType.Spock, spockSprite }
            };
        }

        private void Start()
        {
            SetState(new MainMenuState()); 
        }
        public void StartTimerAnimation()
        {
            if (timerProgressBar != null)
            {
                timerProgressBar.fillAmount = 1; 
                timerProgressBar.DOFillAmount(0, 2f).SetEase(Ease.Linear);
            }
        }
        public void ResetTimerAnimation()
        {
            if (timerProgressBar != null)
            {
                timerProgressBar.DOKill();
                timerProgressBar.fillAmount = 1;
            }
        }
        public void SetState(UIState newState)
        {
            _currentState?.Exit(this);
            _currentState = newState;
            _currentState.Enter(this);
        }

        public void StartGame()
        {
            SetState(new GameState()); 
            GameManager.Instance.StartRoundTimer();
        }

        public void ShowMainMenu()
        {
            SetState(new MainMenuState());
        }
         
        public void ShowGameOver(int finalScore)
        {
            SetState(new GameOverState(finalScore));
        }
        
        public void SetButtonsInteractable(bool state)
        {
            foreach (Button btn in handButtons)
            {
                btn.interactable = state;
            }
        }
        public void UpdateHandImages(HandType playerHand, HandType aiHand)
        {
            if (_handSprites.TryGetValue(playerHand, out Sprite playerSprite))
            {
                playerHandImage.sprite = playerSprite;
                AnimatePlayerHand(playerSprite);
            }
            else
                Debug.LogError($"No sprite found: {playerHand}");

            if (_handSprites.TryGetValue(aiHand, out Sprite aiSprite))
            {
                aiHandImage.sprite = aiSprite;
                AnimateAIHand(aiSprite);
            }
            else
                Debug.LogError($"No sprite found: {aiHand}");
        }
        
        public void ResetHandImages()
        {
            playerHandImage.sprite = defaultSprite;
            aiHandImage.sprite = defaultSprite;
           
            playerHandImage.color = new Color(1, 1, 1, 0);
            aiHandImage.color = new Color(1, 1, 1, 0);
        }
        
        private void AnimatePlayerHand(Sprite playerSprite)
        {
            playerHandImage.sprite = playerSprite;
            playerHandImage.transform.localScale = Vector3.zero;
            playerHandImage.color = new Color(1, 1, 1, 0);
            
            playerHandImage.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            playerHandImage.DOFade(1f, 0.5f);
        }
        
        private void AnimateAIHand(Sprite aiSprite, float delay = 0.1f)
        {
            aiHandImage.sprite = aiSprite;
            aiHandImage.transform.localScale = Vector3.zero;
            aiHandImage.color = new Color(1, 1, 1, 0);

            aiHandImage.transform.DOScale(1f, 0.5f).SetDelay(delay).SetEase(Ease.OutBack);
            aiHandImage.DOFade(1f, 0.5f).SetDelay(delay);
        }
        
    }
}