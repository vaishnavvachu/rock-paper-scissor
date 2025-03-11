using System.Collections.Generic;
public static class WinningReason
{
    private static readonly Dictionary<(RPSChoice, RPSChoice), string> WinningReasons =
        new Dictionary<(RPSChoice, RPSChoice), string>
        {
            {(RPSChoice.Rock, RPSChoice.Scissors), "Rock crushes Scissors"},
            {(RPSChoice.Scissors, RPSChoice.Paper), "Scissors cuts Paper"},
            {(RPSChoice.Paper, RPSChoice.Rock), "Paper covers Rock"},
            {(RPSChoice.Rock, RPSChoice.Lizard), "Rock crushes Lizard"},
            {(RPSChoice.Lizard, RPSChoice.Spock), "Lizard poisons Spock"},
            {(RPSChoice.Spock, RPSChoice.Scissors), "Spock smashes Scissors"},
            {(RPSChoice.Scissors, RPSChoice.Lizard), "Scissors decapitates Lizard"},
            {(RPSChoice.Lizard, RPSChoice.Paper), "Lizard eats Paper"},
            {(RPSChoice.Paper, RPSChoice.Spock), "Paper disproves Spock"},
            {(RPSChoice.Spock, RPSChoice.Rock), "Spock vaporizes Rock"}
        };

    public static string GetWinningReason(RPSChoice playerChoice, RPSChoice aiChoice)
    {
        if (playerChoice == aiChoice)
            return $"Both chose {playerChoice}";

        if (WinningReasons.TryGetValue((playerChoice, aiChoice), out string reason))
            return reason;

        if (WinningReasons.TryGetValue((aiChoice, playerChoice), out reason))
            return reason; 

        return "Unknown outcome";
    }
}