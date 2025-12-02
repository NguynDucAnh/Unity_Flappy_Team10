using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public float displayTime = 2f; // Thời gian hiện logo

    void Start()
    {
        // Bắt đầu coroutine để chuyển scene sau vài giây
        StartCoroutine(GoToNextScene());
    }

    private System.Collections.IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(displayTime);

        // Chuyển sang scene chính
        SceneManager.LoadScene("StartScene");
    }
}
