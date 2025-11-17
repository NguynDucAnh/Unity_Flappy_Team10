using UnityEngine;
using System.Collections;

public class LandControl : MonoBehaviour {

    // ❌ Xóa biến 'speed' cũ: public float speed = 5f;
    
    // Các biến này vẫn cần thiết để lặp lại mặt đất
    public float startX;
    public float endX;

    private bool inGame = false;

    void Awake()
    {
        // Lắng nghe GameManager thông báo
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOver += OnGameOver;
    }

    void OnDestroy()
    {
        // Hủy đăng ký
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOver -= OnGameOver;
    }

    private void OnGameStarted()
    {
        inGame = true;
    }

    private void OnGameOver()
    {
        inGame = false;
    }
    
    void Update () 
    {
        // Chỉ di chuyển khi game đang chạy
        if (!inGame) return; 

        // ✅ Quan trọng: Dùng biến static 'scrollSpeed' TỪ SCRIPT 'PipeMove'
        // Điều này đảm bảo mặt đất và ống luôn di chuyển CÙNG tốc độ
        transform.Translate(Vector2.left * PipeMove.scrollSpeed * Time.deltaTime);

        if (transform.position.x < endX)
        {
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }

    // ❌ Xóa hàm GameOver() cũ, vì giờ chúng ta dùng Event 'OnGameOver'
}