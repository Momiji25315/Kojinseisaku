using System.Collections;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("�������鏰")]
    public GameObject groundPrefab;

    [Header("�����ݒ�")]
    public float scrollSpeed = 5f;
    public float spawnY = -4.5f;

    [Header("�R�̐����ݒ�")]
    public int minGroundLength = 3;
    public int maxGroundLength = 8;
    public int minCliffWidth = 2;
    public int maxCliffWidth = 4;

    private float groundWidth;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        groundWidth = groundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        // �V�����J�n����
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // �܂��A��ʑS�̂𖄂߂�悤�ɏ����̏���z�u����
        float startX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - groundWidth;
        float endX = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x + groundWidth;
        float currentX = startX;
        while (currentX < endX)
        {
            SpawnGround(currentX);
            currentX += groundWidth;
        }

        // �������炪�{�Ԃ���B�����ɏ��ƊR�𐶂ݏo��������
        while (true)
        {
            // �܂��A�����_���Ȑ��̏���A���Ő��ݏo��
            int groundCount = Random.Range(minGroundLength, maxGroundLength + 1);
            for (int i = 0; i < groundCount; i++)
            {
                // ��1�����̎��Ԃ��o�߂���̂�҂�
                yield return new WaitForSeconds(groundWidth / scrollSpeed);
                // ������ ��ɉ�ʉE�[�̊O���ɐ�������I ������
                SpawnGround(GetSpawnX());
            }

            // ���ɁA�����_���ȕ��̊R�����
            int cliffCount = Random.Range(minCliffWidth, maxCliffWidth + 1);

            // �R�̕��̕������A�������Ȃ��ő҂�
            yield return new WaitForSeconds((groundWidth * cliffCount) / scrollSpeed);
        }
    }

    // ��ɉ�ʉE�[�̊O����X���W���v�Z���ĕԂ��p
    float GetSpawnX()
    {
        return mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x + (groundWidth / 2);
    }

    // ����1�A�w�肵��X���W�ɐ���������̏p
    void SpawnGround(float xPosition)
    {
        Vector3 spawnPosition = new Vector3(xPosition, spawnY, 0);
        GameObject newGround = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);

        // ���܂ꂽ���ɁA�����œ������߂� PlatformController �������A�����𖽂���
        if (newGround.GetComponent<PlatformController>() == null)
        {
            newGround.AddComponent<PlatformController>();
        }
        newGround.GetComponent<PlatformController>().SetSpeed(scrollSpeed);
    }
}