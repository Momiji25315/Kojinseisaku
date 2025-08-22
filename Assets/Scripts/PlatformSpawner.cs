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

    [Header("アイテム生成")]
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
            // --- 足場の生成 ---
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            newPlatform.GetComponent<PlatformController>().moveSpeed = this.platformMoveSpeed;

            // --- アイテムの生成（確率で実行） ---
            if (Random.value < itemSpawnChance)
            {
                int index = Random.Range(0, itemPrefabs.Length);
                GameObject selectedItemPrefab = itemPrefabs[index];
                Vector3 itemPosition = newPlatform.transform.position + new Vector3(0, 0.7f, 0);

                // アイテムを生成し、その情報を newItem 変数に格納する
                GameObject newItem = Instantiate(selectedItemPrefab, itemPosition, Quaternion.identity);

                // ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
                // ここが重要な追加行です！
                // 生成したアイテム（newItem）を、足場（newPlatform）の子オブジェクトに設定する
                newItem.transform.SetParent(newPlatform.transform);
                // ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
            }

            // --- 次の生成までの待機 ---
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}