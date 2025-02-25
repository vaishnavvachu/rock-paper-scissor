using HandStrategy;
using UnityEngine;

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

            IHandStrategy aiHand = _aiPlayer.GetRandomHand();
        
            GameManager.Instance.EvaluateRound(_playerHand, aiHand);
        }
    }
}