using UnityEngine;

public class ItemController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float deadZone = -15f;

    // ItemSpawner‚©‚ç‘¬“x‚ğİ’è‚µ‚Ä‚à‚ç‚¤‚½‚ß‚Ìê—p“üŒû
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