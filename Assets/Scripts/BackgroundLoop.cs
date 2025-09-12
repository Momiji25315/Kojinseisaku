using UnityEngine; // ← この一行が、全てを解決する！

public class BackgroundLoop : MonoBehaviour
{
    public float speed = 1.0f;
    private float width;

    void Start()
    {
        // 背景画像の幅を取得
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // 背景を左に移動
        transform.position += Vector3.left * speed * Time.deltaTime;

        // 背景が一定位置まで移動したら右端に再配置
        if (transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2, 0, 0);
        }
    }
}