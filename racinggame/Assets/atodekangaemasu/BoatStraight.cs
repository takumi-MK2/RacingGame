using UnityEngine;

public class BoatStraight : MonoBehaviour
{
    [Header("最小スピード")]
    public float minSpeed = 15.0f;

    [Header("最大スピード")]
    public float maxSpeed = 30.0f;

    [Header("ここまで来たら戻るライン（X座標など）")]
    public float resetLine = -225.0f;

    private float finalSpeed;
    private Vector3 startPosition; // 最初のスタート位置を覚えておく変数

    void Start()
    {
        // 💡 最初に配置した場所を「スタート位置」として記憶する
        startPosition = transform.position;

        // 最初のスピードを決める
        SetRandomSpeed();
    }

    void Update()
    {
        // まっすぐ進む
        transform.Translate(Vector3.forward * finalSpeed * Time.deltaTime);

        // 💡 もしボートの「今のX座標」が、設定したリセットラインを越えたら
        if (transform.position.x > resetLine)
        {
            // スタート位置にワープで戻す
            transform.position = startPosition;

            // 💡 スピードを新しくランダムに決め直す（これで毎回順位が変わります！）
            SetRandomSpeed();
        }
    }

    // スピードをランダムに決める処理
    void SetRandomSpeed()
    {
        finalSpeed = Random.Range(minSpeed, maxSpeed);
    }
}