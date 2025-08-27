using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ジャンプ設定")]
    public float jumpForce = 10f;

    [Header("接地判定")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("アイテムと弾")]
    public int itemsPerShot = 5;
    public GameObject bulletPrefab;
    public Transform firePoint;

    // --- 内部で使う変数 ---
    private Rigidbody2D rb;
    private bool isGrounded;
    private int currentItemCount = 0;
    private int shotCount = 0;

    // ★★★ アニメーション再生装置を操るための変数を追加じゃ ★★★
    private Animator anim;

    // ゲーム開始時に一度だけ呼ばれる
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ★★★ 自分自身に付いておる Animator を見つけて、変数に覚えておくのじゃ ★★★
        anim = GetComponent<Animator>();
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        // まず、地面にいるかどうかを判断する
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ★★★ ここからが新しい魔法じゃ ★★★
        // もし地面にいるならば…
        if (isGrounded)
        {
            // …アニメーションの時間を通常の速度で進める (1が通常速度)
            anim.speed = 1;
        }
        // そうでなければ (つまり、空中にいるならば) …
        else
        {
            // …アニメーションの時間を止めてしまうのじゃ！ (0で停止)
            anim.speed = 0;
        }
        // ★★★ ここまでじゃ ★★★


        // --- ジャンプ処理 ---
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // --- 弾の発射処理 ---
        if (shotCount > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    // (これより下の OnTriggerEnter2D や Shoot メソッドなどは変更なしじゃ)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            currentItemCount++;
            Debug.Log("アイテム取得！ 現在のカウント: " + currentItemCount);

            if (currentItemCount >= itemsPerShot)
            {
                currentItemCount = 0;
                shotCount++;
                Debug.Log("弾を1発獲得！ 残弾数: " + shotCount);
            }
            Destroy(other.gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shotCount--;
        Debug.Log("発射！ 残弾数: " + shotCount);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}