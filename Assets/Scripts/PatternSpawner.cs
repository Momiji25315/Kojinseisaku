using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    [Header("地形パターン")]
    public GameObject[] terrainPatterns;

    [Header("生成設定")]
    public float scrollSpeed = 5f;
    public float spawnY = 0f;

    // --- 内部変数 ---
    private List<GameObject> activePatterns = new List<GameObject>();
    private Camera mainCamera;
    private GameObject lastSpawnedPattern; // 最後に生成したパターンを覚えておく

    void Start()
    {
        mainCamera = Camera.main;

        // 最初のパターンを原点に配置
        GameObject initialPattern = Instantiate(terrainPatterns[0], new Vector3(0, spawnY, 0), Quaternion.identity);
        activePatterns.Add(initialPattern);
        lastSpawnedPattern = initialPattern;
    }

    void Update()
    {
        // 全てのパターンを左に動かす
        foreach (GameObject pattern in activePatterns)
        {
            if (pattern != null)
            {
                pattern.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
            }
        }

        // 最後に生成したパターンの右端が画面内に入ってきたら、次のパターンを生成する
        if (lastSpawnedPattern != null)
        {
            // パターンの右端の位置を取得
            float rightEdge = GetPatternRightEdge(lastSpawnedPattern);
            if (rightEdge < mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x)
            {
                SpawnNextPattern();
            }
        }

        // 画面外に出た古いパターンを削除する
        // (省略... 前回のコードと同じで構いません)
    }

    void SpawnNextPattern()
    {
        // どのパターンを出すかランダムに選ぶ
        int randomIndex = Random.Range(0, terrainPatterns.Length);
        GameObject selectedPatternPrefab = terrainPatterns[randomIndex];

        // ★★★ ここが新しい連結ロジックです ★★★
        // 1. 最後に生成したパターンの「EndPoint」を探す
        Transform endPoint = lastSpawnedPattern.transform.Find("EndPoint");
        if (endPoint == null)
        {
            Debug.LogError("パターンに 'EndPoint' が見つかりません！: " + lastSpawnedPattern.name);
            return;
        }

        // 2. 新しいパターンを生成する
        GameObject newPattern = Instantiate(selectedPatternPrefab);

        // 3. 新しいパターンの左端を、前のパターンのEndPointにぴったり合わせる
        //    (新しいパターンの幅の半分を考慮します)
        float newPatternWidth = GetPatternWidth(newPattern);
        float newPatternLeftOffset = newPatternWidth / 2;
        newPattern.transform.position = new Vector3(endPoint.position.x + newPatternLeftOffset, spawnY, 0);

        // 4. 新しいパターンをリストに追加し、「最後のパターン」として記憶する
        activePatterns.Add(newPattern);
        lastSpawnedPattern = newPattern;
    }

    // パターンの幅をColliderから取得
    float GetPatternWidth(GameObject pattern)
    {
        BoxCollider2D collider = pattern.GetComponent<BoxCollider2D>();
        if (collider != null) return collider.size.x * pattern.transform.localScale.x;
        Debug.LogWarning("幅を測るためのBoxCollider2Dがありません: " + pattern.name);
        return 10f;
    }

    // パターンのワールド座標での右端を取得
    float GetPatternRightEdge(GameObject pattern)
    {
        return pattern.transform.position.x + GetPatternWidth(pattern) / 2;
    }
}