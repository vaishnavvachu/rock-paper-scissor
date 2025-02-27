using System;
using UnityEngine;

namespace Animation
{
    public static class AnimationEvents
    {
        public static event Action<GameObject> OnButtonClick;

        public static void ButtonClicked(GameObject button)
        {
            OnButtonClick?.Invoke(button);
        }
        public static event Action<Vector3, string> OnScoreIncrease;

        public static void ScoreIncreased(Vector3 position, string popupText)
        {
            OnScoreIncrease?.Invoke(position, popupText);
        }
    }
}