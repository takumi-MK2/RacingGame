using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance;

    [Header("現在の設定人数")]
    public int playerCount = 2; // デフォルトを2人に設定

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ボタンから呼ばれる人数セット用メソッド
    public void SetPlayerCount(int count)
    {
        // 2〜4人の範囲に制限（念のため）
        playerCount = Mathf.Clamp(count, 2, 4);
        Debug.Log($"<color=cyan>プレイ人数を {playerCount} 人に設定しました！</color>");
    }
}