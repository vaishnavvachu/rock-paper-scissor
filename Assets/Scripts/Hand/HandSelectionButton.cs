using UnityEngine;
using UnityEngine.UI;

public class HandSelectionButton : MonoBehaviour
{
    public HandDataSO handData;
    public Image buttonImage;

    private RPSView _view;

    private void Start()
    {
        _view = FindFirstObjectByType<RPSView>(); 
        buttonImage.sprite = handData.handSprite;

        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _view.PlayerSelectsHand(handData.handType); 
    }
}