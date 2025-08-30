using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject clearEffectPrefab;

    public TextMeshProUGUI clearText;
    public TextMeshProUGUI gameOverText;
    public GameObject ball;
    public GameObject retryButton;
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

        if(blockCount == 0)
        {
            StageClear();
        }
    }

    private void StageClear()
    {
        clearText.gameObject.SetActive(true);
        Instantiate(clearEffectPrefab, Vector3.zero, Quaternion.identity);
        retryButton.SetActive(true);
        isGameOver = true;
        clearSound.Play();

        ball.GetComponent<Ball>().StopBall();
        
        
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        retryButton.SetActive(true);
        isGameOver = true;

        Invoke("Retry", 5f);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
