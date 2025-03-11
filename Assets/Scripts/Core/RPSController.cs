using System.Collections;
using UnityEngine;

public class RPSController : MonoBehaviour
{
    private RPSModel _model;
    private RPSView _view;

    private int _highScore;
    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        _model = new RPSModel();
        _view = FindFirstObjectByType<RPSView>();

        _model.OnRoundComplete += HandleRoundComplete;
        _model.OnScoreUpdated += UpdateScore;
        _model.OnPlayerLose += _view.ShowMainMenu;
        
        _view.OnTimerExpired += HandleTimeout;
        _view.OnChoiceSelected += OnPlayerChoice;
        
        _view.UpdateHighScoreUI(_highScore); 
    }

    public void StartGame()
    {
        _model.ResetGame();  
        _view.ResetUI();    
        _view.ShowGameUI();  

        StartNewRound();
    }

    private void StartNewRound()
    {
        _view.ResetTimer();
        _view.StartTimer();
    }
    public void UpdateScore(int playerScore)
    {
        _view.UpdateScoreUI(playerScore);

        if (playerScore > _highScore)
        {
            _highScore = playerScore;
            PlayerPrefs.SetInt("HighScore", _highScore);
            PlayerPrefs.Save();
            _view.UpdateHighScoreUI(_highScore); 
        }
    }
    private void HandleRoundComplete(RPSChoice playerChoice, RPSChoice aiChoice, RoundResult result)
    {
        _view.UpdateRoundUI(playerChoice, aiChoice, result);
        if (result == RoundResult.Win || result == RoundResult.Tie)
        {
            StartCoroutine(StartNewRoundWithDelay(2f));
        }
        else
        {
            _view.ShowMainMenu(); 
        }
    }

    private IEnumerator StartNewRoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartNewRound();
    }

    private void HandleTimeout()
    {
        _view.ShowMainMenu();
    }

    public void OnPlayerChoice(RPSChoice choice)
    {
        
        _model.SetPlayerChoice(choice);
    }

    private void OnDestroy()
    {
        if (_view != null)
        {
            _view.OnChoiceSelected -= OnPlayerChoice;
            _view.OnTimerExpired -= HandleTimeout;
        }

        if (_model != null)
        {
            _model.OnRoundComplete -= HandleRoundComplete;
            _model.OnScoreUpdated -= _view.UpdateScoreUI;
            _model.OnPlayerLose -= _view.ShowMainMenu;
        }

    }
}
