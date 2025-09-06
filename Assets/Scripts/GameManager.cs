using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using PlayFab.ClientModels;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float startTime;
    private float clearTime;
    private bool isTiming = false;
    private int blockCount;
    public bool isGameOver = false;
    public bool isCleared = false;
    private static bool hasCleared = false;

    public TextMeshProUGUI startText;
    public TextMeshProUGUI clearText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultTimeText;
    public TextMeshProUGUI topTimesText;
    public TextMeshProUGUI sendText;
    public TextMeshProUGUI sendCompletedText;

    public TMP_InputField nameInputField;

    public GameObject ball;
    public GameObject retryButton;
    public GameObject titleButton;
    public GameObject sendButton;
    public GameObject rankingButton;
    public GameObject clearEffectPrefab;
    public GameObject rankingPanel;
    public GameObject corkBoard;

    public AudioSource clearSound;

    private PlayFabLogin playfab;

    private static List<float> bestTimes = new List<float>();

    [Header("�����L���OUI")]
    public Transform rankingContent;     // ScrollView��Content
    public GameObject rowPrefab;         // 1�s�̃v���n�u

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playfab = FindFirstObjectByType<PlayFabLogin>();

        clearText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.SetActive(false);
        titleButton.SetActive(false);
        sendButton.SetActive(false);
        rankingButton.SetActive(false);
        resultTimeText.gameObject.SetActive(false);
        sendText.gameObject.SetActive(false);
        nameInputField.gameObject.SetActive(false);
        rankingPanel.SetActive(false);
        corkBoard.SetActive(false);

        if (hasCleared)
        {
            UpdateTopTimesUI();
        }
        else
        {
            topTimesText.gameObject.SetActive(false);
        }

        Invoke("EraseTitleText", 1.3f);
    }

    void EraseTitleText()
    {
        startText.gameObject.SetActive(false);

        startTime = Time.time;
        isTiming = true;
    }

    void Update()
    {
        if (isTiming)
        {
            clearTime = Time.time - startTime;
            timerText.text = clearTime.ToString("F2") + " s";
        }

        if (isGameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (EventSystem.current.currentSelectedGameObject == nameInputField.gameObject)
            {
                return;
            }

            Retry();
        }
    }

    public void AddBlock()
    {
        blockCount++;
    }

    public void RemoveBlock()
    {
        blockCount--;

        if (blockCount == 0)
        {
            StageClear();
        }
    }

    private void UpdateTopTimesUI()
    {
        if (bestTimes.Count == 0) return;

        // TOP3�\��
        string display = "Best Time\n";
        for (int i = 0; i < bestTimes.Count; i++)
        {
            display += (i + 1) + ". " + bestTimes[i].ToString("F2") + " s\n";
        }
        topTimesText.text = display;
        topTimesText.gameObject.SetActive(true);
    }

    private void StageClear()
    {
        hasCleared = true;
        isCleared = true;
        isTiming = false;

        resultTimeText.text = "Time : " + clearTime.ToString("F2") + " s";
        resultTimeText.gameObject.SetActive(true);

        // �^�C���ۑ����\�[�g
        bestTimes.Add(clearTime);
        bestTimes = bestTimes.OrderBy(t => t).Take(3).ToList();
        UpdateTopTimesUI();

        ball.GetComponent<Ball>().StopBall();

        clearText.gameObject.SetActive(true);
        Instantiate(clearEffectPrefab, Vector3.zero, Quaternion.identity);
        retryButton.SetActive(true);
        titleButton.SetActive(true);
        rankingButton.SetActive(true);
        
        isGameOver = true;
        clearSound.Play();;
    }

    public void OnSendButtonClicked()
    {
        if (playfab != null)
        {
            string playerName = nameInputField.text;

            if (!string.IsNullOrEmpty(playerName))
            {
                playfab.SetDisplayName(playerName);
            }
            playfab.SendScore(clearTime);
        }
        sendCompletedText.color = new Color32(255, 255, 255, 255);
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        while (true)
        {
            for (int i = 0; i < 255; i++)
            {
                sendCompletedText.color = sendCompletedText.color - new Color32(0, 0, 0, 1);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    public void OnRankingButtonClicked()
    {
        sendButton.SetActive(true);
        sendText.gameObject.SetActive(true);
        nameInputField.gameObject.SetActive(true);
        rankingPanel.SetActive(true);
        corkBoard.SetActive(true);

        if (playfab != null)
        {
            playfab.GetLeaderboard();
        }
    }

    public void ShowLeaderboard(List<PlayerLeaderboardEntry> entries)
    {
        // �Â��s���폜
        foreach (Transform child in rankingContent)
        {
            Destroy(child.gameObject);
        }

        // �V�����s��ǉ�
        foreach (var entry in entries)
        {
            GameObject row = Instantiate(rowPrefab, rankingContent);
            TMP_Text[] texts = row.GetComponentsInChildren<TMP_Text>();

            float realTime = 9999f - (entry.StatValue / 100f);

            texts[0].text = $"{entry.Position + 1}��"; // ����
            texts[1].text = string.IsNullOrEmpty(entry.DisplayName) ? "������" : entry.DisplayName; // ���O
            texts[2].text = $"{realTime:F2} s"; // �^�C��
        }
    }
    public void GameOver()
    {
        isTiming = false;
        isGameOver = true;

        gameOverText.gameObject.SetActive(true);
        retryButton.SetActive(true);
        titleButton.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
