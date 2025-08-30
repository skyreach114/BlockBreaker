using UnityEngine;

public class DurableBlock : MonoBehaviour
{
    public int hitPoints = 2;
    public GameObject crackEffectPrefab;   // 1��ڂ̂ЂуG�t�F�N�g
    public GameObject breakEffectPrefab;   // 2��ڂ̔j��G�t�F�N�g
    public Sprite crackedSprite;           // �Ђъ����̉摜

    private void Start()
    {
        GameManager.instance.AddBlock();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitPoints--;

            if (hitPoints <= 0)
            {
                // �j��G�t�F�N�g
                if (breakEffectPrefab != null)
                {
                    GameObject effect = Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
                    effect.GetComponent<SoundFade>().PlayAndFadeOut();
                }

                Destroy(gameObject);
                GameManager.instance.RemoveBlock();
            }
            else
            {
                // �Ђъ��ꉉ�o
                if (crackedSprite != null)
                {
                    GetComponent<SpriteRenderer>().sprite = crackedSprite;
                }
                if (crackEffectPrefab != null)
                {
                    Instantiate(crackEffectPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
