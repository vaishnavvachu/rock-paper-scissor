using DG.Tweening;
using UnityEngine;

namespace Animation
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance { get; private set; }
        [SerializeField] private GameObject scorePopUpPrefab;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            AnimationEvents.OnButtonClick += PlayButtonClickAnimation;
            AnimationEvents.OnScoreIncrease += PlayScoreIncreaseAnimation;
        }

        private void OnDisable()
        {
            AnimationEvents.OnButtonClick -= PlayButtonClickAnimation;
            AnimationEvents.OnScoreIncrease -= PlayScoreIncreaseAnimation;
        }
        private void PlayButtonClickAnimation(GameObject buttonObj)
        {
            Vector3 originalScale = buttonObj.transform.localScale;
            buttonObj.transform.DOScale(originalScale * 0.9f, 0.1f)
                .OnComplete(() => buttonObj.transform.DOScale(originalScale, 0.1f).SetEase(Ease.OutBack));
        }

        private void PlayScoreIncreaseAnimation(Vector3 position, string popupText)
        {
            GameObject popup = Instantiate(scorePopUpPrefab, position, Quaternion.identity, transform);
            popup.GetComponent<FloatingScorePopup>().SetText(popupText);
        }
    }
}
