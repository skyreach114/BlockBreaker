using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleStart : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Level1";

    // �{�^���p
    public void OnClickStart()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // �L�[�ł��J�n�ł���悤��
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
