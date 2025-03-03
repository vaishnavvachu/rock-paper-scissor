using UnityEngine;
using UnityEngine.UI;

public class RPSButton : MonoBehaviour
{
    public HandDataSO handData;  
    public Image buttonImage;  

    private RPSController _controller;

    private void Start()
    {
        _controller = FindFirstObjectByType<RPSController>();
        buttonImage.sprite = handData.handSprite; 

        GetComponent<Button>().onClick.AddListener(() => _controller.OnPlayerChoice(handData.handType));
    }
}