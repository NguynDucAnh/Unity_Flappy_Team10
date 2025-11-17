using UnityEngine;
using System.Collections;

public class PipeMove : MonoBehaviour {

    // ✅ Biến 'static' này sẽ được set bởi GameMain (SceneInitializer)
    //    và được dùng chung bởi cả LandControl.
    public static float scrollSpeed = 5f; 

    private bool inGame = false;

    void Awake()
    {
        // Lắng nghe GameManager thông báo
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOver += OnGameOver;
    }

    void OnDestroy()
    {
        // Hủy đăng ký khi ống bị hủy (rất quan trọng)
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

        // Sử dụng biến static 'scrollSpeed'
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    // ❌ Xóa hàm GameOver() cũ, vì giờ chúng ta dùng Event 'OnGameOver'
}