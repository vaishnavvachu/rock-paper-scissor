namespace HandStrategy
{
    public class Rock : IHandStrategy
    {
        public bool Beats(IHandStrategy opponentHand)
        {
            return opponentHand.GetHandType() == HandType.Scissors || opponentHand.GetHandType() == HandType.Lizard;
        }

        public HandType GetHandType() => HandType.Rock;
    }

    public class Paper : IHandStrategy
    {
        public bool Beats(IHandStrategy opponentHand)
        {
            return opponentHand.GetHandType() == HandType.Rock || opponentHand.GetHandType() == HandType.Spock;
        }

        public HandType GetHandType() => HandType.Paper;
    }

    public class Scissors : IHandStrategy
    {
        public bool Beats(IHandStrategy opponentHand)
        {
            return opponentHand.GetHandType() == HandType.Paper || opponentHand.GetHandType() == HandType.Lizard;
        }

        public HandType GetHandType() => HandType.Scissors;
    }

    public class Lizard : IHandStrategy
    {
        public bool Beats(IHandStrategy opponentHand)
        {
            return opponentHand.GetHandType() == HandType.Paper || opponentHand.GetHandType() == HandType.Spock;
        }

        public HandType GetHandType() => HandType.Lizard;
    }

    public class Spock : IHandStrategy
    {
        public bool Beats(IHandStrategy opponentHand)
        {
            return opponentHand.GetHandType() == HandType.Rock || opponentHand.GetHandType() == HandType.Scissors;
        }

        public HandType GetHandType() => HandType.Spock;
    }
}