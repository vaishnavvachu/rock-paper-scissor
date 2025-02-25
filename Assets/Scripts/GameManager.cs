using HandStrategy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

   [SerializeField] private TextMeshProUGUI resultText;
   [SerializeField] private TextMeshProUGUI scoreText;

    private int _score = 0;

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

    public void EvaluateRound(IHandStrategy playerHand, IHandStrategy aiHand)
    {
        HandType playerChoice = playerHand.GetHandType();
        HandType aiChoice = aiHand.GetHandType();

        if (playerHand.Beats(aiHand))
        {
            _score++;
            resultText.text = $"You Win!";
        }
        else if (aiHand.Beats(playerHand))
        {
            resultText.text = $"You Lose!";
            _score = 0;
        }
        else
        {
            resultText.text = "It's a Draw!";
        }

        scoreText.text = "Score: " + _score;
    }
}