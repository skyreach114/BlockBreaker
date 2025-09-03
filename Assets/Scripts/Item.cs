using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isStopped = false;

    public enum ItemType { SpeedUp, Pierce }
    public ItemType type;

    public Sprite speedUpSprite;
    public Sprite pierceSprite;
    public AudioSource speedUpSound;
    public AudioSource pierceSound;

    private void Start()
    {
        // ��ނɉ����ĉ摜��؂�ւ���
        var sr = GetComponent<SpriteRenderer>();
        if (type == ItemType.SpeedUp && speedUpSprite != null)
        {
            sr.sprite = speedUpSprite;
        }
        else if (type == ItemType.Pierce && pierceSprite != null)
        {
            sr.sprite = pierceSprite;
        }
    }

    private void Update()
    {
        // �N���A���Q�[���I�[�o�[�Ȃ�~�߂�
        if (GameManager.instance.isCleared || GameManager.instance.isGameOver)
        {
            isStopped = true;
        }

        if (!isStopped)
        {
            transform.Translate(Vector2.down * 3f * Time.deltaTime);
        }
    }

    public void StopItem()
    {
        isStopped = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            if (type == ItemType.SpeedUp)
            {
                FindFirstObjectByType<Ball>().IncreaseSpeed();
                if (speedUpSound != null && speedUpSound.clip != null)
                {
                    AudioSource.PlayClipAtPoint(speedUpSound.clip, transform.position, 0.3f);
                }
            }
            else if (type == ItemType.Pierce)
            {
                FindFirstObjectByType<Ball>().ActivatePierce();
                if (pierceSound != null && pierceSound.clip != null)
                {
                    AudioSource.PlayClipAtPoint(pierceSound.clip, transform.position, 0.3f);
                }
            }
            Destroy(gameObject);
        }

        else if (collision.CompareTag("Bottom")) // ��ʉ��ɗ������������
        {
            Destroy(gameObject);
        }
    }
}

