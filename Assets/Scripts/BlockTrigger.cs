using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    private BlockBase parentBlock;

    private void Start()
    {
        parentBlock = GetComponentInParent<BlockBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Ball ball = collision.GetComponent<Ball>();
            if (ball != null && ball.isPiercing)
            {
                parentBlock.OnBallHit();  // 親のブロックを壊す処理
            }
        }
    }
}
