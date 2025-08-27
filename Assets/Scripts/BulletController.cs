using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("弾の基本設定")]
    public float speed = 15f;    // 弾の速さ
    public float lifeTime = 3f;  // 弾が勝手に消えるまでの時間

    private Rigidbody2D rb;

    // 弾が生まれた瞬間に一度だけ呼ばれる命令
    void Start()
    {
        // 物理演算の宝玉（Rigidbody2D）を見つけて覚えておく
        rb = GetComponent<Rigidbody2D>();
        // 前方（右向き）に、設定された速さで力を与える
        rb.linearVelocity = transform.right * speed;
        // lifeTime秒後に、自動的に我が身を消し去る予約をする
        Destroy(gameObject, lifeTime);
    }

    // ★★★ Trigger（Is TriggerがON）の当たり判定に触れた時に呼ばれる命令 ★★★
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                // ★★★ 伝令鳩を放つ呪文を追加じゃ！ ★★★
                Debug.Log("敵に命中！ 点滅の術、発動を命ず！");
                enemy.TakeDamage();
            }
            Destroy(gameObject);
        }
    }
}