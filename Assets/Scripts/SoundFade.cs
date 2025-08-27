using UnityEngine;
using System.Collections;

public class SoundFade : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 0.9f; // フェードアウトにかける秒数
    public void PlayAndFadeOut()
    {
        audioSource.Play();
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // 次に使う時のためリセット
    }
}
