using HandStrategy;
using UnityEngine;

namespace Player
{
    public class AIPlayer : MonoBehaviour
    {
        [SerializeField] private AIDifficultySettings difficultySettings;
        private static IHandStrategy[] hands = { new Rock(), new Paper(), new Scissors(), new Lizard(), new Spock() };

        public IHandStrategy GetRandomHand(IHandStrategy playerHand = null)
        {
            if (playerHand != null && Random.value < difficultySettings.chanceToWin)
            {
                foreach (IHandStrategy hand in hands)
                {
                    if (hand.Beats(playerHand))
                        return hand;
                }
            }
            
            return hands[Random.Range(0, hands.Length)];
        }
    }
}

