using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject gamePanel;

    private UIState _currentState;

    private void Start()
    {
        SetState(new MainMenuState());  
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
    }

    public void ShowMainMenu()
    {
        SetState(new MainMenuState());
    }
}