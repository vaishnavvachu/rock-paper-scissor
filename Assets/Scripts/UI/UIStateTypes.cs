using DG.Tweening;
using UnityEngine;

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
    
    public class GameOverState : UIState
    {
        private int finalScore;
    
        public GameOverState(int score)
        {
            finalScore = score;
        }
    
        public override void Enter(UIManager uiManager)
        {
            uiManager.gamePanel.SetActive(false);
            uiManager.mainMenuPanel.SetActive(false);
            uiManager.gameOverPanel.SetActive(true);
            uiManager.gameOverScoreText.text = "Score: " + finalScore;
        
            // Optionally, animate the GameOver panel (e.g., fade in)
            CanvasGroup cg = uiManager.gameOverPanel.GetComponent<CanvasGroup>();
            if(cg != null)
            {
                cg.alpha = 0;
                cg.DOFade(1f, 2f);
            }
        }

        public override void Exit(UIManager uiManager)
        {
            uiManager.gameOverPanel.SetActive(false);
        }
    }
}