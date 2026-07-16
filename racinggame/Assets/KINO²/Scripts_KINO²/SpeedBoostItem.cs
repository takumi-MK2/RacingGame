using System.Collections;
using UnityEngine;
using AshVP; // 【重要】carControllerのネームスペースを合わせる

public class SpeedBoostItem : MonoBehaviour
{
    [Header("ブースト設定")]
    [Tooltip("加速力")]
    [SerializeField] private float boostMultiplier = 2.0f;

    [Tooltip("ブースト時間")]
    [SerializeField] private float boostDuration = 3.0f;

    [Header("演出設定")]
    [Tooltip("アイテムの見た目のオブジェクト（消える演出用）")]
    [SerializeField] private GameObject itemVisual = null;

    [Tooltip("再出現するまでの時間（秒）。0なら復活しない")]
    [SerializeField] private float respawnTime = 5.0f;

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        // 触れたオブジェクト、またはその親から carController を探す
        carController car = other.GetComponentInParent<carController>();

        if (car != null)
        {
            // コルーチンを使ってブースト処理を開始
            StartCoroutine(BoostRoutine(car));
        }
    }

    private IEnumerator BoostRoutine(carController car)
    {
        isCollected = true;

        // アイテムの見た目を非表示にする
        if (itemVisual != null) itemVisual.SetActive(false);

        // 1. 現在の元の加速力を記憶しておく
        float originalForce = car.accelerationForce;

        // 2. 加速力を引き上げる
        car.accelerationForce = originalForce * boostMultiplier;

        // 指定された時間だけ待つ
        yield return new WaitForSeconds(boostDuration);

        // 3. 効果時間が切れたので、元の加速力に戻す（車がまだ存在する場合のみ）
        if (car != null)
        {
            car.accelerationForce = originalForce;
        }

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