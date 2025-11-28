using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BirdControl : MonoBehaviour
{
    public int rotateRate = 3;
    public float upSpeed = 10;
    public GameObject scoreMgr;
    public GameObject gameOverPanel;
    public float maxY = 5.2f;
    public AudioClip jumpUp;
    public AudioClip hit;
    public AudioClip score;

    public bool inGame = false;

    private bool dead = false;
    private bool landed = false;

    private Sequence birdSequence;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        // Lấy component Rigidbody2D một lần duy nhất lúc đầu
        rb = GetComponent<Rigidbody2D>();

        float birdOffset = 0.05f;
        float birdTime = 0.3f;
        float birdStartY = transform.position.y;

        birdSequence = DOTween.Sequence();

        birdSequence.Append(transform.DOMoveY(birdStartY + birdOffset, birdTime).SetEase(Ease.Linear))
            .Append(transform.DOMoveY(birdStartY - 2 * birdOffset, 2 * birdTime).SetEase(Ease.Linear))
            .Append(transform.DOMoveY(birdStartY, birdTime).SetEase(Ease.Linear))
            .SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inGame)
        {
            return;
        }
        birdSequence.Kill();

        if (!dead)
        {
            // Fix bird fly out of screen
            if (transform.position.y > maxY)
            {
                // Force the bird to lie on the ceiling, do not let it go higher
                transform.position = new Vector3(transform.position.x, maxY, transform.position.z);

                // If the bird is in flight, kill it immediately and let it fall naturally.
                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                JumpUp();
            }
        }

        if (!landed)
        {
            // Dùng biến rb đã cache
            float v = rb.velocity.y;
            float rotate = Mathf.Clamp(v * 2f, -45f, 10f);
            transform.rotation = Quaternion.Euler(0f, 0f, rotate);
        }
        else
        {
            rb.rotation = -90;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 🚫 Bỏ qua va chạm khi game chưa bắt đầu
        if (!inGame) return;

        if (other.name == "land" || other.name == "pipe_up" || other.name == "pipe_down")
        {
            if (!dead)
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag("movable");
                foreach (GameObject g in objs)
                {
                    g.BroadcastMessage("GameOver");
                }

                GetComponent<Animator>().SetTrigger("die");
                AudioSource.PlayClipAtPoint(hit, Vector3.zero);

                // Hiện panel Game Over sau 1 giây
                StartCoroutine(ShowGameOverDelay());
            }

            if (other.name == "land")
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(0, 0);
                landed = true;
            }
        }

        if (other.name == "pass_trigger")
        {
            scoreMgr.GetComponent<ScoreMgr>().AddScore();
            AudioSource.PlayClipAtPoint(score, Vector3.zero);
        }
    }

    IEnumerator ShowGameOverDelay()
    {
        yield return new WaitForSeconds(1f);

        if (gameOverPanel != null)
        {
            int currentScore = scoreMgr.GetComponent<ScoreMgr>().GetCurrentScore();
            gameOverPanel.GetComponent<GameOverPanel>().ShowGameOver(currentScore);
        }
    }

    public void JumpUp()
    {
        // Dùng rb đã cache
        rb.velocity = new Vector2(0, upSpeed);
        AudioSource.PlayClipAtPoint(jumpUp, Vector3.zero);
    }

    public void GameOver()
    {
        if (!dead)
        {
            dead = true;

            int finalScore = scoreMgr.GetComponent<ScoreMgr>().GetScore();
            Debug.Log($"Game Over! Final Score = {finalScore}");

            if (LeaderboardMgr.Instance == null)
            {
                Debug.LogError("❌ LeaderboardMgr.Instance is NULL — chưa có trong Scene hoặc bị Destroy!");
                return;
            }

            LeaderboardMgr.Instance.AddNewScore(finalScore);

            Debug.Log("✅ Score added to Leaderboard!");

            // 🔄 Cập nhật bảng xếp hạng ngay
            LeaderboardUI ui = FindObjectOfType<LeaderboardUI>();
            if (ui != null)
            {
                ui.ForceUpdate();
                Debug.Log("📋 Leaderboard UI refreshed!");
            }
        }
    }
}