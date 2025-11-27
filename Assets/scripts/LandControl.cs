using UnityEngine;

public class LandControl : MonoBehaviour
{
    [SerializeField] private float startX = 10f;
    [SerializeField] private float endX = -10f;

    private bool moving = false;
    private System.Action onStart, onOver;

    void OnEnable()
    {
        onStart = () => moving = true;
        onOver = () => moving = false;
        GameManager.OnGameStarted += onStart;
        GameManager.OnGameOver += onOver;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= onStart;
        GameManager.OnGameOver -= onOver;
    }

    void Update()
    {
        if (!moving) return;
        transform.position += Vector3.left * (PipeMove.scrollSpeed * Time.deltaTime);
        if (transform.position.x <= endX)
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }
}
