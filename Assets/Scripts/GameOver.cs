using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            gameOverText.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}
