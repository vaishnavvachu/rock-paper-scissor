
using System;
using UnityEngine;
using Random = UnityEngine.Random;


public enum RPSChoice { Rock, Paper, Scissors, Lizard, Spock, None }
public enum RoundResult { Win, Lose, Tie }

public class RPSModel
{
    public RPSChoice PlayerChoice { get; private set; }
    public RPSChoice AIChoice { get; private set; }
    public int PlayerScore { get; private set; }
    public event Action<RPSChoice, RPSChoice, RoundResult> OnRoundComplete; 
    public event Action<int> OnScoreUpdated; 
    public event Action OnPlayerLose;
    
    public void SetPlayerChoice(RPSChoice choice)
    {
        PlayerChoice = choice;
        AIChoice = (RPSChoice)UnityEngine.Random.Range(0, 5);
        DetermineWinner();
    }

    private HandStrategy GetStrategy(RPSChoice choice)
    {
        return choice switch
        {
            RPSChoice.Rock => new RockStrategy(),
            RPSChoice.Paper => new PaperStrategy(),
            RPSChoice.Scissors => new ScissorsStrategy(),
            RPSChoice.Lizard => new LizardStrategy(),
            RPSChoice.Spock => new SpockStrategy(),
            _ => throw new ArgumentException("Invalid choice"),
        };
    }
    public void ResetGame()
    {
        PlayerScore = 0;
        OnScoreUpdated?.Invoke(PlayerScore);
    }
   
    private void DetermineWinner()
    {
        RoundResult roundResult;
        if (PlayerChoice == AIChoice)
        {
            roundResult = RoundResult.Tie;
        }
        else if (GetStrategy(PlayerChoice).Beats(AIChoice))
        {
            roundResult = RoundResult.Win;
            PlayerScore++;
            OnScoreUpdated?.Invoke(PlayerScore);
        }
        else
        {
            roundResult = RoundResult.Lose;
            OnPlayerLose?.Invoke();
            return;
        }

        OnRoundComplete?.Invoke(PlayerChoice, AIChoice, roundResult);

    }




    
}


