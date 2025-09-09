using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleStart : MonoBehaviour
{
    private bool isRankingClicked = false;

    [SerializeField] private string gameSceneName = "Level1";

    public RankingManager rankingManager;
    private PlayFabLogin playfab;

    public void Start()
    {
        playfab = FindFirstObjectByType<PlayFabLogin>();
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnRankingButtonClicked()
    {
        if(isRankingClicked)
        {
            isRankingClicked = false;
            rankingManager.HideRanking();

            return;
        }

        isRankingClicked = true;

        if (playfab != null)
        {
            Debug.Log(isRankingClicked);
            playfab.GetLeaderboard();
        }
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
