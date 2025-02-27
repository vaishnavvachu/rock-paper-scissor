using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Animation
{
    public class FloatingScorePopup : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private float floatDistance = 100f;    
        [SerializeField] private float duration = 1f;           

        [Header("References")]
        [SerializeField] private TextMeshProUGUI popupText;   

        public void SetText(string text)
        {
            popupText.text = text;
        }

        private void Start()
        {
            popupText.transform.DOMoveY(popupText.transform.position.y + floatDistance, duration)
                .SetEase(Ease.OutQuad);
            popupText.DOFade(0f, duration)
                .SetEase(Ease.InQuad)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}