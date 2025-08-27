using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("�e�̊�{�ݒ�")]
    public float speed = 15f;    // �e�̑���
    public float lifeTime = 3f;  // �e������ɏ�����܂ł̎���

    private Rigidbody2D rb;

    // �e�����܂ꂽ�u�ԂɈ�x�����Ă΂�閽��
    void Start()
    {
        // �������Z�̕�ʁiRigidbody2D�j�������Ċo���Ă���
        rb = GetComponent<Rigidbody2D>();
        // �O���i�E�����j�ɁA�ݒ肳�ꂽ�����ŗ͂�^����
        rb.linearVelocity = transform.right * speed;
        // lifeTime�b��ɁA�����I�ɉ䂪�g����������\�������
        Destroy(gameObject, lifeTime);
    }

    // ������ Trigger�iIs Trigger��ON�j�̓����蔻��ɐG�ꂽ���ɌĂ΂�閽�� ������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                // ������ �`�ߔ����������ǉ�����I ������
                Debug.Log("�G�ɖ����I �_�ł̏p�A�����𖽂��I");
                enemy.TakeDamage();
            }
            Destroy(gameObject);
        }
    }
}