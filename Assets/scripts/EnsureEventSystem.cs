using UnityEngine;
using UnityEngine.EventSystems;

[DefaultExecutionOrder(-1000)]
public class EnsureEventSystem : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            var go = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            DontDestroyOnLoad(go);
        }
    }
}
