namespace UI
{
    public class MainMenuState : UIState
    {
        public override void Enter(UIManager uiManager)
        {
            uiManager.mainMenuPanel.SetActive(true);
            uiManager.gamePanel.SetActive(false);
        }

        public override void Exit(UIManager uiManager)
        {
            uiManager.mainMenuPanel.SetActive(false);
        }
    }

    public class GameState : UIState
    {
        public override void Enter(UIManager uiManager)
        {
            uiManager.gamePanel.SetActive(true);
            uiManager.mainMenuPanel.SetActive(false);
        }

        public override void Exit(UIManager uiManager)
        {
            uiManager.gamePanel.SetActive(false);
        }
    }
}