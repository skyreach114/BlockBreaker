using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 9.5f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float angle = Random.Range(-20f, -160f);
        Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        rb.linearVelocity = dir.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
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
            velocity.x += paddleVelocityX * 1.5f;

            // 再正規化して一定速度に保つ
            velocity = velocity.normalized * speed;

            rb.linearVelocity = velocity;
        }
    }

}
