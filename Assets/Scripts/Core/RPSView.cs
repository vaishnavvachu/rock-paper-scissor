using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class RPSView : MonoBehaviour
{
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
    
    [SerializeField] private HandDataSO[] handDataArray;
    
    public event Action<RPSChoice> OnChoiceSelected;

    private void Start()
    {
        resultText.text = "Choose your hand!";
    }
    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }
    public void UpdateRoundUI(RPSChoice playerChoice, RPSChoice aiChoice, string result)
    {
        playerHandImage.sprite = GetSprite(playerChoice);
        aiHandImage.sprite = GetSprite(aiChoice);
        resultText.text = result;
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

    
}