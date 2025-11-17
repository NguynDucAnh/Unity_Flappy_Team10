using UnityEngine;
using TMPro; // Đảm bảo bạn đang dùng TextMeshPro
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    public List<TextMeshProUGUI> nameTexts;
    public List<TextMeshProUGUI> scoreTexts;

    void OnEnable()
    {
        ForceUpdate();
    }

    public void ForceUpdate()
    {
        var lb = LeaderboardMgr.Instance.GetLeaderboard();

        for (int i = 0; i < nameTexts.Count; i++)
        {
            if (i < lb.scores.Count)
            {
                nameTexts[i].text = lb.scores[i].name;
                scoreTexts[i].text = lb.scores[i].score.ToString();
            }
            else
            {
                nameTexts[i].text = "---";
                scoreTexts[i].text = "---";
            }
        }
    }
}