using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("�w�i�ݒ�")]
    public GameObject backgroundPrefab; // �w�i�̃v���n�u
    public float scrollSpeed = 2f;

    // --- �����ϐ� ---
    private List<GameObject> backgrounds = new List<GameObject>();
    private float backgroundWidth;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        backgroundWidth = backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        // �ŏ��ɉ�ʂ𖄂߂�̂ɕK�v�Ȑ��̔w�i��z�u����
        float screenWidth = (mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)) - mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0))).x;
        int initialCount = Mathf.CeilToInt(screenWidth / backgroundWidth) + 2; // ��ʕ�+2���Ŋm���ɖ��߂�

        for (int i = 0; i < initialCount; i++)
        {
            GameObject bg = Instantiate(backgroundPrefab, transform); // Manager�̎q�Ƃ��Đ���
            // X���W�����ԂȂ����ׂ�
            bg.transform.position = new Vector3(i * backgroundWidth, transform.position.y, transform.position.z);
            backgrounds.Add(bg);
        }
    }

    void Update()
    {
        // �Ǘ����Ă���S�Ă̔w�i�����ɓ�����
        foreach (GameObject bg in backgrounds)
        {
            bg.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        // ������ �������V�������[�v�̃��W�b�N�ł� ������
        // �ł����ɂ���w�i���擾
        GameObject leftmostBG = backgrounds[0];

        // �����A�ł����̔w�i�́u�E�[�v���A��ʂ́u���[�v�������ɍs������i�����S�Ɍ����Ȃ��Ȃ�����j
        if (leftmostBG.transform.position.x + backgroundWidth < mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            // �ł��E�ɂ���w�i��T��
            GameObject rightmostBG = backgrounds[backgrounds.Count - 1];

            // ��ʊO�ɏo�����[�̔w�i���A�E�[�̔w�i�̂���ɉE�ׂɏu�Ԉړ�������
            leftmostBG.transform.position = new Vector3(rightmostBG.transform.position.x + backgroundWidth, leftmostBG.transform.position.y, leftmostBG.transform.position.z);

            // �Ǘ����X�g�̒��ł��A�擪���疖���Ɉړ�������
            backgrounds.Remove(leftmostBG);
            backgrounds.Add(leftmostBG);
        }
    }
}