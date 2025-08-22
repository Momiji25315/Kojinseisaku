using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // public を外すことで、インスペクターに表示されなくする
    // これで、この変数は他のスクリプトから命令を受け取る専用になる
    public float moveSpeed = 5f;

    private float deadZone = -15f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}