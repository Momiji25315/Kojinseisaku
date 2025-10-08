using UnityEngine;

public class CameraBreathing : MonoBehaviour
{
    [Header("カメラの呼吸 設定")]
    [Tooltip("ズームの中心となるカメラのサイズ")]
    public float baseSize = 5f;

    [Tooltip("ズームイン・ズームアウトする幅の大きさ")]
    public float zoomAmount = 0.5f;

    [Tooltip("ズームが1往復するのにかかる時間（秒）")]
    public float zoomSpeed = 4.0f;

    // --- 内部変数 ---
    private Camera mainCamera;
    private float initialZ; // Z座標を固定するための変数

    void Start()
    {
        // このスクリプトがアタッチされているカメラを取得
        mainCamera = GetComponent<Camera>();

        // 最初のZ座標を記憶しておく（2Dでは通常-10）
        initialZ = transform.position.z;
    }

    void Update()
    {
        // --- カメラのサイズ（ズーム）を滑らかに変化させる ---

        // Time.time を使って、時間と共に変化する値を作る
        // Math.Sin() は、-1 から 1 の範囲を滑らかに行き来する波を作る
        float sinValue = Mathf.Sin(Time.time * (2 * Mathf.PI) / zoomSpeed);

        // Sin波の結果（-1～1）を、ズームの幅に合わせて調整する
        float zoomOffset = sinValue * zoomAmount;

        // 基本となるサイズに、計算したオフセットを加える
        mainCamera.orthographicSize = baseSize + zoomOffset;


        // (おまけ) もし、Z座標もわずかに前後に動かしたい場合は、以下のコメントを外します
        /*
        float zOffset = sinValue * (zoomAmount / 5f); // ズーム量に応じてZも少し動かす
        transform.position = new Vector3(transform.position.x, transform.position.y, initialZ + zOffset);
        */
    }
}