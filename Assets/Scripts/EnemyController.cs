using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("点滅表現の設定")]
    public float damageFlashTime = 0.5f;   // 点滅する合計時間
    public Color damageColor = Color.white;  // 点滅する時の色
    public float flashInterval = 0.05f;    // 点滅の間隔（秒）。小さいほど速い！

    [Header("押し戻し（ノックバック）の設定")]
    public float knockbackDistance = 0.5f; // 1発当たりの押し戻される距離

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        // 自身の見た目を司る SpriteRenderer を見つけて覚えておく
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 最初の色を元々の色として覚えておく
        originalColor = spriteRenderer.color;
    }

    // 弾から「ダメージを受けよ」と命令された時に呼ばれる
    public void TakeDamage()
    {
        // まず、我が身を後ろへ下げる
        // 右方向（Vector3.right）へ、設定した距離だけ移動するのじゃ
        transform.position += Vector3.right * knockbackDistance;

        // 次に、点滅の術を詠唱する
        // 連続で術が発動してもおかしくならぬよう、一度古い術は中断する
        StopAllCoroutines();
        StartCoroutine(DamageFlash());
    }

    // 点滅の術の本体
    private IEnumerator DamageFlash()
    {
        float timer = 0;

        // 指定された合計時間、点滅を繰り返す
        while (timer < damageFlashTime)
        {
            // ダメージ色に変える → 少し待つ
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashInterval);

            // 元の色に戻す → 少し待つ
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashInterval);

            // 経過時間を計算
            timer += flashInterval * 2;
        }

        // 念のため、最後に必ず元の色に戻しておく
        spriteRenderer.color = originalColor;
    }
}