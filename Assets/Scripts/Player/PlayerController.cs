using HandStrategy;
using Managers;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private IHandStrategy _playerHand;
        private AIPlayer _aiPlayer;
        void Start()
        {
            _aiPlayer = FindFirstObjectByType<AIPlayer>();
        }
        public void SelectHand(int handIndex)
        {
            if(GameManager.Instance.IsRoundEvaluating)
                return;
            
            UIManager.Instance.SetButtonsInteractable(false);
            
            GameManager.Instance.CancelRoundTimer();
            
            HandType handType = (HandType)handIndex;
        
            switch (handType)
            {
                case HandType.Rock:
                    _playerHand = new Rock();
                    break;
                case HandType.Paper:
                    _playerHand = new Paper();
                    break;
                case HandType.Scissors:
                    _playerHand = new Scissors();
                    break;
                case HandType.Lizard:
                    _playerHand = new Lizard();
                    break;
                case HandType.Spock:
                    _playerHand = new Spock();
                    break;
            }

            IHandStrategy aiHand = _aiPlayer.GetRandomHand(_playerHand);
        
            GameManager.Instance.EvaluateRound(_playerHand, aiHand);
            UIManager.Instance.UpdateHandImages(_playerHand.GetHandType(), aiHand.GetHandType());

        }
        
        
    }
}