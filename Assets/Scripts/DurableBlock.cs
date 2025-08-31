using UnityEngine;

public class DurableBlock : BlockBase
{
    public int hitPoints = 2;
    public GameObject crackEffectPrefab;   // 1��ڂ̂ЂуG�t�F�N�g
    public Sprite crackedSprite;           // �Ђъ����̉摜

    private void Start()
    {
        GameManager.instance.AddBlock();
    }

    public override void OnBallHit()
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            base.OnBallHit();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            OnBallHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            OnBallHit();
        }
    }
}