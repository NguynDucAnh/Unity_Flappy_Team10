using UnityEngine;
using UnityEngine.UI;
using Game.Infra; // cần dòng này để nhìn thấy MapId

public class MapSelectController : MonoBehaviour
{
    [Header("Gán 3 buttons ở Inspector")]
    public Button mapWinterBtn;
    public Button mapMushroomBtn;
    public Button mapLavaBtn;

    private void Awake()
    {
        mapWinterBtn.onClick.AddListener(() => GameManager.Instance.SelectMap(MapId.Winter));
        mapMushroomBtn.onClick.AddListener(() => GameManager.Instance.SelectMap(MapId.MushroomNight));
        mapLavaBtn.onClick.AddListener(() => GameManager.Instance.SelectMap(MapId.Lava));
    }
}
