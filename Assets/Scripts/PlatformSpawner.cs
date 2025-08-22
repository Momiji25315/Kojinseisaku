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

    [Header("�A�C�e������")]
    public GameObject[] itemPrefabs;
    [Range(0, 1)] public float itemSpawnChance = 0.5f;

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
            newPlatform.GetComponent<PlatformController>().moveSpeed = this.platformMoveSpeed;

            // --- �A�C�e���̐����i�m���Ŏ��s�j ---
            if (Random.value < itemSpawnChance)
            {
                int index = Random.Range(0, itemPrefabs.Length);
                GameObject selectedItemPrefab = itemPrefabs[index];
                Vector3 itemPosition = newPlatform.transform.position + new Vector3(0, 0.7f, 0);

                // �A�C�e���𐶐����A���̏��� newItem �ϐ��Ɋi�[����
                GameObject newItem = Instantiate(selectedItemPrefab, itemPosition, Quaternion.identity);

                // ����������������������������������������������������������������������
                // �������d�v�Ȓǉ��s�ł��I
                // ���������A�C�e���inewItem�j���A����inewPlatform�j�̎q�I�u�W�F�N�g�ɐݒ肷��
                newItem.transform.SetParent(newPlatform.transform);
                // ����������������������������������������������������������������������
            }

            // --- ���̐����܂ł̑ҋ@ ---
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}