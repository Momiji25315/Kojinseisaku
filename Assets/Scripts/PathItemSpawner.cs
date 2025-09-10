using System.Collections;
using UnityEngine;

public class PathItemSpawner : MonoBehaviour
{
    [Header("�������A�C�e���̐ݒ�")]
    // ������ ������ύX����I 1�ł͂Ȃ��A�����̃v���n�u���o������悤�ɂ��� ������
    public GameObject[] pathItemPrefabs; // ���Ƃ��ĕ��ׂ����A�C�e���̃v���n�u�i�����`�j

    public float itemSpacing = 1.5f;
    public float scrollSpeed = 5f;
    public float spawnY = -3.0f;
    public float spawnX = 12f;

    void Start()
    {
        StartCoroutine(SpawnItemPath());
    }

    private IEnumerator SpawnItemPath()
    {
        float waitTime = itemSpacing / scrollSpeed;

        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            // --- �A�C�e���̐��� ---

            // ������ �������炪�V�����V������I ������
            // �܂��A0����o�^���ꂽ�v���n�u�̐�-1�܂ł̊ԂŁA�����_���Ȑ�����I��
            int index = Random.Range(0, pathItemPrefabs.Length);

            // ���ɁA���̃����_���Ȑ������g���āA�v���n�u�̔z�񂩂�1�������ς�o��
            GameObject selectedPrefab = pathItemPrefabs[index];
            // ������ �����܂ł��� ������

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            // ������ �����āA�������I�񂾃v���n�u���g���ăA�C�e���𐶂ݏo���̂���I ������
            GameObject newItem = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            if (newItem.GetComponent<ItemController>() != null)
            {
                newItem.GetComponent<ItemController>().SetSpeed(this.scrollSpeed);
            }
        }
    }
}