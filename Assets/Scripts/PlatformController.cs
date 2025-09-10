using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // この者の速さを覚えておくためのもの
    private float moveSpeed = 5f;

    // この者が消え去るべき、世界の果て（X座標）
    private float deadZone = -30f; // 少し広めに取っておこうかのう

    // 外部から速さを授かるための唯一の窓口
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    // 毎フレーム、瞬きのごとく呼ばれる命令
    void Update()
    {
        // 我が身を左へ、定められた速さで動かす
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // もし、世界の果てを越えてしまったならば…
        // （自分の子も含めた座標で判断する必要がある）
        if (transform.position.x < deadZone)
        {
            // …我が身をこの世から消し去る
            Destroy(gameObject);
        }
    }
}