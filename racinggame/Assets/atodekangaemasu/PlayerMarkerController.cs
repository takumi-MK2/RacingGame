using UnityEngine;

public class PlayerMarkerController : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer myRenderer;

    [Header("密集判定の設定")]
    [SerializeField] private float hideDistance = 2.0f; // マーク同士がこの距離（メートル）以下になったら消す
    private const string MARKER_TAG = "PlayerMarker";

    void Start()
    {
        mainCamera = Camera.main;
        myRenderer = GetComponent<Renderer>();
        gameObject.tag = MARKER_TAG;
    }

    void LateUpdate()
    {
        if (mainCamera == null || myRenderer == null) return;

        // ① ずっとカメラの正面を向かせる（ビルボード）
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);

        // ② 密集したときにマークを非表示にする
        GameObject[] allMarkers = GameObject.FindGameObjectsWithTag(MARKER_TAG);
        bool shouldHide = false;

        foreach (GameObject otherMarker in allMarkers)
        {
            if (otherMarker == gameObject) continue;

            // 3D空間上の距離を測定
            float distance = Vector3.Distance(transform.position, otherMarker.transform.position);

            if (distance < hideDistance)
            {
                // インスタンスIDを比較して、片方だけを非表示にする（両方消えるのを防ぐ）
                if (gameObject.GetInstanceID() < otherMarker.GetInstanceID())
                {
                    shouldHide = true;
                    break;
                }
            }
        }

        // スプライトの表示・非表示を切り替え
        myRenderer.enabled = !shouldHide;
    }
}