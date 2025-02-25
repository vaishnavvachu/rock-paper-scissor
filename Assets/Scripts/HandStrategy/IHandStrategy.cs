namespace HandStrategy
{
    public interface IHandStrategy
    {
        bool Beats(IHandStrategy opponentHand);
        HandType GetHandType();
    }
}