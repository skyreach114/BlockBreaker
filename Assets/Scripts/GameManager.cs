using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject clearEffectPrefab;

    public TextMeshProUGUI startText;
    public TextMeshProUGUI clearText;
    public TextMeshProUGUI gameOverText;
    public GameObject ball;
    public GameObject retryButton;
    public GameObject titleButton;
    public AudioSource clearSound;
    private int blockCount;
    private bool isGameOver = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clearText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.SetActive(false);
        titleButton.SetActive(false);
        Invoke("EraseTitleText", 1.3f);
    }

    void EraseTitleText()
    {
        startText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        clearText.gameObject.SetActive(true);
        Instantiate(clearEffectPrefab, Vector3.zero, Quaternion.identity);
        retryButton.SetActive(true);
        titleButton.SetActive(true);
        isGameOver = true;
        clearSound.Play();

        ball.GetComponent<Ball>().StopBall();


    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        retryButton.SetActive(true);
        titleButton.SetActive(true);
        isGameOver = true;
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
