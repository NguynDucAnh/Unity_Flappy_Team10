using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    public GameObject bird;
    public GameObject readyPic;
    public GameObject tipPic;

    // ✅ THÊM: Các "vị trí" để tạo asset
    public Transform backgroundContainer;
    public Transform landContainer;

    // ❌ XÓA: public GameObject scoreMgr;
    // ❌ XÓA: public GameObject pipeSpawner;
    // ❌ XÓA: private bool gameStarted = false;

    void Start()
{
    // Lấy dữ liệu map hiện tại
    MapData map = GameManager.Instance.CurrentMapData;

    // 1. Khởi tạo môi trường ĐÚNG
    if (map.backgroundPrefab != null)
        Instantiate(map.backgroundPrefab, backgroundContainer);

    if (map.landPrefab != null)
        Instantiate(map.landPrefab, landContainer);

    // 2. ✅ SỬA LỖI: Chỉ cần set tốc độ cho PipeMove.
    //    LandControl sẽ tự động đọc tốc độ này.
    PipeMove.scrollSpeed = map.scrollSpeed;

    // ❌ XÓA DÒNG GÂY LỖI NÀY:
    // LandControl.speed = map.scrollSpeed;

    // 3. Chuẩn bị bird (tắt trọng lực)
    bird.GetComponent<Rigidbody2D>().isKinematic = true;
}
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // 1. Kích hoạt GameManager
            GameManager.Instance.StartGame();

            // 2. Ẩn UI
            readyPic.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.2f);
            tipPic.GetComponent<SpriteRenderer>().material.DOFade(0f, 0.2f);

            // 3. Hủy script này đi, nó đã xong nhiệm vụ
            enabled = false;
        }
    }

    // ❌ XÓA: private void StartGame() {}
    // ❌ XÓA: public void GameOver() {}
}