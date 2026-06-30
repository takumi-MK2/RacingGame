using UnityEngine;

public class RespawnController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody carRb = other.GetComponent<Rigidbody>();

        if (carRb != null)
        {
            Debug.Log($"{other.gameObject.name} が落下！チェックポイントに戻します。");

            // 車の勢いをゼロにする
            carRb.linearVelocity = Vector3.zero;
            carRb.angularVelocity = Vector3.zero;

            CarRespawnMemory memory = other.GetComponent<CarRespawnMemory>();
            if (memory != null)
            {
                // チェックポイントの位置ぴったりに復活（埋まらないように少しだけ浮かす）
                other.transform.position = memory.lastGroundedPosition + Vector3.up * 0.5f;
                other.transform.rotation = memory.lastGroundedRotation;
            }
        }
    }
}