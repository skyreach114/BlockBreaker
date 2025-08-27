using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public GameObject retryButton;
    private bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
