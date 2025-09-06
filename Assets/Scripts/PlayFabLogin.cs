using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        var request = new LoginWithCustomIDRequest
        {
            //CustomId = System.Guid.NewGuid().ToString(), // 毎回新規ID
            CustomId = SystemInfo.deviceUniqueIdentifier, // デバイスごとに固有ID
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    public void SendScore(float clearTime)
    {
        // 小さい値が良い → 反転して保存
        int invertedValue = Mathf.RoundToInt((9999f - clearTime) * 100f);

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new System.Collections.Generic.List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "BestTime",
                    Value = invertedValue
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request,
            result => Debug.Log("タイム送信: " + clearTime + " s (小数第二位まで)"),
            error => Debug.LogError("タイム送信失敗: " + error.GenerateErrorReport()));
    }

    public void SetDisplayName(string name)
    {
        var req = new UpdateUserTitleDisplayNameRequest { DisplayName = name };
        PlayFabClientAPI.UpdateUserTitleDisplayName(req,
            res => Debug.Log("DisplayName set: " + res.DisplayName),
            err => Debug.LogError(err.GenerateErrorReport()));
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "BestTime",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request,
            result =>
            {
                Debug.Log("ランキング取得成功");
                gameManager.ShowLeaderboard(result.Leaderboard); // GameManagerに表示を依頼
            },
            error => Debug.LogError("ランキング取得失敗: " + error.GenerateErrorReport()));
    }


void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("ログイン成功！");
    }

    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("ログイン失敗: " + error.GenerateErrorReport());
    }
}

