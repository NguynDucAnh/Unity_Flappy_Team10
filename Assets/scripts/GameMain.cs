using UnityEngine;

public class GameMain : MonoBehaviour
{
    [Header("Start overlay")]
    [SerializeField] private GameObject ready_pic;
    [SerializeField] private GameObject tip_pic;

    [Header("References")]
    [SerializeField] private Rigidbody2D bird;

    [Header("Gameplay")]
    [SerializeField] private float scrollSpeed = 2.5f;

    private bool clickedToStart = false;

    void Start()
    {
        PipeMove.scrollSpeed = 0f; // wait for first tap
        if (bird) bird.isKinematic = true;
    }

    void Update()
    {
        if (!clickedToStart && Input.GetMouseButtonDown(0))
        {
            clickedToStart = true;
            BeginGame();
        }
    }

    private void BeginGame()
    {
        PipeMove.scrollSpeed = Mathf.Max(0.1f, scrollSpeed);
        if (bird) bird.isKinematic = false;
        if (ready_pic) ready_pic.SetActive(false);
        if (tip_pic) tip_pic.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
