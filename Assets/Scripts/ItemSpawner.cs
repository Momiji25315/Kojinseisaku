using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("全般設定")]
    public GameObject[] itemPrefabs;
    public float itemMoveSpeed = 5f; // ← アイテムの速度はここで一括管理！

    [Header("足場上のアイテム設定")]
    [Range(0, 1)] public float chanceOnPlatform = 0.5f;

    [Header("床のアイテム設定")]
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

            // ItemControllerに速度を設定するよう命令する
            if (newItem.GetComponent<ItemController>() != null)
            {
                // ★★★ ここを変更 ★★★
                // .moveSpeed = ではなく、SetSpeed()メソッドを呼び出す形に変更
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
            Vector3 itemPosition = platform.transform.position + new Vector3(0, 0.7f, 0);

            GameObject newItem = Instantiate(selectedItemPrefab, itemPosition, Quaternion.identity);
            newItem.transform.SetParent(platform.transform);
        }
    }
}