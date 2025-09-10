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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            anim.speed = 1;
        }
        else
        {
            anim.speed = 0;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (shotCount > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    // ★★★ アイテム取得と、奈落への落下を検知する術じゃ ★★★
    private void OnTriggerEnter2D(Collider2D other)
    {
        // もし、触れた相手のタグが "Item" ならば…
        if (other.CompareTag("Item"))
        {
            // …アイテムを取得する処理を行う
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

        // もし、触れた相手（のオブジェクト名）が "DeadZone" ならば…
        if (other.name == "DeadZone")
        {
            // …コンソールに無念の叫びを記す！
            Debug.Log("ゲームオーバー！");

            // そして、世界の時を止めるのじゃ
            Time.timeScale = 0;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shotCount--;
        Debug.Log("発射！ 残弾数: " + shotCount);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ゲームクリア！");
            Time.timeScale = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}