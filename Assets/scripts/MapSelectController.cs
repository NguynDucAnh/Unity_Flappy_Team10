using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectController : MonoBehaviour
{
    // Gán hàm này vào nút Map 1 (Fly / Classic)
    public void LoadMapFly()
    {
        SceneManager.LoadScene("GameScene_Fly");
    }

    // Gán hàm này vào nút Map 2 (Sea / Biển)
    public void LoadMapSea()
    {
        SceneManager.LoadScene("GameScene_Sea");
    }

    // Gán hàm này vào nút Map 3 (Snow / Tuyết)
    public void LoadMapSnow()
    {
        SceneManager.LoadScene("GameScene_Snow");
    }

    // Gán hàm này vào nút Map 4 (Night / Nấm Đêm)
    public void LoadMapNight()
    {
        SceneManager.LoadScene("GameScene_NightMushroom");
    }
    
    // Gán hàm này vào nút Map 5 (Solid / Đất)
    public void LoadMapSolid()
    {
        SceneManager.LoadScene("GameScene_Soild");
    }

    // Gán vào nút Back
    public void BackToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}