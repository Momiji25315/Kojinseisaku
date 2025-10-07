using UnityEngine;

public class PatternDestroyer : MonoBehaviour
{
    // このスクリプトは、何かのトリガーに触れた時に自動で呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        // もし、触れた相手のタグが "DeadZone" だったら…
        // (より安全にするなら、other.CompareTag("DeadZone") を使います)
        if (other.name == "DeadZone")
        {
            // 自分自身（このスクリプトが付いているGameObject）を破壊する
            Destroy(gameObject);
        }
    }
}