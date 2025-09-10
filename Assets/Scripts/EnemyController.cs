using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("�_�ŕ\���̐ݒ�")]
    public float damageFlashTime = 0.5f;   // �_�ł��鍇�v����
    public Color damageColor = Color.white;  // �_�ł��鎞�̐F
    public float flashInterval = 0.05f;    // �_�ł̊Ԋu�i�b�j�B�������قǑ����I

    [Header("�����߂��i�m�b�N�o�b�N�j�̐ݒ�")]
    public float knockbackDistance = 0.5f; // 1��������̉����߂���鋗��

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        // ���g�̌����ڂ��i�� SpriteRenderer �������Ċo���Ă���
        spriteRenderer = GetComponent<SpriteRenderer>();
        // �ŏ��̐F�����X�̐F�Ƃ��Ċo���Ă���
        originalColor = spriteRenderer.color;
    }

    // �e����u�_���[�W���󂯂�v�Ɩ��߂��ꂽ���ɌĂ΂��
    public void TakeDamage()
    {
        // �܂��A�䂪�g�����։�����
        // �E�����iVector3.right�j�ցA�ݒ肵�����������ړ�����̂���
        transform.position += Vector3.right * knockbackDistance;

        // ���ɁA�_�ł̏p���r������
        // �A���ŏp���������Ă����������Ȃ�ʂ悤�A��x�Â��p�͒��f����
        StopAllCoroutines();
        StartCoroutine(DamageFlash());
    }

    // �_�ł̏p�̖{��
    private IEnumerator DamageFlash()
    {
        float timer = 0;

        // �w�肳�ꂽ���v���ԁA�_�ł��J��Ԃ�
        while (timer < damageFlashTime)
        {
            // �_���[�W�F�ɕς��� �� �����҂�
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashInterval);

            // ���̐F�ɖ߂� �� �����҂�
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashInterval);

            // �o�ߎ��Ԃ��v�Z
            timer += flashInterval * 2;
        }

        // �O�̂��߁A�Ō�ɕK�����̐F�ɖ߂��Ă���
        spriteRenderer.color = originalColor;
    }
}