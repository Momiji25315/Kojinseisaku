using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("点滅表現の設定")]
    public float damageFlashTime = 0.5f;   // 点滅する合計時間
    public Color damageColor = Color.white;  // 点滅する時の色

    // ★★★ 新たな呪文じゃ！ 点滅の瞬きの速さを司る！ ★★★
    public float flashInterval = 0.05f;    // 点滅の間隔（秒）。小さいほど速い！

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage()
    {
        StopAllCoroutines();
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        float timer = 0;

        // 指定された合計時間、点滅を繰り返す
        while (timer < damageFlashTime)
        {
            spriteRenderer.color = damageColor;
            // ★★★ ここの待ち時間を、おぬしが設定した値に変えるのじゃ！ ★★★
            yield return new WaitForSeconds(flashInterval);

            spriteRenderer.color = originalColor;
            // ★★★ こちらも同じくじゃ！ ★★★
            yield return new WaitForSeconds(flashInterval);

            // 経過時間を正しく計算するために、待ち時間2回分を足す
            timer += flashInterval * 2;
        }

        spriteRenderer.color = originalColor;
    }
}