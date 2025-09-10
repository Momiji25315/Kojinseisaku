using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�W�����v�ݒ�")]
    public float jumpForce = 10f;

    [Header("�ڒn����")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("�A�C�e���ƒe")]
    public int itemsPerShot = 5;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int currentItemCount = 0;
    private int shotCount = 0;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            anim.speed = 1;
        }
        else
        {
            anim.speed = 0;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (shotCount > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    // ������ �A�C�e���擾�ƁA�ޗ��ւ̗��������m����p���� ������
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �����A�G�ꂽ����̃^�O�� "Item" �Ȃ�΁c
        if (other.CompareTag("Item"))
        {
            // �c�A�C�e�����擾���鏈�����s��
            currentItemCount++;
            Debug.Log("�A�C�e���擾�I ���݂̃J�E���g: " + currentItemCount);

            if (currentItemCount >= itemsPerShot)
            {
                currentItemCount = 0;
                shotCount++;
                Debug.Log("�e��1���l���I �c�e��: " + shotCount);
            }
            Destroy(other.gameObject);
        }

        // �����A�G�ꂽ����i�̃I�u�W�F�N�g���j�� "DeadZone" �Ȃ�΁c
        if (other.name == "DeadZone")
        {
            // �c�R���\�[���ɖ��O�̋��т��L���I
            Debug.Log("�Q�[���I�[�o�[�I");

            // �����āA���E�̎����~�߂�̂���
            Time.timeScale = 0;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shotCount--;
        Debug.Log("���ˁI �c�e��: " + shotCount);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("�Q�[���N���A�I");
            Time.timeScale = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}