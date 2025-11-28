using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Khi click chuột vào object này (có collider)
        SceneManager.LoadScene("UpdateMap");
    }
}
