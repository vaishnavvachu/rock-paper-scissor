using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class RPSUIAnimator : MonoBehaviour
{
    [Header("Hand Reveal")]
    [SerializeField] private Image playerHandImage;
    [SerializeField] private Image  aiHandImage;
    [Header("Result UI")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI resultText;
    //[SerializeField] private Image winLoseImage;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    
    public void AnimateHandReveal(Sprite playerSprite, Sprite aiSprite)
    {
        playerHandImage.sprite = playerSprite;
        aiHandImage.sprite = aiSprite;

        playerHandImage.transform.localScale = Vector3.zero;
        aiHandImage.transform.localScale = Vector3.zero;

        Sequence handRevealSequence = DOTween.Sequence();
        handRevealSequence.Append(playerHandImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack))
            .Join(aiHandImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack))
            .OnComplete(() => Debug.Log("Hand reveal animation complete"));
    }

    public void AnimateWinLoseScreen(RoundResult result, string reason)
    {
        resultPanel.SetActive(true);

        if (result == RoundResult.Win)
            resultText.color = Color.green;
        else if (result == RoundResult.Lose)
            resultText.color = Color.red;
        else
            resultText.color = Color.yellow;

        resultText.text = $"{result}\n{reason}";
        resultPanel.transform.localScale = Vector3.zero;
    
        resultPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutElastic)
            .OnComplete(() => HideResultPanelAfterDelay(2f));
    }

    private void HideResultPanelAfterDelay(float delay)
    {
        DOVirtual.DelayedCall(delay, () =>
        {
            resultPanel.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack)
                .OnComplete(() => resultPanel.SetActive(false));
        });
    }


    public void AnimateGameOverScreen()
    {
        gameOverPanel.transform.localScale = Vector3.zero;
        gameOverPanel.transform.DOScale(1f, 1.5f).SetEase(Ease.OutElastic);
        gameOverPanel.SetActive(true);
    }
}