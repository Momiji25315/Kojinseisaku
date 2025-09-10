using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // ���̎҂̑������o���Ă������߂̂���
    private float moveSpeed = 5f;

    // ���̎҂���������ׂ��A���E�̉ʂāiX���W�j
    private float deadZone = -30f; // �����L�߂Ɏ���Ă��������̂�

    // �O�����瑬���������邽�߂̗B��̑���
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    // ���t���[���A�u���̂��Ƃ��Ă΂�閽��
    void Update()
    {
        // �䂪�g�����ցA��߂�ꂽ�����œ�����
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // �����A���E�̉ʂĂ��z���Ă��܂����Ȃ�΁c
        // �i�����̎q���܂߂����W�Ŕ��f����K�v������j
        if (transform.position.x < deadZone)
        {
            // �c�䂪�g�����̐������������
            Destroy(gameObject);
        }
    }
}