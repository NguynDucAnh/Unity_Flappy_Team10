using UnityEngine;

public class StartMain : MonoBehaviour
{
    [SerializeField] private Transform bird;
    [SerializeField] private float amplitude = 0.1f;
    [SerializeField] private float frequency = 1.5f;
    private Vector3 startPos;

    void Start(){ if (bird) startPos = bird.position; }

    void Update()
    {
        if (!bird) return;
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        bird.position = startPos + new Vector3(0, y, 0);
    }
}
