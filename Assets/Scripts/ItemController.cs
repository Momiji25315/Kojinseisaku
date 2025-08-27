using UnityEngine;

public class ItemController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float deadZone = -15f;

    // ItemSpawner���瑬�x��ݒ肵�Ă��炤���߂̐�p����
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    void Update()
    {
        if (transform.parent != null)
        {
            return;
        }

        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}