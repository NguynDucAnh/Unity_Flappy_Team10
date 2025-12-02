using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartMain : MonoBehaviour
{
    public GameObject bird;

    // Tự động tìm, không cần kéo thả
    private RatingDialog ratingDialog;
    private LeaderboardMgr leaderboardMgr;

    void Start()
    {
        // 1. Tự động tìm các script quản lý
        ratingDialog = FindObjectOfType<RatingDialog>();
        leaderboardMgr = FindObjectOfType<LeaderboardMgr>();

        // 2. Hiệu ứng chim bay lượn sóng
        if (bird != null)
        {
            float startY = bird.transform.position.y;
            bird.transform.DOMoveY(startY + 0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }

    private void Update()
    {
        // Hỗ trợ cả chuột và cảm ứng
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                string btnName = hit.collider.gameObject.name;
                
                // Debug để xem mình bấm trúng cái gì
                Debug.Log("Bấm trúng: " + btnName);

                // Xử lý nút
                if (btnName == "start_btn" || btnName == "play")
                {
                    SceneManager.LoadScene("UpdateMap");
                }
                else if (btnName == "rank_btn" || btnName == "rank")
                {
                    if (leaderboardMgr != null) leaderboardMgr.ShowLeaderboard();
                }
                else if (btnName == "rate_btn" || btnName == "rate")
                {
                    if (ratingDialog != null) ratingDialog.ShowDialog();
                }
            }
        }
    }
}