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
            //CustomId = System.Guid.NewGuid().ToString(), // ����V�KID
            CustomId = SystemInfo.deviceUniqueIdentifier, // �f�o�C�X���ƂɌŗLID
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    public void SendScore(float clearTime)
    {
        // �������l���ǂ� �� ���]���ĕۑ�
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
            result => Debug.Log("�^�C�����M: " + clearTime + " s (�������ʂ܂�)"),
            error => Debug.LogError("�^�C�����M���s: " + error.GenerateErrorReport()));
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
                Debug.Log("�����L���O�擾����");
                gameManager.ShowLeaderboard(result.Leaderboard); // GameManager�ɕ\�����˗�
            },
            error => Debug.LogError("�����L���O�擾���s: " + error.GenerateErrorReport()));
    }


void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("���O�C�������I");
    }

    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("���O�C�����s: " + error.GenerateErrorReport());
    }
}

