using HandStrategy;
using UnityEngine;

namespace Player
{
    public class AIPlayer : MonoBehaviour
    {
        private static IHandStrategy[] hands = { new Rock(), new Paper(), new Scissors(), new Lizard(), new Spock() };

        public IHandStrategy GetRandomHand()
        {
            return hands[Random.Range(0, hands.Length)];
        }
    }
}

