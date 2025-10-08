using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    [Header("地形パターン")]
    public GameObject[] terrainPatterns;
    public GameObject safePatternPrefab; // ★ 新機能：安全な地形「Template3」をここに設定 ★

    [Header("生成設定")]
    public float scrollSpeed = 5f;
    public float spawnY = 0f;
    public float safeZoneDuration = 2.0f; // ★ 新機能：安全地帯が続く秒数 ★

    // --- 内部変数 ---
    private List<GameObject> activePatterns = new List<GameObject>();
    private Camera mainCamera;
    private GameObject lastSpawnedPattern;
    private float timeSinceStart = 0f; // ゲーム開始からの経過時間
    private bool isSafeZone = true;    // 現在が安全地帯かどうか

    void Start()
    {
        mainCamera = Camera.main;

        // 最初のパターンとして、安全な地形を原点に配置
        GameObject initialPattern = Instantiate(safePatternPrefab, new Vector3(0, spawnY, 0), Quaternion.identity);
        activePatterns.Add(initialPattern);
        lastSpawnedPattern = initialPattern;
    }

    void Update()
    {
        // ゲーム開始からの経過時間を計測
        timeSinceStart += Time.deltaTime;

        // もし、安全地帯の時間が終了したら、フラグを切り替える
        if (isSafeZone && timeSinceStart > safeZoneDuration)
        {
            isSafeZone = false;
        }

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
            float rightEdge = GetPatternRightEdge(lastSpawnedPattern);
            if (rightEdge < mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x)
            {
                SpawnNextPattern();
            }
        }

        // 画面外に出た古いパターンを削除する処理も忘れずに追加しましょう
        // (この部分は別途追加が必要です)
    }

    void SpawnNextPattern()
    {
        GameObject selectedPatternPrefab;

        // ★★★ ここが新しいロジックです ★★★
        // もし、まだ安全地帯なら…
        if (isSafeZone)
        {
            // …強制的に安全なパターンを選ぶ
            selectedPatternPrefab = safePatternPrefab;
        }
        // そうでなければ…
        else
        {
            // …これまで通り、配列からランダムに選ぶ
            int randomIndex = Random.Range(0, terrainPatterns.Length);
            selectedPatternPrefab = terrainPatterns[randomIndex];
        }

        // 1. 最後に生成したパターンの「EndPoint」を探す
        Transform endPoint = lastSpawnedPattern.transform.Find("EndPoint");
        if (endPoint == null)
        {
            Debug.LogError("パターンに 'EndPoint' が見つかりません！: " + lastSpawnedPattern.name);
            return;
        }

        // 2. 選ばれたパターンを生成する
        GameObject newPattern = Instantiate(selectedPatternPrefab);

        // 3. 新しいパターンの左端を、前のパターンのEndPointにぴったり合わせる
        float newPatternWidth = GetPatternWidth(newPattern);
        float newPatternLeftOffset = newPatternWidth / 2;
        newPattern.transform.position = new Vector3(endPoint.position.x + newPatternLeftOffset, spawnY, 0);

        // 4. リストに追加し、「最後のパターン」として記憶する
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