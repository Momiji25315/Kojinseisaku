using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ジャンプ設定")]
    public float jumpForce = 10f; // ジャンプの強さ

    [Header("接地判定")]
    public Transform groundCheck;      // 接地判定の位置を指定するオブジェクト
    public float groundCheckRadius = 0.2f; // 接地判定の円の半径
    public LayerMask groundLayer;      // 「地面」とみなすレイヤー

    [Header("アイテムと弾")]
    public int itemsPerShot = 5;       // 弾を1発得るのに必要なアイテム数
    public GameObject bulletPrefab;    // 弾のプレハブ
    public Transform firePoint;        // 弾の発射位置

    // --- 内部で使う変数 ---
    private Rigidbody2D rb;
    private bool isGrounded;
    private int currentItemCount = 0;  // 現在のアイテム収集数
    private int shotCount = 0;         // 発射可能な弾の数

    // ゲーム開始時に一度だけ呼ばれる
    void Start()
    {
        // 物理演算を扱うために、自分に付いているRigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        // --- 接地判定 ---
        // GroundCheckオブジェクトの位置に円を作り、Groundレイヤーのオブジェクトがあるか確認する
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- ジャンプ処理 ---
        // もし地面にいて、かつスペースキーが押された瞬間に
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // 上向きに瞬間的な力を加える
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // --- 弾の発射処理 ---
        // もし撃てる弾が1発以上あり、かつ左コントロールキーが押された瞬間に
        if (shotCount > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    // IsTriggerがONのColliderに触れた時に呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        // もし触れたオブジェクトのタグが "Item" だったら
        if (other.CompareTag("Item"))
        {
            // アイテムの収集数を1増やす
            currentItemCount++;
            // ログを出して確認（ゲーム実行中、Consoleウィンドウに表示されます）
            Debug.Log("アイテム取得！ 現在のカウント: " + currentItemCount);

            // もし収集数が、弾を撃つのに必要な数に達したら
            if (currentItemCount >= itemsPerShot)
            {
                currentItemCount = 0; // カウントを0に戻す
                shotCount++;          // 撃てる弾の数を1増やす
                Debug.Log("弾を1発獲得！ 残弾数: " + shotCount);
            }

            // 触れたアイテムのゲームオブジェクトを消す
            Destroy(other.gameObject);
        }
    }

    // 弾を撃つ処理
    void Shoot()
    {
        // 弾のプレハブを、FirePointの位置と角度で生成する
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 弾を1発消費する
        shotCount--;
        Debug.Log("発射！ 残弾数: " + shotCount);
    }

    // Sceneビューで接地判定の範囲を視覚的に確認するためのコード
    // （ゲームの動作には影響しません）
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}