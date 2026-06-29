using UnityEngine;

public class CarRespawnMemory : MonoBehaviour
{
    [HideInInspector] public Vector3 lastGroundedPosition;
    [HideInInspector] public Quaternion lastGroundedRotation;

    [Header("レイを飛ばす高さ（車体の中心より少し下）")]
    public float rayOffset = 0.2f;
    [Header("地面を検知する下方向への距離")]
    public float rayDistance = 1.0f;

    private void Start()
    {
        lastGroundedPosition = transform.position;
        lastGroundedRotation = transform.rotation;
    }

    // 💡 物理的な「接触（Collision）」に頼るのをやめて、
    // 毎フレーム「真下に本物の道路があるか」を光線でチェックする方式に変更します！
    private void Update()
    {
        // 車の中心から少し下げた位置から、真下（Vector3.down）に向かって光線を飛ばす
        Vector3 rayOrigin = transform.position + (Vector3.up * rayOffset);
        RaycastHit hit;

        // 真下に何か床があるかチェック
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance))
        {
            string objName = hit.collider.gameObject.name.ToLower();

            // ❌ もし真下が「海」や「端っこ」なら記憶を更新しない（ドリフトはみ出し対策）
            if (objName.Contains("water") ||
                objName.Contains("border") ||
                objName.Contains("pavement"))
            {
                return;
            }

            // ⭕ 真下が「本当の道路」の時だけ、安全な位置としてがっちり記憶！
            if (objName.Contains("road") || objName.Contains("track") || objName.Contains("ground"))
            {
                // 車がちゃんと上を向いている（ひっくり返っていない）ときだけ記憶
                if (transform.up.y > 0.5f)
                {
                    lastGroundedPosition = transform.position;
                    lastGroundedRotation = transform.rotation;
                }
            }
        }
    }

    // 🛠️ Unityの画面（Sceneビュー）に、見えない光線を白い線で表示させて確認できるようにします
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector3 rayOrigin = transform.position + (Vector3.up * rayOffset);
        Gizmos.DrawLine(rayOrigin, rayOrigin + (Vector3.down * rayDistance));
    }
}