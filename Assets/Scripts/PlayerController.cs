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

    // --- �����Ŏg���ϐ� ---
    private Rigidbody2D rb;
    private bool isGrounded;
    private int currentItemCount = 0;
    private int shotCount = 0;

    // ������ �A�j���[�V�����Đ����u�𑀂邽�߂̕ϐ���ǉ����� ������
    private Animator anim;

    // �Q�[���J�n���Ɉ�x�����Ă΂��
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ������ �������g�ɕt���Ă��� Animator �������āA�ϐ��Ɋo���Ă����̂��� ������
        anim = GetComponent<Animator>();
    }

    // ���t���[���Ă΂��
    void Update()
    {
        // �܂��A�n�ʂɂ��邩�ǂ����𔻒f����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ������ �������炪�V�������@���� ������
        // �����n�ʂɂ���Ȃ�΁c
        if (isGrounded)
        {
            // �c�A�j���[�V�����̎��Ԃ�ʏ�̑��x�Ői�߂� (1���ʏ푬�x)
            anim.speed = 1;
        }
        // �����łȂ���� (�܂�A�󒆂ɂ���Ȃ��) �c
        else
        {
            // �c�A�j���[�V�����̎��Ԃ��~�߂Ă��܂��̂���I (0�Œ�~)
            anim.speed = 0;
        }
        // ������ �����܂ł��� ������


        // --- �W�����v���� ---
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // --- �e�̔��ˏ��� ---
        if (shotCount > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    // (�����艺�� OnTriggerEnter2D �� Shoot ���\�b�h�Ȃǂ͕ύX�Ȃ�����)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
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
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shotCount--;
        Debug.Log("���ˁI �c�e��: " + shotCount);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}