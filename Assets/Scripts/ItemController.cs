using UnityEngine;

public class ItemController : MonoBehaviour
{
    // private �ɕύX���A�C���X�y�N�^�[���猩���Ȃ�����
    private float moveSpeed = 5f;
    private float deadZone = -15f;

    // ItemSpawner���瑬�x��ݒ肵�Ă��炤���߂̐�p�����i���\�b�h�j
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    void Update()
    {
        // �����e�I�u�W�F�N�g������ꍇ�i����̏�ɏ���Ă���ꍇ�j�͉������Ȃ�
        if (transform.parent != null)
        {
            return;
        }

        // �e�����Ȃ��i�n�ʂɒ��ڐ������ꂽ�j�ꍇ�̂݁A�����œ���
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}