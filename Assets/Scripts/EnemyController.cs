using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("AIジャンプ設定")]
    public float jumpCheckDistance = 3f;
    public float jumpRayAngle = -45f;
    public float jumpCooldown = 0.5f;

    [Header("ジャンプ設定")]
    public float jumpForce = 20f;
    public Transform cliffCheck;
    public LayerMask groundLayer;

    [Header("点滅表現の設定")]
    public float damageFlashTime = 0.5f;
    public Color damageColor = Color.white;
    public float flashInterval = 0.05f;

    [Header("押し戻し（ノックバック）の設定")]
    public float knockbackDistance = 0.5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine damageFlashCoroutine;
    private bool isGroundedOnRail = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // 導きのレール（トリガー）を自動で追加
        BoxCollider2D railCollider = gameObject.AddComponent<BoxCollider2D>();
        railCollider.isTrigger = true;
        BoxCollider2D mainCollider = GetComponent<BoxCollider2D>();
        if (mainCollider != null)
        {
            railCollider.size = new Vector2(mainCollider.size.x + 0.1f, 0.1f);
            railCollider.offset = new Vector2(mainCollider.offset.x, mainCollider.offset.y - mainCollider.size.y / 2);
        }
    }

    void FixedUpdate()
    {
        if (isGroundedOnRail)
        {
            RaycastHit2D groundAheadInfo = Physics2D.Raycast(cliffCheck.position, Vector2.down, 1f, groundLayer);
            if (groundAheadInfo.collider == null)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, jumpRayAngle);
                Vector2 direction = rotation * -transform.right;
                RaycastHit2D landingSpotInfo = Physics2D.Raycast(cliffCheck.position, direction, jumpCheckDistance, groundLayer);

                if (landingSpotInfo.collider != null)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGroundedOnRail = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGroundedOnRail = false;
        }
    }

    // ★★★ この TakeDamage メソッドが重要です ★★★
    public void TakeDamage()
    {
        // ノックバック
        transform.position += -Vector3.right * knockbackDistance;

        // 点滅処理の管理
        if (damageFlashCoroutine != null)
        {
            StopCoroutine(damageFlashCoroutine);
        }
        damageFlashCoroutine = StartCoroutine(DamageFlash());
    }

    // ★★★ この DamageFlash メソッドも必要です ★★★
    private IEnumerator DamageFlash()
    {
        float timer = 0;
        while (timer < damageFlashTime)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashInterval);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval * 2;
        }
        spriteRenderer.color = originalColor;
    }

    private void OnDrawGizmosSelected()
    {
        if (cliffCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(cliffCheck.position, Vector2.down * 1f);

        Quaternion rotation = Quaternion.Euler(0, 0, jumpRayAngle);
        Vector2 direction = rotation * -transform.right;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(cliffCheck.position, direction * jumpCheckDistance);
    }
}