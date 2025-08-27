using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float deadZone = -15f;

    // PlatformSpawner���瑬�x��ݒ肵�Ă��炤���߂̐�p����
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}