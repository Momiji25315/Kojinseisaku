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
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // --- �ڒn���� ---
        // ���t���[���A�����ɒn�ʂ����邩�ǂ������m�F����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- �A�j���[�V�������� ---
        // isGrounded �̏�Ԃ�Animator�ɓ`����
        // isGrounded �� false �̂Ƃ��i���󒆂ɂ���Ƃ��j�A"isJumping" �p�����[�^�� true �ɂ���
        // isGrounded �� true �̂Ƃ��i���n�ʂɂ���Ƃ��j�A"isJumping" �p�����[�^�� false �ɂ���
        anim.SetBool("isJumping", !isGrounded);

        // --- ������� ---
        // �����n�ʂɂ��āA���X�y�[�X�L�[�������ꂽ�u�Ԃ�
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // �W�����v����
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // �������Ă�e��1���ȏ゠��A�����R���g���[���L�[�������ꂽ�u�Ԃ�
        if (shotCount > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            // �e������
            Shoot();
        }
    }

    // �A�C�e���擾 �� �ޗ��ւ̗��� �����m����
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �A�C�e���ɐG�ꂽ�ꍇ
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

        // DeadZone�ɐG�ꂽ�ꍇ
        if (other.name == "DeadZone")
        {
            Debug.Log("�Q�[���I�[�o�[�I");
            Time.timeScale = 0; // �Q�[�����~
        }
    }

    // �e�𔭎˂��鏈��
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shotCount--;
        Debug.Log("���ˁI �c�e��: " + shotCount);
    }

    // �G�Ƃ̐ڐG�ɂ�鏟������
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("�Q�[���N���A�I");
            Time.timeScale = 0; // �Q�[�����~
        }
    }

    // Scene�r���[�Őڒn����͈̔͂����₷�����邽�߂̂���
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}