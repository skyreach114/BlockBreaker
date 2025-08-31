using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 7f;
    private Rigidbody2D rb;
    private bool isStopped = false;
    public bool isPiercing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float angle = Random.Range(-20f, -160f);
        Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        rb.linearVelocity = dir.normalized * speed;
    }
    void FixedUpdate()
    {
        if (isStopped) return;

        rb.linearVelocity = rb.linearVelocity.normalized * speed;
        Vector2 v = rb.linearVelocity;

        if (Mathf.Abs(v.x) < 0.2f)
        {
            v = new Vector2(Mathf.Sign(v.x) * 0.28f, v.y);
        }

        if (Mathf.Abs(v.y) < 0.2f)
        {
            v = new Vector2(v.x, Mathf.Sign(v.y) * 0.28f);
        }

        rb.linearVelocity = v.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // ボールの現在の速度を取得
            Vector2 velocity = rb.linearVelocity;

            // パドルの速度を取得（あらかじめRigidbody2Dを持たせるか、スクリプトから参照）
            float paddleVelocityX = collision.rigidbody != null ? collision.rigidbody.linearVelocity.x : 0f;

            // 横方向の力を少しだけ加える
            velocity.x += paddleVelocityX * 2f;

            float hitPos = transform.position.x - collision.transform.position.x;
            float halfWidth = collision.collider.bounds.size.x / 2;
            float normalizedHitPos = hitPos / halfWidth;

            // 端に当たるほど横方向の動きを強くする
            velocity.x += normalizedHitPos * 2f;

            // 再正規化して一定速度に保つ
            velocity = velocity.normalized * speed;
            rb.linearVelocity = velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPiercing && collision.CompareTag("Block"))
        {
            var block = collision.GetComponent<BlockBase>();
            if (block != null)
            {
                block.OnBallHit();
            }
        }
    }


    public void StopBall()
    {
        isStopped = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

    }

    public void IncreaseSpeed()
    {
        if (speed <= 15f) speed += 4f;
        Invoke("DecreaseSpeed", 5f);
    }

    public void DecreaseSpeed()
    {
        speed -= 4f;
    }

    public void ActivatePierce()
    {
        isPiercing = true;
        gameObject.layer = LayerMask.NameToLayer("PiercingBall");
        Invoke("DeactivatePierce", 5f);
    }

    public void DeactivatePierce()
    {
        isPiercing = false;
        gameObject.layer = LayerMask.NameToLayer("Ball");
    }
}
