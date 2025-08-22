using System.Collections;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("�������鑫��")]
    public GameObject platformPrefab;

    [Header("�����ݒ�")]
    public float spawnY = -2.5f;
    public float spawnX = 12f;

    [Header("�����Ԋu�i�b�j")]
    public float minSpawnInterval = 1.0f;
    public float maxSpawnInterval = 2.5f;

    [Header("����̑��x")]
    public float platformMoveSpeed = 5f;

    [Header("�A�g")]
    public ItemSpawner itemSpawner; // �A�C�e���������u�ւ̎Q��

    void Start()
    {
        StartCoroutine(SpawnPlatforms());
    }

    private IEnumerator SpawnPlatforms()
    {
        while (true)
        {
            // --- ����̐��� ---
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // PlatformController�ɑ��x��ݒ肷��悤���߂���
            newPlatform.GetComponent<PlatformController>().SetSpeed(this.platformMoveSpeed);

            // --- �A�C�e�������̒ʒm ---
            // ����ItemSpawner���C���X�y�N�^�[�Őݒ肳��Ă�����A����𐶐��������Ƃ�ʒm����
            if (itemSpawner != null)
            {
                // ItemSpawner�ɁA�V�������������̏�ւ̃A�C�e���������˗�����
                itemSpawner.SpawnItemOnPlatform(newPlatform);
            }

            // --- ���̐����܂ł̑ҋ@ ---
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}