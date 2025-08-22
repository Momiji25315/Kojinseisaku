using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�W�����v�ݒ�")]
    public float jumpForce = 10f; // �W�����v�̋���

    [Header("�ڒn����")]
    public Transform groundCheck;      // �ڒn����̈ʒu���w�肷��I�u�W�F�N�g
    public float groundCheckRadius = 0.2f; // �ڒn����̉~�̔��a
    public LayerMask groundLayer;      // �u�n�ʁv�Ƃ݂Ȃ����C���[

    [Header("�A�C�e���ƒe")]
    public int itemsPerShot = 5;       // �e��1������̂ɕK�v�ȃA�C�e����
    public GameObject bulletPrefab;    // �e�̃v���n�u
    public Transform firePoint;        // �e�̔��ˈʒu

    // --- �����Ŏg���ϐ� ---
    private Rigidbody2D rb;
    private bool isGrounded;
    private int currentItemCount = 0;  // ���݂̃A�C�e�����W��
    private int shotCount = 0;         // ���ˉ\�Ȓe�̐�

    // �Q�[���J�n���Ɉ�x�����Ă΂��
    void Start()
    {
        // �������Z���������߂ɁA�����ɕt���Ă���Rigidbody2D�R���|�[�l���g���擾
        rb = GetComponent<Rigidbody2D>();
    }

    // ���t���[���Ă΂��
    void Update()
    {
        // --- �ڒn���� ---
        // GroundCheck�I�u�W�F�N�g�̈ʒu�ɉ~�����AGround���C���[�̃I�u�W�F�N�g�����邩�m�F����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- �W�����v���� ---
        // �����n�ʂɂ��āA���X�y�[�X�L�[�������ꂽ�u�Ԃ�
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // ������ɏu�ԓI�ȗ͂�������
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // --- �e�̔��ˏ��� ---
        // �������Ă�e��1���ȏ゠��A�����R���g���[���L�[�������ꂽ�u�Ԃ�
        if (shotCount > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    // IsTrigger��ON��Collider�ɐG�ꂽ���ɌĂ΂��
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �����G�ꂽ�I�u�W�F�N�g�̃^�O�� "Item" ��������
        if (other.CompareTag("Item"))
        {
            // �A�C�e���̎��W����1���₷
            currentItemCount++;
            // ���O���o���Ċm�F�i�Q�[�����s���AConsole�E�B���h�E�ɕ\������܂��j
            Debug.Log("�A�C�e���擾�I ���݂̃J�E���g: " + currentItemCount);

            // �������W�����A�e�����̂ɕK�v�Ȑ��ɒB������
            if (currentItemCount >= itemsPerShot)
            {
                currentItemCount = 0; // �J�E���g��0�ɖ߂�
                shotCount++;          // ���Ă�e�̐���1���₷
                Debug.Log("�e��1���l���I �c�e��: " + shotCount);
            }

            // �G�ꂽ�A�C�e���̃Q�[���I�u�W�F�N�g������
            Destroy(other.gameObject);
        }
    }

    // �e��������
    void Shoot()
    {
        // �e�̃v���n�u���AFirePoint�̈ʒu�Ɗp�x�Ő�������
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �e��1�������
        shotCount--;
        Debug.Log("���ˁI �c�e��: " + shotCount);
    }

    // Scene�r���[�Őڒn����͈̔͂����o�I�Ɋm�F���邽�߂̃R�[�h
    // �i�Q�[���̓���ɂ͉e�����܂���j
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}