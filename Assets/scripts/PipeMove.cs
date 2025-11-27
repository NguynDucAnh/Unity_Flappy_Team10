using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public static float scrollSpeed = 0f;
    [SerializeField] private float killX = -12f;

    private bool moving = false;

    void OnEnable()
    {
        GameManager.OnGameStarted += HandleStart;
        GameManager.OnGameOver += HandleGameOver;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= HandleStart;
        GameManager.OnGameOver -= HandleGameOver;
    }

    private void HandleStart() { moving = true; }
    private void HandleGameOver() { moving = false; }

    void Update()
    {
        if (!moving) return;
        transform.position += Vector3.left * (scrollSpeed * Time.deltaTime);
        if (transform.position.x < killX) Destroy(gameObject);
    }
}
