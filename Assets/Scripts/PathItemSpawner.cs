using System.Collections;
using UnityEngine;

public class PathItemSpawner : MonoBehaviour
{
    [Header("道を作るアイテムの設定")]
    // ★★★ ここを変更じゃ！ 1つではなく、複数のプレハブを覚えられるようにする ★★★
    public GameObject[] pathItemPrefabs; // 道として並べたいアイテムのプレハブ（複数形）

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

            // --- アイテムの生成 ---

            // ★★★ ここからが新しい儀式じゃ！ ★★★
            // まず、0から登録されたプレハブの数-1までの間で、ランダムな数字を選ぶ
            int index = Random.Range(0, pathItemPrefabs.Length);

            // 次に、そのランダムな数字を使って、プレハブの配列から1つを引っぱり出す
            GameObject selectedPrefab = pathItemPrefabs[index];
            // ★★★ ここまでじゃ ★★★

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            // ★★★ そして、さっき選んだプレハブを使ってアイテムを生み出すのじゃ！ ★★★
            GameObject newItem = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            if (newItem.GetComponent<ItemController>() != null)
            {
                newItem.GetComponent<ItemController>().SetSpeed(this.scrollSpeed);
            }
        }
    }
}