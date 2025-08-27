using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("�_�ŕ\���̐ݒ�")]
    public float damageFlashTime = 0.5f;   // �_�ł��鍇�v����
    public Color damageColor = Color.white;  // �_�ł��鎞�̐F

    // ������ �V���Ȏ�������I �_�ł̏u���̑������i��I ������
    public float flashInterval = 0.05f;    // �_�ł̊Ԋu�i�b�j�B�������قǑ����I

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage()
    {
        StopAllCoroutines();
        StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        float timer = 0;

        // �w�肳�ꂽ���v���ԁA�_�ł��J��Ԃ�
        while (timer < damageFlashTime)
        {
            spriteRenderer.color = damageColor;
            // ������ �����̑҂����Ԃ��A���ʂ����ݒ肵���l�ɕς���̂���I ������
            yield return new WaitForSeconds(flashInterval);

            spriteRenderer.color = originalColor;
            // ������ �����������������I ������
            yield return new WaitForSeconds(flashInterval);

            // �o�ߎ��Ԃ𐳂����v�Z���邽�߂ɁA�҂�����2�񕪂𑫂�
            timer += flashInterval * 2;
        }

        spriteRenderer.color = originalColor;
    }
}