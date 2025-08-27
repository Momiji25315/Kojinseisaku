using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("�S�ʐݒ�")]
    public GameObject[] itemPrefabs;
    public float itemMoveSpeed = 5f;

    [Header("�����̃A�C�e���ݒ�")]
    [Range(0, 1)] public float chanceOnPlatform = 0.5f;

    [Header("���̃A�C�e���ݒ�")]
    public float floorSpawnY = -4.0f;
    public float floorSpawnX = 12f;
    public float minFloorInterval = 2.0f;
    public float maxFloorInterval = 5.0f;

    void Start()
    {
        StartCoroutine(SpawnFloorItems());
    }

    private IEnumerator SpawnFloorItems()
    {
        while (true)
        {
            float waitTime = Random.Range(minFloorInterval, maxFloorInterval);
            yield return new WaitForSeconds(waitTime);

            int index = Random.Range(0, itemPrefabs.Length);
            GameObject selectedItemPrefab = itemPrefabs[index];
            Vector3 spawnPosition = new Vector3(floorSpawnX, floorSpawnY, 0);

            GameObject newItem = Instantiate(selectedItemPrefab, spawnPosition, Quaternion.identity);

            if (newItem.GetComponent<ItemController>() != null)
            {
                newItem.GetComponent<ItemController>().SetSpeed(this.itemMoveSpeed);
            }
        }
    }

    public void SpawnItemOnPlatform(GameObject platform)
    {
        if (Random.value < chanceOnPlatform)
        {
            int index = Random.Range(0, itemPrefabs.Length);
            GameObject selectedItemPrefab = itemPrefabs[index];
            Vector3 itemPosition = platform.transform.position + new Vector3(0, 1.1f, 0);

            GameObject newItem = Instantiate(selectedItemPrefab, itemPosition, Quaternion.identity);
            newItem.transform.SetParent(platform.transform);
        }
    }
}