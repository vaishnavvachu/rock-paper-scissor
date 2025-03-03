public abstract class HandStrategy
{
    public abstract bool Beats(RPSChoice opponentChoice);
}

public class RockStrategy : HandStrategy
{
    public override bool Beats(RPSChoice opponentChoice) => 
        opponentChoice == RPSChoice.Scissors || opponentChoice == RPSChoice.Lizard;
}

public class PaperStrategy : HandStrategy
{
    public override bool Beats(RPSChoice opponentChoice) => 
        opponentChoice == RPSChoice.Rock || opponentChoice == RPSChoice.Spock;
}

public class ScissorsStrategy : HandStrategy
{
    public override bool Beats(RPSChoice opponentChoice) => 
        opponentChoice == RPSChoice.Paper || opponentChoice == RPSChoice.Lizard;
}

public class LizardStrategy : HandStrategy
{
    public override bool Beats(RPSChoice opponentChoice) => 
        opponentChoice == RPSChoice.Paper || opponentChoice == RPSChoice.Spock;
}

public class SpockStrategy : HandStrategy
{
    public override bool Beats(RPSChoice opponentChoice) => 
        opponentChoice == RPSChoice.Rock || opponentChoice == RPSChoice.Scissors;
}
