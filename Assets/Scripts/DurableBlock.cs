using UnityEngine;

public class DurableBlock : MonoBehaviour
{
    public int hitPoints = 2;
    public GameObject crackEffectPrefab;   // 1回目のひびエフェクト
    public GameObject breakEffectPrefab;   // 2回目の破壊エフェクト
    public Sprite crackedSprite;           // ひび割れ後の画像

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
                // 破壊エフェクト
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
    }
}
