using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, 0);
        transform.Translate(moveDirection * speed * Time.deltaTime);
        // 壁の内側で制限
        float clampedX = Mathf.Clamp(transform.position.x, -2.7f, 2.7f);
        transform.position = new Vector3(clampedX, transform.position.y, 0);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
