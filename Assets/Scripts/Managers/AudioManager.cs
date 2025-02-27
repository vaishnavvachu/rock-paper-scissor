using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
    
        [Header("Audio Source")]
        [SerializeField] private AudioSource sfxSource;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip buttonClickClip;
        [SerializeField] private AudioClip winClip;
        [SerializeField] private AudioClip loseClip;
        [SerializeField] private AudioClip drawClip;
        [SerializeField] private AudioClip highScoreClip;

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
    
        public void PlayButtonClickSFX()
        {
            if(buttonClickClip != null)
                sfxSource.PlayOneShot(buttonClickClip);
            else
                Debug.LogWarning("ButtonClick clip not assigned.");
        }

  
        public void PlayWinSFX()
        {
            if(winClip != null)
                sfxSource.PlayOneShot(winClip);
            else
                Debug.LogWarning("Win clip not assigned.");
        }
    
        public void PlayLoseSFX()
        {
            if(loseClip != null)
                sfxSource.PlayOneShot(loseClip);
            else
                Debug.LogWarning("Lose clip not assigned.");
        }

  
        public void PlayDrawSFX()
        {
            if(drawClip != null)
                sfxSource.PlayOneShot(drawClip);
            else
                Debug.LogWarning("Draw clip not assigned.");
        }

        public void PlayHighScoreSFX()
        {
            if(highScoreClip != null)
                sfxSource.PlayOneShot(highScoreClip);
            else
                Debug.LogWarning("High Score clip not assigned.");
            
        }
    }
}