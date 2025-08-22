using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float speed = 1.0f;
    private float width;

    void Start()
    {
        // ”wŒi‰æ‘œ‚Ì•‚ğæ“¾
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // ”wŒi‚ğ¶‚ÉˆÚ“®
        transform.position += Vector3.left * speed * Time.deltaTime;

        // ”wŒi‚ªˆê’èˆÊ’u‚Ü‚ÅˆÚ“®‚µ‚½‚ç‰E’[‚ÉÄ”z’u
        if (transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2, 0, 0);
        }
    }
}