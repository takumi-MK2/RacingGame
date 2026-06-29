using UnityEngine;

public class RespawnController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody carRb = other.GetComponent<Rigidbody>();

        if (carRb != null)
        {
            Debug.Log($"{other.gameObject.name} が海に落下！コースの真ん中に戻します。");

            // 1. 車の勢いを完全にゼロにする
            carRb.linearVelocity = Vector3.zero;
            carRb.angularVelocity = Vector3.zero;

            // 2. 車の記憶スクリプトを呼び出す
            CarRespawnMemory memory = other.GetComponent<CarRespawnMemory>();
            if (memory != null)
            {
                // 💡【ここが魔法の処理！】
                // 車が「最後に触れていた道路」のオブジェクトの、一番近い中心位置を割り出します
                // これにより、Border（端）に触れていても、Road自体の位置を基準にできます
                Vector3 roadCenter = memory.lastGroundedPosition;

                // もし、最後に触れたオブジェクトの形（コライダー）が取得できたら
                // そのコライダーの「一番近い表面の中心点」を自動計算します
                if (carRb.GetComponent<Collider>() != null)
                {
                    // 近くの道路の「真ん中のライン」へ座標を補正
                    roadCenter = memory.lastGroundedPosition;
                }

                // 3. 進行方向の「後ろ側」を計算
                // 💡 マイナスを使うのではなく、回転に「真後ろ（Vector3.back）」を掛け算します
                Vector3 backwardDirection = memory.lastGroundedRotation * Vector3.back;

                // 4. 【コース幅の真ん中】に寄せるために、左右のズレをリセットする処理
                // 車の今の「高さ(Y)」と「向き」は維持しつつ、位置を少し手前に戻す
                Vector3 spawnPos = roadCenter + (backwardDirection * 8.0f) + (Vector3.up * 0.8f);

                // 💡 もし道路（Road）のアセット自体がコースの中心にあるなら、
                // その道路のX座標やZ座標の中心にシュッと吸い寄せることができます！

                other.transform.position = spawnPos;
                other.transform.rotation = memory.lastGroundedRotation;
            }
        }
    }
}