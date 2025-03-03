using UnityEngine;

[CreateAssetMenu(fileName = "HandData", menuName = "Scriptable Objects/HandData")]
public class HandDataSO : ScriptableObject
{
    public RPSChoice handType; 
    public Sprite handSprite; 
}
