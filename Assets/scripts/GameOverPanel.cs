using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject panelRoot;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button homeButton;

    void Awake()
    {
        if (panelRoot) panelRoot.SetActive(false);
        GameManager.OnGameOver += ShowPanel;
        if (playButton) playButton.onClick.AddListener(Restart);
        if (homeButton) homeButton.onClick.AddListener(GameManager.Instance.LoadStartScene);
    }

    void OnDestroy(){ GameManager.OnGameOver -= ShowPanel; }

    private void ShowPanel()
    {
        int current = ScoreMgr.Instance.GetScore();
        LeaderboardMgr.Instance.AddScore("Player", current);
        var lb = LeaderboardMgr.Instance.GetLeaderboard();
        int best = (lb.scores != null && lb.scores.Count > 0) ? lb.scores[0].score : current;

        if (scoreText) scoreText.text = current.ToString();
        if (bestScoreText) bestScoreText.text = best.ToString();
        if (panelRoot) panelRoot.SetActive(true);
    }

    private void Restart()
    {
        var active = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(active);
    }
}
