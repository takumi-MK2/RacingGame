using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 触れてきたのが車だったら、その車にこの場所を覚えさせる
        CarRespawnMemory memory = other.GetComponent<CarRespawnMemory>();
        if (memory != null)
        {
            // このチェックポイントの位置と向きを記憶させる
            memory.lastGroundedPosition = transform.position;
            memory.lastGroundedRotation = transform.rotation;
        }
    }
}