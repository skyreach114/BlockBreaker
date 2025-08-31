using UnityEngine;

public class DurableBlock : BlockBase
{
    public int hitPoints = 2;
    public GameObject crackEffectPrefab;   // 1回目のひびエフェクト
    public Sprite crackedSprite;           // ひび割れ後の画像

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
            // ひび割れ演出
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