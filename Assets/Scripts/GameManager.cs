using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float startTime;
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

    public GameObject ball;
    public GameObject retryButton;
    public GameObject titleButton;
    public GameObject clearEffectPrefab;

    public AudioSource clearSound;

    private static List<float> bestTimes = new List<float>();

    private void Awake()
    {
        instance = this;
    }

    private void UpdateTopTimesUI()
    {
        if (bestTimes.Count == 0)
        {
            //topTimesText.gameObject.SetActive(false);
            return;
        }

        // TOP3表示
        string display = "Best Time\n";
        for (int i = 0; i < bestTimes.Count; i++)
        {
            display += (i + 1) + ". " + bestTimes[i].ToString("F2") + " s\n";
        }
        topTimesText.text = display;
        topTimesText.gameObject.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clearText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.SetActive(false);
        titleButton.SetActive(false);
        resultTimeText.gameObject.SetActive(false);

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

    // Update is called once per frame
    void Update()
    {
        if (isTiming)
        {
            float t = Time.time - startTime;
            timerText.text = t.ToString("F2") + " s";
        }

        if (isGameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
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

    private void StageClear()
    {
        hasCleared = true;
        isCleared = true;
        isTiming = false;

        float finalTime = Time.time - startTime;
        resultTimeText.text = "Time : " + finalTime.ToString("F2") + " s";
        resultTimeText.gameObject.SetActive(true);

        // タイム保存＆ソート
        bestTimes.Add(finalTime);
        bestTimes = bestTimes.OrderBy(t => t).Take(3).ToList();

        UpdateTopTimesUI();

        ball.GetComponent<Ball>().StopBall();

        clearText.gameObject.SetActive(true);
        Instantiate(clearEffectPrefab, Vector3.zero, Quaternion.identity);
        retryButton.SetActive(true);
        titleButton.SetActive(true);
        isGameOver = true;
        clearSound.Play();
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
