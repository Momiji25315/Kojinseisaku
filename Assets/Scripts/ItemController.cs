using UnityEngine;

public class ItemController : MonoBehaviour
{
    // private に変更し、インスペクターから見えなくする
    private float moveSpeed = 5f;
    private float deadZone = -15f;

    // ItemSpawnerから速度を設定してもらうための専用入口（メソッド）
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    void Update()
    {
        // もし親オブジェクトがいる場合（足場の上に乗っている場合）は何もしない
        if (transform.parent != null)
        {
            return;
        }

        // 親がいない（地面に直接生成された）場合のみ、自分で動く
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}