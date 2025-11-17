using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BirdControl : MonoBehaviour
{
    // --- Các biến cài đặt gốc ---
    public int rotateRate = 3;
    public float upSpeed = 10;

    // --- Các biến nội bộ ---
    private MapData currentMap;
    private bool inGame = false;
    private bool dead = false;
    private bool landed = false; // ✅ Giữ lại biến này để xử lý logic xoay khi chạm đất
    private Sequence birdSequence;

    // --- Tái cấu trúc: Đăng ký sự kiện ---
    void Awake()
    {
        // Thử lấy MapData, nếu không có (ví dụ: test scene) thì không sao
        if (GameManager.Instance != null)
        {
            currentMap = GameManager.Instance.CurrentMapData;
        }
        
        // Đăng ký sự kiện
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOver += OnGameOver;
    }

    // --- Tái cấu trúc: Hủy đăng ký sự kiện ---
    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOver -= OnGameOver;
    }

    // --- ✅ PHỤC HỒI CODE GỐC: Logic animation lượn sóng ban đầu ---
    void Start()
    {
        float birdOffset = 0.05f;
        float birdTime = 0.3f;
        float birdStartY = transform.position.y;

        birdSequence = DOTween.Sequence();

        birdSequence.Append(transform.DOMoveY(birdStartY + birdOffset, birdTime).SetEase(Ease.Linear))
            .Append(transform.DOMoveY(birdStartY - 2 * birdOffset, 2 * birdTime).SetEase(Ease.Linear))
            .Append(transform.DOMoveY(birdStartY, birdTime).SetEase(Ease.Linear))
            .SetLoops(-1);
    }

    // --- Tái cấu trúc: Logic khi Game Bắt đầu ---
    private void OnGameStarted()
    {
        inGame = true;
        GetComponent<Rigidbody2D>().isKinematic = false; // Bật trọng lực
        if (birdSequence != null) // Check null cho an toàn
        {
            birdSequence.Kill(); // Dừng lượn sóng
        }
        JumpUp(); // Nhảy lần đầu
    }

    // --- Tái cấu trúc: Logic khi Game Kết thúc ---
    private void OnGameOver()
    {
        inGame = false;
        dead = true;
        
        // Dừng tất cả vật thể di chuyển
        GameObject[] objs = GameObject.FindGameObjectsWithTag("movable");
        foreach (GameObject g in objs)
        {
            // Dùng StopSpawning cho PipeSpawner và GameOver cho các cái khác
            if (g.GetComponent<PipeSpawner>() != null)
            {
                g.GetComponent<PipeSpawner>().StopSpawning();
            }
            else
            {
                g.BroadcastMessage("GameOver"); // Dùng cho LandControl, PipeMove cũ (nếu còn)
            }
        }
    }

    // --- ✅ HỢP NHẤT: Logic Update (Input + Xoay) ---
    void Update()
    {
        if (!inGame) return; // Chờ game bắt đầu

        // Logic Input
        if (!dead)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                JumpUp();
            }
        }

        // ✅ PHỤC HỒI CODE GỐC: Logic xoay chim
        if (!landed)
        {
            float v = transform.GetComponent<Rigidbody2D>().velocity.y;
            float rotate = Mathf.Clamp(v * 2f, -45f, 10f);
            transform.rotation = Quaternion.Euler(0f, 0f, rotate);
        }
        else
        {
            // Nếu đã chạm đất, giữ chim cắm đầu xuống
            transform.GetComponent<Rigidbody2D>().rotation = -90;
        }
    }

    // --- ✅ HỢP NHẤT: Logic Va chạm (Collision) ---
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!inGame) return;

        // Va chạm với ống hoặc đất
        if (other.name == "land" || other.name == "pipe_up" || other.name == "pipe_down")
        {
            if (!dead)
            {
                // ✅ Tái cấu trúc: Thông báo cho GameManager
                GameManager.Instance.EndGame(); 

                GetComponent<Animator>().SetTrigger("die");
                // Phát âm thanh va chạm từ MapData
                if (currentMap != null) 
                    AudioSource.PlayClipAtPoint(currentMap.hitSound, Vector3.zero);
            }

            // ✅ PHỤC HỒI CODE GỐC: Logic chạm đất
            if (other.name == "land")
            {
                transform.GetComponent<Rigidbody2D>().gravityScale = 0;
                transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                landed = true;
            }
        }

        // Ghi điểm
        if (other.name == "pass_trigger")
        {
            // ✅ Tái cấu trúc: Thông báo cho GameManager
            GameManager.Instance.AddScore();
            // Phát âm thanh ghi điểm từ MapData
            if (currentMap != null)
                AudioSource.PlayClipAtPoint(currentMap.scoreSound, Vector3.zero);
        }
    }

    // --- Tái cấu trúc: Logic Nhảy ---
    public void JumpUp()
    {
        if (dead || !inGame) return; // Không cho nhảy khi chết hoặc game chưa bắt đầu
        
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, upSpeed);
        
        // Phát âm thanh nhảy từ MapData
        if (currentMap != null)
            AudioSource.PlayClipAtPoint(currentMap.jumpSound, Vector3.zero);
    }
}