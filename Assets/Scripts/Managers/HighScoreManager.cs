using Managers;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    private int _highScore = 0;
    public int HighScore => _highScore;

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScore();
        }
        else
        {
            Destroy(gameObject);
        }
        
        highScoreText.text = $"High Score: {HighScore}";
    }
    public void UpdateScore(int newScore)
    {
        if (newScore > _highScore)
        {
            _highScore = newScore;
            AudioManager.Instance.PlayLoseSFX();
            SaveHighScore();
        }
    }
    public void LoadHighScore()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = $"HIGHSCORE: {HighScore}";
    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", _highScore);
        PlayerPrefs.Save();
    }
}