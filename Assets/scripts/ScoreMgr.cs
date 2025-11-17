using UnityEngine;
using System.Collections;
using TMPro; // ✅ THÊM DÒNG NÀY

public class ScoreMgr : MonoBehaviour
{
    // ✅ THÊM:
    public static ScoreMgr Instance { get; private set; }
    public TextMeshProUGUI scoreText; // Kéo TextMeshPro UI vào đây

    // ❌ XÓA: public GameObject[] scorePrefabs;
    // ❌ XÓA: public float digitOffset;
    // ❌ XÓA: private GameObject[] nowShowScores = new GameObject[5];

    private int nowScore = 0;

    void Awake()
    {
        Instance = this; // Tạo Singleton
        GameManager.OnScorePoint += AddScore; // Lắng nghe sự kiện
        GameManager.OnGameStarted += () => { SetScore(0); }; // Reset điểm khi game start
    }

    void OnDestroy()
    {
        GameManager.OnScorePoint -= AddScore;
        GameManager.OnGameStarted -= () => { SetScore(0); };
    }

    public void AddScore()
    {
        nowScore++;
        SetScore(nowScore);
    }

    public int GetCurrentScore()
    {
        return nowScore;
    }

    public void SetScore(int score)
    {
        nowScore = score;

        // ✅ ĐƠN GIẢN HÓA:
        if (scoreText != null)
        {
            scoreText.text = nowScore.ToString();
        }

        // ❌ XÓA: Toàn bộ vòng lặp for và logic "digits"
    }

    public int GetScore()
    {
        return nowScore;
    }
}