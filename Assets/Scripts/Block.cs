using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject breakEffectPrefab;

    private void Start()
    {
        GameManager.instance.AddBlock();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {        
            Destroy(gameObject);
            GameObject effect = Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
            effect.GetComponent<SoundFade>().PlayAndFadeOut();
            GameManager.instance.RemoveBlock();
        }
    }
}
