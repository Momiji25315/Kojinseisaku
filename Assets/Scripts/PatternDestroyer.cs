using UnityEngine;

public class PatternDestroyer : MonoBehaviour
{
    // ���̃X�N���v�g�́A�����̃g���K�[�ɐG�ꂽ���Ɏ����ŌĂ΂��
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �����A�G�ꂽ����̃^�O�� "DeadZone" ��������c
        // (�����S�ɂ���Ȃ�Aother.CompareTag("DeadZone") ���g���܂�)
        if (other.name == "DeadZone")
        {
            // �������g�i���̃X�N���v�g���t���Ă���GameObject�j��j�󂷂�
            Destroy(gameObject);
        }
    }
}