using UnityEngine;
using UnityEngine.EventSystems;

public class EnsureEventSystem : MonoBehaviour
{
    void Awake()
    {
        // Nếu chưa có EventSystem nào trong scene thì tự tạo
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject obj = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            Debug.Log("✅ EventSystem tự động được thêm vào scene.");
        }
    }
}
