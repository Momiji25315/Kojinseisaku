using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 15f;    // �e�̑��x
    public float lifeTime = 3f;  // �e�������ŏ�����܂ł̎���

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ���˂��ꂽ�u�ԂɁA�E�����֗͂�������
        rb.linearVelocity = transform.right * speed;

        // lifeTime�b��ɁA���̒e�I�u�W�F�N�g��j������
        Destroy(gameObject, lifeTime);
    }
}