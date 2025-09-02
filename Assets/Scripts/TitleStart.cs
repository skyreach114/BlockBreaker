using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleStart : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Level1";

    // ボタン用
    public void OnClickStart()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // キーでも開始できるように
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
