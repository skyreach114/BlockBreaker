using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 moveInput;

    // Update is called once per frame
    void Update()
    {
        // タッチやクリックがある場合は優先
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            // タッチ位置を取得
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane));
            transform.position = new Vector3(worldPos.x, transform.position.y, 0);
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            // マウスクリック位置を取得
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            transform.position = new Vector3(worldPos.x, transform.position.y, 0);
        }
        else
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, 0);
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }

        // 壁の内側で制限
        float clampedX = Mathf.Clamp(transform.position.x, -2.7f, 2.7f);
        transform.position = new Vector3(clampedX, transform.position.y, 0);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
