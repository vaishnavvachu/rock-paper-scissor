using System.Collections;
using UnityEngine;

public class RPSController : MonoBehaviour
{
    private RPSModel _model;
    private RPSView _view;

    private void Start()
    {
        _model = new RPSModel();
        _view = FindFirstObjectByType<RPSView>();

        _model.OnRoundComplete += HandleRoundComplete;
        _model.OnScoreUpdated += _view.UpdateScoreUI;
        _model.OnPlayerLose += _view.ShowMainMenu;
        
        _view.OnTimerExpired += HandleTimeout;
        _view.OnChoiceSelected += OnPlayerChoice;
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

    private void HandleRoundComplete(RPSChoice playerChoice, RPSChoice aiChoice, RoundResult result)
    {
        _view.UpdateRoundUI(playerChoice, aiChoice, result.ToString());
        if (result == RoundResult.Win || result == RoundResult.Tie)
        {
            StartCoroutine(StartNewRoundWithDelay(1f));
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
