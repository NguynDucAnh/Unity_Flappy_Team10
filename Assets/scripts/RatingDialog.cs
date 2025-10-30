using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RatingDialog : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialogPanel;
    public GameObject[] stars; // 5 star objects
    public Button submitButton;
    public Button closeButton;

    [Header("Star Sprites")]
    public Sprite starEmpty;
    public Sprite starFilled;

    [Header("Settings")]
    public float starAnimationDuration = 0.2f;
    public float starScalePunch = 1.3f;

    private int currentRating = 0;
    private bool hasRated = false;

    private const string RATING_KEY = "PlayerRating";
    private const string HAS_RATED_KEY = "HasRated";

    void Start()
    {
        // Kiểm tra xem đã rate chưa
        hasRated = PlayerPrefs.GetInt(HAS_RATED_KEY, 0) == 1;

        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        SetupStarButtons();
        SetupButtons();
    }

    private void SetupStarButtons()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            int starIndex = i + 1; // 1-5 instead of 0-4
            Button starButton = stars[i].GetComponent<Button>();

            if (starButton != null)
            {
                starButton.onClick.AddListener(() => OnStarClicked(starIndex));
            }

            // Set initial sprite
            Image starImage = stars[i].GetComponent<Image>();
            if (starImage != null && starEmpty != null)
            {
                starImage.sprite = starEmpty;
            }
        }
    }

    private void SetupButtons()
    {
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmitRating);
            submitButton.interactable = false;
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseDialog);
        }
    }

    public void ShowDialog()
    {
        if (hasRated)
        {
            Debug.Log("User has already rated");
            return;
        }

        if (dialogPanel != null)
        {
            dialogPanel.SetActive(true);

            // Animation hiển thị
            dialogPanel.transform.localScale = Vector3.zero;
            dialogPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        }

        currentRating = 0;
        UpdateStarDisplay();

        if (submitButton != null)
            submitButton.interactable = false;
    }

    private void OnStarClicked(int rating)
    {
        currentRating = rating;
        UpdateStarDisplay();

        // Animate star được click
        if (stars[rating - 1] != null)
        {
            stars[rating - 1].transform.DOKill();
            stars[rating - 1].transform.localScale = Vector3.one;
            stars[rating - 1].transform.DOPunchScale(Vector3.one * (starScalePunch - 1f), starAnimationDuration, 1);
        }

        if (submitButton != null)
            submitButton.interactable = true;
    }

    private void UpdateStarDisplay()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            Image starImage = stars[i].GetComponent<Image>();
            if (starImage != null)
            {
                if (i < currentRating)
                {
                    starImage.sprite = starFilled;
                    starImage.color = Color.white;
                }
                else
                {
                    starImage.sprite = starEmpty;
                    starImage.color = new Color(1f, 1f, 1f, 0.5f);
                }
            }
        }
    }

    private void OnSubmitRating()
    {
        if (currentRating == 0) return;

        // Lưu rating vào PlayerPrefs
        PlayerPrefs.SetInt(RATING_KEY, currentRating);
        PlayerPrefs.SetInt(HAS_RATED_KEY, 1);
        PlayerPrefs.Save();

        hasRated = true;

        Debug.Log($"Rating saved: {currentRating} stars");

        // Đóng dialog ngay
        CloseDialog();

        // Nếu rating >= 4, có thể mở link store để rate thật
        if (currentRating >= 4)
        {
            // OpenStoreForRating(); // Implement riêng nếu cần
        }
    }

    public void CloseDialog()
    {
        if (dialogPanel != null)
        {
            dialogPanel.transform.DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => dialogPanel.SetActive(false));
        }
    }

    // Hàm để mở store (tuỳ chọn)
    private void OpenStoreForRating()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + Application.identifier);
#elif UNITY_IOS
        Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_APP_ID");
#endif
    }

    // Hàm để check xem đã rate chưa
    public static bool HasUserRated()
    {
        return PlayerPrefs.GetInt(HAS_RATED_KEY, 0) == 1;
    }

    // Hàm để lấy rating
    public static int GetSavedRating()
    {
        return PlayerPrefs.GetInt(RATING_KEY, 0);
    }

    // Hàm để reset rating (dùng cho testing)
    public static void ResetRating()
    {
        PlayerPrefs.DeleteKey(RATING_KEY);
        PlayerPrefs.DeleteKey(HAS_RATED_KEY);
        PlayerPrefs.Save();
    }
}