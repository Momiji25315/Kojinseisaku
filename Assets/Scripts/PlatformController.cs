using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // public ���O�����ƂŁA�C���X�y�N�^�[�ɕ\������Ȃ�����
    // ����ŁA���̕ϐ��͑��̃X�N���v�g���疽�߂��󂯎���p�ɂȂ�
    public float moveSpeed = 5f;

    private float deadZone = -15f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}