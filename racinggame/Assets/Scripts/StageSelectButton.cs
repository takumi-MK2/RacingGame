using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    [Header("このボタンが何番のステージか設定")]
    public int stageID;

    // カーソルが重なって「決定（Fire）」された時に呼ばれる関数
    public void OnClickStage()
    {
        // 全員共通のデータ管理（PlayerDataManager）に選んだIDを送る
        if (PlayerDataManager.instance != null)
        {
            PlayerDataManager.instance.selectedStageIndices.Add(stageID);
            Debug.Log($"ステージ {stageID} に1票入りました！ 現在の合計票数: {PlayerDataManager.instance.selectedStageIndices.Count}");
        }
    }
}