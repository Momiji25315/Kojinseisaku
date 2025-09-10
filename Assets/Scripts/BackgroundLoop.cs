using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [Header("�w�i������鑬��")]
    public float scrollSpeed = 2f;

    private float backgroundWidth; // �w�i1���̉������o���Ă������߂̂���
    private Vector3 startPosition;   // ���̔w�i�����܂ꂽ�ꏊ���o���Ă������߂̂���

    void Start()
    {
        // �Ȃ����܂ꂽ�ꏊ���L������
        startPosition = Vector3.zero;
        // �Ȃ̊G�̉������v�Z���ċL������
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // �Ђ�����ɍ��ցA��߂�ꂽ�����œ���������
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // ������ �������p�̐S��������I ������
        // �����A���܂ꂽ�ꏊ����G1�����ȏ�A���֐i��ł��܂����Ȃ�΁c
        if (transform.position.x < startPosition.x - backgroundWidth)
        {
            // �c�䂪�g���A�G3�����A�E�ւƏu�Ԉړ�������̂���I
            // ����ɂ��A��̐擪����A�Ō���ւƉ�荞�ނ��Ƃ��ł���
            transform.position += Vector3.right * backgroundWidth * 3;
        }
    }
}