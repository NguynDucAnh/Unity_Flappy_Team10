using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // cần import namespace này để load scene

public class MenuController : MonoBehaviour
{
    // Hàm này sẽ được gọi khi nhấn nút Easy
    public void PlayEasy()
    {
        SceneManager.LoadScene("GameScene"); // thay bằng tên scene thật
    }
}
