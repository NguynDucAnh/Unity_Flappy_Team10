using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> nameTexts;
    [SerializeField] private List<TextMeshProUGUI> scoreTexts;

    void OnEnable(){ ForceUpdate(); }

    public void ForceUpdate()
    {
        var b = LeaderboardMgr.Instance.GetLeaderboard();
        int n = Mathf.Min(nameTexts.Count, scoreTexts.Count);
        for (int i = 0; i < n; i++)
        {
            if (i < b.scores.Count)
            {
                nameTexts[i].text = b.scores[i].name;
                scoreTexts[i].text = b.scores[i].score.ToString();
            }
            else
            {
                nameTexts[i].text = "---";
                scoreTexts[i].text = "0";
            }
        }
    }
}
