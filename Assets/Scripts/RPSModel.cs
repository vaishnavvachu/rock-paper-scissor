using UnityEngine;
using System;
using Random = System.Random;

public enum RPSChoice { Rock, Paper, Scissors, Lizard, Spock, None }

public class RPSModel
{
    public RPSChoice PlayerChoice { get; private set; }
    public RPSChoice AIChoice { get; private set; }

    public event Action<RPSChoice, RPSChoice, string> OnGameResult;

    private Random _random = new Random();

    public void SetPlayerChoice(RPSChoice choice)
    {
        PlayerChoice = choice;
        AIChoice = (RPSChoice)_random.Next(0, 5);
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

    private void DetermineWinner()
    {
        string result;

        if (PlayerChoice == AIChoice)
        {
            result = "It's a Tie!";
        }
        else if (GetStrategy(PlayerChoice).Beats(AIChoice))
        {
            result = "You Win!";
        }
        else
        {
            result = "You Lose!";
        }

        OnGameResult?.Invoke(PlayerChoice, AIChoice, result);
    }
}


