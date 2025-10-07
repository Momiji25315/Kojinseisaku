using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("背景設定")]
    public GameObject backgroundPrefab; // 背景のプレハブ
    public float scrollSpeed = 2f;

    // --- 内部変数 ---
    private List<GameObject> backgrounds = new List<GameObject>();
    private float backgroundWidth;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        backgroundWidth = backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        // 最初に画面を埋めるのに必要な数の背景を配置する
        float screenWidth = (mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)) - mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0))).x;
        int initialCount = Mathf.CeilToInt(screenWidth / backgroundWidth) + 2; // 画面幅+2枚で確実に埋める

        for (int i = 0; i < initialCount; i++)
        {
            GameObject bg = Instantiate(backgroundPrefab, transform); // Managerの子として生成
            // X座標を隙間なく並べる
            bg.transform.position = new Vector3(i * backgroundWidth, transform.position.y, transform.position.z);
            backgrounds.Add(bg);
        }
    }

    void Update()
    {
        // 管理している全ての背景を左に動かす
        foreach (GameObject bg in backgrounds)
        {
            bg.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        // ★★★ ここが新しいループのロジックです ★★★
        // 最も左にある背景を取得
        GameObject leftmostBG = backgrounds[0];

        // もし、最も左の背景の「右端」が、画面の「左端」よりも左に行ったら（＝完全に見えなくなったら）
        if (leftmostBG.transform.position.x + backgroundWidth < mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            // 最も右にある背景を探す
            GameObject rightmostBG = backgrounds[backgrounds.Count - 1];

            // 画面外に出た左端の背景を、右端の背景のさらに右隣に瞬間移動させる
            leftmostBG.transform.position = new Vector3(rightmostBG.transform.position.x + backgroundWidth, leftmostBG.transform.position.y, leftmostBG.transform.position.z);

            // 管理リストの中でも、先頭から末尾に移動させる
            backgrounds.Remove(leftmostBG);
            backgrounds.Add(leftmostBG);
        }
    }
}