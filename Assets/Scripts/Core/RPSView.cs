using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

public class RPSView : MonoBehaviour
{
    public event Action<RPSChoice> OnChoiceSelected;
    public event Action OnTimerExpired;
    
    [Header("Canvas References")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject gameCanvas;
    [Header("Button References")]
    [SerializeField] private Button rockButton;
    [SerializeField] private Button paperButton;
    [SerializeField] private Button scissorsButton;
    [SerializeField] private Button lizardButton;
    [SerializeField] private Button spockButton;
    [Header("TextBox References")]
    [SerializeField] private TextMeshProUGUI playerChoiceText;
    [SerializeField] private TextMeshProUGUI aiChoiceText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [Header("Image References")]
    [SerializeField] private Image playerHandImage;
    [SerializeField] private Image aiHandImage;
    [SerializeField] private Image timerImage;
    
    [SerializeField] private HandDataSO[] handDataArray;
    
    private float _roundTime = 2f;
    private bool _isTimerRunning;
    private bool _choiceMade;
    private Coroutine _timerCoroutine;
    private void Start()
    {
        resultText.text = "Choose your hand!";
    }
    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        ResetTimer();
    }
    
    public void UpdateRoundUI(RPSChoice playerChoice, RPSChoice aiChoice, string result)
    {
        playerHandImage.sprite = GetSprite(playerChoice);
        aiHandImage.sprite = GetSprite(aiChoice);
        resultText.text = result;
    }
    public void StartTimer()
    {
        _choiceMade = false;
        if (_isTimerRunning) return;
    
        _isTimerRunning = true;
        StartCoroutine(TimerCoroutine());
    }


    private IEnumerator TimerCoroutine()
    {
        _isTimerRunning = true;
        float timeRemaining = _roundTime;

        while (timeRemaining > 0)
        {
            if (_choiceMade)
            {
                _isTimerRunning = false;
                yield break;
            }

            timeRemaining -= Time.deltaTime;
            timerImage.fillAmount = timeRemaining / _roundTime; 
            yield return null;
        }

        _isTimerRunning = false;

        if (!_choiceMade)
        {
            OnTimerExpired?.Invoke();
        }
    }

    public void PlayerSelectsHand(RPSChoice choice)
    {
        if (_choiceMade) return;
        _choiceMade = true;

        StopCoroutine(TimerCoroutine()); 
        _isTimerRunning = false;
        timerImage.fillAmount = 1f;

        if (OnChoiceSelected == null)
        {
            Debug.LogError("OnChoiceSelected event is NULL!");  
        }
        else
        {
            OnChoiceSelected.Invoke(choice);
        }
    }



    public void ResetTimer()
    {
        _isTimerRunning = false;
        timerImage.fillAmount = 1f;
    }
    public void UpdateScoreUI(int playerScore)
    {
        playerScoreText.text = "Score: " + playerScore;
    }
    public void ShowGameUI()
    {
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }
    
    public void UpdateUI(RPSChoice playerChoice, RPSChoice aiChoice, string result)
    {
        playerHandImage.sprite = GetSprite(playerChoice);
        aiHandImage.sprite = GetSprite(aiChoice);
        playerChoiceText.text = "Player: " + playerChoice;
        aiChoiceText.text = "AI: " + aiChoice;
        resultText.text = result;
        ResetTimer();
    }

    private Sprite GetSprite(RPSChoice choice)
    {
        foreach (HandDataSO handData in handDataArray)
        {
            if (handData.handType == choice)
                return handData.handSprite;
        }
        return null;
    }
    public void ResetUI()
    {
        resultText.text = "Choose your hand!";
        playerHandImage.sprite = null; 
        aiHandImage.sprite = null;
        timerImage.fillAmount = 1f;
        playerScoreText.text = "Score: 0";
    }

}