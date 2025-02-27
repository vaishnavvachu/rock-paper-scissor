using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animation
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance { get; private set; }
        [SerializeField] private GameObject scorePopupPrefab;
        [SerializeField] private GameObject resultPopupPrefab;
        
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
            AnimationEvents.OnRoundResultPopup += HandleRoundResultPopup;
        }

        private void OnDisable()
        {
            AnimationEvents.OnButtonClick -= PlayButtonClickAnimation;
            AnimationEvents.OnScoreIncrease -= PlayScoreIncreaseAnimation;
            AnimationEvents.OnRoundResultPopup -= HandleRoundResultPopup;
        }
        private void PlayButtonClickAnimation(GameObject buttonObj)
        {
            Vector3 originalScale = buttonObj.transform.localScale;
            buttonObj.transform.DOScale(originalScale * 0.9f, 0.1f)
                .OnComplete(() => buttonObj.transform.DOScale(originalScale, 0.1f).SetEase(Ease.OutBack));
        }

        private void PlayScoreIncreaseAnimation(Vector3 position, string popupText)
        {
            GameObject popup = Instantiate(scorePopupPrefab, position, Quaternion.identity, transform);
            popup.GetComponent<FloatingScorePopup>().SetText(popupText);
        }
        
        private void HandleRoundResultPopup(string message)
        {
            GameObject popupObj = Instantiate(resultPopupPrefab);
            ResultPopup popupAnim = popupObj.GetComponent<ResultPopup>();
            popupAnim.Show(message);
        }
    }
}
