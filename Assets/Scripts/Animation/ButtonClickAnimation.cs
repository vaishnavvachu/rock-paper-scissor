using UnityEngine;
using UnityEngine.UI;

namespace Animation
{
    public class ButtonClickAnimation : MonoBehaviour
    {
        private Vector3 _originalScale;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => 
            {
                AnimationEvents.ButtonClicked(gameObject);
            });
        }
    }
}