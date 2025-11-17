using UnityEngine;
using UnityEngine.SceneManagement;

// Đây là phiên bản MỚI của MapSelectController
public class MapSelectController : MonoBehaviour
{
    // Kéo 4 file MapData (bạn sẽ tạo ở Bước 4) vào 4 ô này
    public MapData map1Data;
    public MapData map2Data;
    public MapData map3Data;
    public MapData map4Data;

    // Hàm này được kết nối với nút Map 1 trong Unity Editor
    public void OnMap1ButtonClicked()
    {
        if (map1Data != null)
            GameManager.Instance.SelectMap(map1Data);
    }

    // Hàm này được kết nối với nút Map 2
    public void OnMap2ButtonClicked()
    {
        if (map2Data != null)
            GameManager.Instance.SelectMap(map2Data);
    }

    // Hàm này được kết nối với nút Map 3
    public void OnMap3ButtonClicked()
    {
        if (map3Data != null)
            GameManager.Instance.SelectMap(map3Data);
    }

    // Hàm này được kết nối với nút Map 4
    public void OnMap4ButtonClicked()
    {
        if (map4Data != null)
            GameManager.Instance.SelectMap(map4Data);
    }

    // Hàm cho nút Back (nếu có)
    public void GoToStartMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}