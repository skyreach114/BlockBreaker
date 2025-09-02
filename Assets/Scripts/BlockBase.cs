using UnityEngine;

public class BlockBase : MonoBehaviour
{
    public GameObject breakEffectPrefab;
    public GameObject itemPrefab;

    public virtual void OnBallHit()
    {
        BreakBlock();
    }

    protected void BreakBlock()
    {

        Destroy(gameObject);

        GameObject effect = Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
        effect.GetComponent<SoundFade>().PlayAndFadeOut();
        GameManager.instance.RemoveBlock();

        if (Random.value < 0.3f) // 30%�̊m��
        {
            GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

            // ItemType �������_���Ɍ���
            Item itemScript = item.GetComponent<Item>();
            itemScript.type = (Random.value < 0.5f) ? Item.ItemType.SpeedUp : Item.ItemType.Pierce;
        }
    }
}
