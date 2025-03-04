using UnityEngine;

public class RPSController : MonoBehaviour
{
    private RPSModel _model;
    private RPSView _view;

    private void Start()
    {
        _model = new RPSModel();
        _view = FindFirstObjectByType<RPSView>();

        _model.OnRoundComplete += _view.UpdateRoundUI;
        _model.OnScoreUpdated += _view.UpdateScoreUI;
        _model.OnPlayerLose += _view.ShowMainMenu; 

        _view.ShowMainMenu();
    }
    
    public void OnPlayerChoice(RPSChoice choice)
    {
        _model.SetPlayerChoice(choice);
    }
    private void OnDestroy()
    {
        _view.OnChoiceSelected -= _model.SetPlayerChoice;
        _model.OnRoundComplete -= _view.UpdateUI;
    }
    public void StartGame()
    {
        _view.ShowGameUI();
    }
}