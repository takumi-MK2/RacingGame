using UnityEngine;
using System.Collections.Generic; // これ（Listを使うために必要）を忘れないように！

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance;

    // --- ここがエラーの原因！この変数が定義されている必要があります ---
    public List<int> selectedStageIndices = new List<int>();
    // ----------------------------------------------------------

    public int playerCount = 2;
    public string finalStageName;

    void Awake()
    {
        // シーンをまたいでも消えない設定
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

    public void DecidePlayerCount(int pCnt)
    {
        playerCount = pCnt;
    }

}