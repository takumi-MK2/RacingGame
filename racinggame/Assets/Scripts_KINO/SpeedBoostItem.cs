using System.Collections;
using UnityEngine;

public class SpeedBoostItem : MonoBehaviour
{
    [Header("ブースト設定")]
    [Tooltip("加速させる力（大きくするほど一瞬で強く押し出します）")]
    [SerializeField] private float boostForce = 20f;

    [Tooltip("ブーストが持続する時間（秒）")]
    [SerializeField] private float boostDuration = 2.0f;

    [Header("演出設定")]
    [Tooltip("アイテムの見た目のオブジェクト（消える演出用）")]
    [SerializeField] private GameObject itemVisual = null;

    [Tooltip("再出現するまでの時間（秒）。0なら復活しない")]
    [SerializeField] private float respawnTime = 5.0f;

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        // 触れたオブジェクト、またはその親からRigidbody（物理演算コンポーネント）を探す
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();

        if (rb != null)
        {
            // 持続的な加速処理を開始
            StartCoroutine(BoostRoutine(rb));
        }
    }

    private IEnumerator BoostRoutine(Rigidbody rb)
    {
        isCollected = true;

        // アイテムの見た目を非表示にする
        if (itemVisual != null) itemVisual.SetActive(false);

        float elapsedTime = 0f;
        Debug.Log("物理ブースト開始！");

        // 指定された時間の間、毎フレーム車を前方に押し続ける
        while (elapsedTime < boostDuration)
        {
            // 車がまだ存在しているかチェック（安全のため）
            if (rb == null) break;

            // 車の「正面方向（rb.transform.forward）」に向かって力を加え続ける
            rb.AddForce(rb.transform.forward * boostForce, ForceMode.Force);

            elapsedTime += Time.deltaTime;
            yield return null; // 1フレーム待つ
        }

        Debug.Log("物理ブースト終了");

        // アイテムの再出現処理
        if (respawnTime > 0f)
        {
            yield return new WaitForSeconds(respawnTime);
            if (itemVisual != null) itemVisual.SetActive(true);
            isCollected = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}