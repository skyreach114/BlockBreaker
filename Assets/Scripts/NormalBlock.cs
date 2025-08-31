using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : BlockBase
{
    private void Start()
    {
        GameManager.instance.AddBlock();
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
        if(collision.gameObject.CompareTag("Ball"))
        {
            OnBallHit();
        }
    }
}
