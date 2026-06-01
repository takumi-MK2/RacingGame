using UnityEngine;
using UnityEngine.UIElements;

public class tonbo_hane : MonoBehaviour
{
    public float angle;
    public float duration;

    float timer = 0;

    void Update()
    {
        // 時間を進める（片道の秒数で割ることで、0〜1の間の進捗率を作るわ）
        timer += Time.deltaTime / duration;

        // Mathf.PingPongで、0から1の間をずっと往復する値を作る
        // timerが1を超えると、自動的に1→0へ折り返してくれる神機能よ！
        float t = Mathf.PingPong(timer, 1f);

        // 0〜1の進捗率（t）を、実際の角度（minAngle 〜 maxAngle）に変換する
        float currentAngle = Mathf.Lerp(-angle, angle, t);

        // 今回はとりあえずX軸を中心に回転させてみるわね（羽の向きに合わせて変えてね）
        transform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
    }
}