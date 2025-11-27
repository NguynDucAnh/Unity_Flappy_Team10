using UnityEngine;
using TMPro;

public class ScoreMgr : MonoBehaviour
{
    public static ScoreMgr Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    private int nowScore = 0;
    private System.Action startedHandler;

    void Awake()
    {
        Instance = this;
        startedHandler = () => SetScore(0);
        GameManager.OnGameStarted += startedHandler;
        GameManager.OnScorePoint += AddScore;
        SetScore(0);
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= startedHandler;
        GameManager.OnScorePoint -= AddScore;
    }

    public void AddScore() => SetScore(nowScore + 1);
    public int GetScore() => nowScore;

    private void SetScore(int v)
    {
        nowScore = v;
        if (scoreText) scoreText.text = nowScore.ToString();
    }
}
