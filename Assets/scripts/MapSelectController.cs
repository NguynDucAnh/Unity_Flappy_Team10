using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectController : MonoBehaviour
{
    public void SelectMap(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
