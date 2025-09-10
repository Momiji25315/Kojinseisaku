using System.Collections;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("生成する床")]
    public GameObject groundPrefab;

    [Header("生成設定")]
    public float scrollSpeed = 5f;
    public float spawnY = -4.5f;

    [Header("崖の生成設定")]
    public int minGroundLength = 3;
    public int maxGroundLength = 8;
    public int minCliffWidth = 2;
    public int maxCliffWidth = 4;

    private float groundWidth;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        groundWidth = groundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        // 儀式を開始する
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // まず、画面全体を埋めるように初期の床を配置する
        float startX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - groundWidth;
        float endX = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x + groundWidth;
        float currentX = startX;
        while (currentX < endX)
        {
            SpawnGround(currentX);
            currentX += groundWidth;
        }

        // ここからが本番じゃ。無限に床と崖を生み出し続ける
        while (true)
        {
            // まず、ランダムな数の床を連続で生み出す
            int groundCount = Random.Range(minGroundLength, maxGroundLength + 1);
            for (int i = 0; i < groundCount; i++)
            {
                // 床1枚分の時間が経過するのを待つ
                yield return new WaitForSeconds(groundWidth / scrollSpeed);
                // ★★★ 常に画面右端の外側に生成する！ ★★★
                SpawnGround(GetSpawnX());
            }

            // 次に、ランダムな幅の崖を作る
            int cliffCount = Random.Range(minCliffWidth, maxCliffWidth + 1);

            // 崖の幅の分だけ、何もしないで待つ
            yield return new WaitForSeconds((groundWidth * cliffCount) / scrollSpeed);
        }
    }

    // 常に画面右端の外側のX座標を計算して返す術
    float GetSpawnX()
    {
        return mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x + (groundWidth / 2);
    }

    // 床を1つ、指定したX座標に生成する専門の術
    void SpawnGround(float xPosition)
    {
        Vector3 spawnPosition = new Vector3(xPosition, spawnY, 0);
        GameObject newGround = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);

        // 生まれた床に、自分で動くための PlatformController を授け、速さを命じる
        if (newGround.GetComponent<PlatformController>() == null)
        {
            newGround.AddComponent<PlatformController>();
        }
        newGround.GetComponent<PlatformController>().SetSpeed(scrollSpeed);
    }
}