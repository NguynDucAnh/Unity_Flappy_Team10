using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float flapForce = 260f;

    [Header("Tags")]
    [SerializeField] private string groundTag = "Ground";
    [SerializeField] private string obstacleTag = "Obstacle";
    [SerializeField] private string scoreZoneTag = "ScoreZone";

    [Header("Audio (per scene)")]
    [SerializeField] private AudioClip flapSound;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip hitSound;

    private Rigidbody2D rb;
    private bool dead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOver += OnGameOver;
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOver -= OnGameOver;
    }

    private void OnGameStarted()
    {
        dead = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = false;
        Flap();
        if (flapSound) AudioSource.PlayClipAtPoint(flapSound, Vector3.zero);
    }

    private void OnGameOver()
    {
        dead = true;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (dead) return;
        if (Input.GetMouseButtonDown(0))
        {
            Flap();
            if (flapSound) AudioSource.PlayClipAtPoint(flapSound, Vector3.zero);
        }
    }

    private void Flap()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * flapForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(groundTag) || collision.collider.CompareTag(obstacleTag))
        {
            if (!dead) GameManager.Instance.EndGame();
            if (hitSound) AudioSource.PlayClipAtPoint(hitSound, Vector3.zero);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(scoreZoneTag))
        {
            GameManager.Instance.AddScore();
            if (scoreSound) AudioSource.PlayClipAtPoint(scoreSound, Vector3.zero);
        }
    }
}
