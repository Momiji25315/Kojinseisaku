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
            // --- 足場の生成 ---
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // PlatformControllerに速度を設定するよう命令する
            newPlatform.GetComponent<PlatformController>().SetSpeed(this.platformMoveSpeed);

            // --- アイテム生成の通知 ---
            // もしItemSpawnerがインスペクターで設定されていたら、足場を生成したことを通知する
            if (itemSpawner != null)
            {
                // ItemSpawnerに、新しく作った足場の上へのアイテム生成を依頼する
                itemSpawner.SpawnItemOnPlatform(newPlatform);
            }

            // --- 次の生成までの待機 ---
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}