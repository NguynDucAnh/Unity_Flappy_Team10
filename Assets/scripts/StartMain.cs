using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartMain : MonoBehaviour
{
    public GameObject bird;
    
    // Xử lý các nút bấm (nếu dùng Sprite làm nút)
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                string btnName = hit.collider.gameObject.name;
                
                // Nút START -> Vào UpdateMap
                if (btnName == "start_btn" || btnName == "play") 
                {
                    SceneManager.LoadScene("UpdateMap");
                }
                // Nút RANK
                else if (btnName == "rank_btn" || btnName == "rank")
                {
                     if(LeaderboardMgr.Instance != null) 
                        LeaderboardMgr.Instance.ShowLeaderboard();
                }
                // Nút RATE
                else if (btnName == "rate_btn" || btnName == "rate")
                {
                    // Code mở dialog rate ở đây
                }
            }
        }
    }
    
    void Start()
    {
        // Hiệu ứng chim bay lượn sóng ở menu
        if(bird != null)
        {
            float startY = bird.transform.position.y;
            bird.transform.DOMoveY(startY + 0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }
}