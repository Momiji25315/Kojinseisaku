using UnityEngine; // �� ���̈�s���A�S�Ă���������I

public class BackgroundLoop : MonoBehaviour
{
    public float speed = 1.0f;
    private float width;

    void Start()
    {
        // �w�i�摜�̕����擾
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // �w�i�����Ɉړ�
        transform.position += Vector3.left * speed * Time.deltaTime;

        // �w�i�����ʒu�܂ňړ�������E�[�ɍĔz�u
        if (transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2, 0, 0);
        }
    }
}