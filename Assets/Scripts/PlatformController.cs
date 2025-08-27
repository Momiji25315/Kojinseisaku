using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float deadZone = -15f;

    // PlatformSpawner‚©‚ç‘¬“x‚ğİ’è‚µ‚Ä‚à‚ç‚¤‚½‚ß‚Ìê—p“üŒû
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