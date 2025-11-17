using UnityEngine;
using System.Collections;
using DG.Tweening; // Giữ lại DOTween cho animation
using UnityEngine.UI;

public class StartMain : MonoBehaviour
{
    public GameObject bird;
    private Sequence birdSequence;

    // Use this for initialization
    void Start()
    {
        // Logic animation lượn sóng cho chim ở Menu
        float birdOffset = 0.05f;
        float birdTime = 0.3f;
        float birdStartY = bird.transform.position.y;

        birdSequence = DOTween.Sequence();

        birdSequence.Append(bird.transform.DOMoveY(birdStartY + birdOffset, birdTime).SetEase(Ease.Linear))
            .Append(bird.transform.DOMoveY(birdStartY - 2 * birdOffset, 2 * birdTime).SetEase(Ease.Linear))
            .Append(bird.transform.DOMoveY(birdStartY, birdTime).SetEase(Ease.Linear))
            .SetLoops(-1);
    }

    // ❌ TOÀN BỘ logic Update() và GameOver() cũ đã được XÓA
    // vì chúng không thuộc về StartScene
}