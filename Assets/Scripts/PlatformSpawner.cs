using System.Collections;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("生成する足場")]
    public GameObject platformPrefab;

    [Header("生成設定")]
    public float spawnY = -2.5f;
    public float spawnX = 12f;

    [Header("生成間隔（秒）")]
    public float minSpawnInterval = 1.0f;
    public float maxSpawnInterval = 2.5f;

    [Header("足場の速度")]
    public float platformMoveSpeed = 5f;

    [Header("連携")]
    public ItemSpawner itemSpawner; // アイテム生成装置への参照

    void Start()
    {
        StartCoroutine(SpawnPlatforms());
    }

    private IEnumerator SpawnPlatforms()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            newPlatform.GetComponent<PlatformController>().SetSpeed(this.platformMoveSpeed);

            if (itemSpawner != null)
            {
                itemSpawner.SpawnItemOnPlatform(newPlatform);
            }

            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}