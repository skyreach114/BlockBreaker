using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public GameObject rankingPanel;
    public GameObject corkBoard;

    [Header("ランキングUI")]
    public Transform rankingContent;     // ScrollViewのContent
    public GameObject rowPrefab;         // 1行のプレハブ

    private void Start()
    {
        rankingPanel.SetActive(false);
        corkBoard.SetActive(false);
    }

    public void ShowRanking(List<PlayerLeaderboardEntry> entries)
    {
        rankingPanel.SetActive(true);
        corkBoard.SetActive(true);

        // 古い行を削除
        foreach (Transform child in rankingContent)
        {
            Destroy(child.gameObject);
        }

        // 新しい行を追加
        foreach (var entry in entries)
        {
            GameObject row = Instantiate(rowPrefab, rankingContent);
            TMP_Text[] texts = row.GetComponentsInChildren<TMP_Text>();

            float realTime = 9999f - (entry.StatValue / 100f);

            texts[0].text = $"{entry.Position + 1}位"; // 順位
            texts[1].text = string.IsNullOrEmpty(entry.DisplayName) ? "名無し" : entry.DisplayName; // 名前
            texts[2].text = $"{realTime:F2} s"; // タイム
        }
    }

    public void HideRanking()
    {
        rankingPanel.SetActive(false);
        corkBoard.SetActive(false);
    }
}
