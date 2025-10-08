using UnityEngine;

public class CameraBreathing : MonoBehaviour
{
    [Header("�J�����̌ċz �ݒ�")]
    [Tooltip("�Y�[���̒��S�ƂȂ�J�����̃T�C�Y")]
    public float baseSize = 5f;

    [Tooltip("�Y�[���C���E�Y�[���A�E�g���镝�̑傫��")]
    public float zoomAmount = 0.5f;

    [Tooltip("�Y�[����1��������̂ɂ����鎞�ԁi�b�j")]
    public float zoomSpeed = 4.0f;

    // --- �����ϐ� ---
    private Camera mainCamera;
    private float initialZ; // Z���W���Œ肷�邽�߂̕ϐ�

    void Start()
    {
        // ���̃X�N���v�g���A�^�b�`����Ă���J�������擾
        mainCamera = GetComponent<Camera>();

        // �ŏ���Z���W���L�����Ă����i2D�ł͒ʏ�-10�j
        initialZ = transform.position.z;
    }

    void Update()
    {
        // --- �J�����̃T�C�Y�i�Y�[���j�����炩�ɕω������� ---

        // Time.time ���g���āA���ԂƋ��ɕω�����l�����
        // Math.Sin() �́A-1 ���� 1 �͈̔͂����炩�ɍs��������g�����
        float sinValue = Mathf.Sin(Time.time * (2 * Mathf.PI) / zoomSpeed);

        // Sin�g�̌��ʁi-1�`1�j���A�Y�[���̕��ɍ��킹�Ē�������
        float zoomOffset = sinValue * zoomAmount;

        // ��{�ƂȂ�T�C�Y�ɁA�v�Z�����I�t�Z�b�g��������
        mainCamera.orthographicSize = baseSize + zoomOffset;


        // (���܂�) �����AZ���W���킸���ɑO��ɓ����������ꍇ�́A�ȉ��̃R�����g���O���܂�
        /*
        float zOffset = sinValue * (zoomAmount / 5f); // �Y�[���ʂɉ�����Z������������
        transform.position = new Vector3(transform.position.x, transform.position.y, initialZ + zOffset);
        */
    }
}