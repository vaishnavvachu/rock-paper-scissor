using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Animation
{
    public class ResultPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI popupText;               
        [SerializeField] CanvasGroup canvasGroup;      
        [SerializeField] float fadeDuration = 0.5f;      
        [SerializeField] float displayDuration = 1.5f;   
        [SerializeField] float scaleDuration = 0.5f;     
        [SerializeField] float targetScale = 1f;       

        public void Show(string message)
        {
            popupText.text = message;
            transform.localScale = Vector3.zero;
            canvasGroup.alpha = 0;
        
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(targetScale, scaleDuration).SetEase(Ease.OutBack));
            seq.Join(canvasGroup.DOFade(1f, fadeDuration));
            seq.AppendInterval(displayDuration);
            seq.Append(canvasGroup.DOFade(0f, fadeDuration));
            seq.OnComplete(() => Destroy(gameObject));
        }
    }
}
