using System.Collections.Generic;
using DG.Tweening;
using HandStrategy;
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
        
        [Header("Hand UI References")]
        [SerializeField] private Image playerHandImage;
        [SerializeField] private Image aiHandImage;
        
        [Header("Timer UI References")]
        [SerializeField] private Image timerProgressBar; 
        
        [Header("Sprite References")]
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
        
        public void UpdateHandImages(HandType playerHand, HandType aiHand)
        {
            if (_handSprites.TryGetValue(playerHand, out Sprite playerSprite))
                playerHandImage.sprite = playerSprite;
            else
                Debug.LogError($"No sprite found: {playerHand}");

            if (_handSprites.TryGetValue(aiHand, out Sprite aiSprite))
                aiHandImage.sprite = aiSprite;
            else
                Debug.LogError($"No sprite found: {aiHand}");
        }
        
        public void ResetHandImages()
        {
            playerHandImage.sprite = null;
            aiHandImage.sprite = null;
        }
    }
}