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
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // --- 接地判定 ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- アニメーション制御 ---
        // Rigidbodyの垂直方向の速度(rb.linearVelocity.y)を、Animatorの"velocityY"パラメータに渡す
        // これでUnity推奨の記法になります
        anim.SetFloat("velocityy", rb.linearVelocity.y);


        // --- 操作入力 ---
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (shotCount > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    // アイテム取得 と 奈落への落下 を検知する
    private void OnTriggerEnter2D(Collider2D other)
    {
        // アイテムに触れた場合
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

        // DeadZoneに触れた場合
        if (other.name == "DeadZone")
        {
            Debug.Log("ゲームオーバー！");
            Time.timeScale = 0; // ゲームを停止
        }
    }

    // 弾を発射する処理
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shotCount--;
        Debug.Log("発射！ 残弾数: " + shotCount);
    }

    // 敵との接触による勝利判定
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ゲームクリア！");
            Time.timeScale = 0; // ゲームを停止
        }
    }

    // Sceneビューで接地判定の範囲を見やすくするためのもの
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}