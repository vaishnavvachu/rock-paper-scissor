using UnityEngine;

[CreateAssetMenu(fileName = "AIDifficultySettings", menuName = "Scriptable Objects/AIDifficultySettings")]
public class AIDifficultySettings : ScriptableObject
{
    [Header("AI Reaction Time")]
    [Tooltip("Time (in seconds) the AI waits before selecting a hand.")]
    public float reactionTime = 1f;

    [Header("Move Selection Bias")]
    [Tooltip("Probability (0-1) that the AI will choose a hand that intentionally wins over the player's move.")]
    [Range(0, 1)]
    public float chanceToWin = 0.5f;
}
