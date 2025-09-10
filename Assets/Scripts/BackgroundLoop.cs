using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [Header("背景が流れる速さ")]
    public float scrollSpeed = 2f;

    private float backgroundWidth; // 背景1枚の横幅を覚えておくためのもの
    private Vector3 startPosition;   // この背景が生まれた場所を覚えておくためのもの

    void Start()
    {
        // 己が生まれた場所を記憶する
        startPosition = Vector3.zero;
        // 己の絵の横幅を計算して記憶する
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // ひたすらに左へ、定められた速さで動き続ける
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // ★★★ ここが術の心臓部じゃ！ ★★★
        // もし、生まれた場所から絵1枚分以上、左へ進んでしまったならば…
        if (transform.position.x < startPosition.x - backgroundWidth)
        {
            // …我が身を、絵3枚分、右へと瞬間移動させるのじゃ！
            // これにより、列の先頭から、最後尾へと回り込むことができる
            transform.position += Vector3.right * backgroundWidth * 3;
        }
    }
}