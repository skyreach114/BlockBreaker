using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject breakEffectPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // エフェクト生成（位置はブロックの位置）
            GameObject effect = Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
            effect.GetComponent<SoundFade>().PlayAndFadeOut();
            Destroy(gameObject);
        }
    }
}
