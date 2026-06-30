using UnityEngine;

public class CarRespawnMemory : MonoBehaviour
{
    // 💡 チェックポイント（Checkpoint.cs）から位置を書き換えてもらうための変数
    // これだけ残しておけばOKです！
    [HideInInspector] public Vector3 lastGroundedPosition;
    [HideInInspector] public Quaternion lastGroundedRotation;

    private void Start()
    {
        // ゲームが始まった瞬間（スタート時）の位置を、最初の復活ポイントとして記憶しておく
        lastGroundedPosition = transform.position;
        lastGroundedRotation = transform.rotation;
    }
}