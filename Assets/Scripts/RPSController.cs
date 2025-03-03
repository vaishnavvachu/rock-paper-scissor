using UnityEngine;

public class RPSController : MonoBehaviour
{
    private RPSModel _model;
    private RPSView _view;

    private void Start()
    {
        _model = new RPSModel();
        _view = FindFirstObjectByType<RPSView>();

        _view.OnChoiceSelected += _model.SetPlayerChoice;
        _model.OnGameResult += _view.UpdateUI;
    }
    public void OnPlayerChoice(RPSChoice choice)
    {
        _model.SetPlayerChoice(choice);
    }
    private void OnDestroy()
    {
        _view.OnChoiceSelected -= _model.SetPlayerChoice;
        _model.OnGameResult -= _view.UpdateUI;
    }
}