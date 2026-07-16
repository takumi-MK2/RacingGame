using UnityEngine;
//using CCC;
//using ROFN;

namespace SD
{

    public class SaveData : MonoBehaviour
    {
        ////以下の種類の情報を保存する////
        /*
         public int playNum;
        */
        public Material color1P, color2P, color3P, color4P; //プレイヤーごとの色マテリアル
        [Header("ここにプレイヤーごとの車種情報が入ります\n0:トリッキー 1:スタンダード 2:ヘビー")]
        public int carChoice1P; //選んだ車の種類
        public int carChoice2P;
        public int carChoice3P;
        public int carChoice4P; 
        //ChangeCarColor CCC;
        //RestrictOperationFromNums ROFN;

        void Start()
        {
            //データの初期値設定(最初は全部スタンダード)
            carChoice1P = 1;
            carChoice2P = 1;
            carChoice3P = 1;
            carChoice4P = 1;

            //ROFN.DOIT();

            //シーン跨いでも消えないようにするやつ
            DontDestroyOnLoad(this.gameObject);
        }

        public void CCLog(int pNum)
        {
            switch (pNum)
            {
                case 1:
                    Debug.Log($"<color=#08f><b>1P:{carChoice1P}</b></color>");
                    break;
                case 2:
                    Debug.Log($"<color=#f00><b>2P:{carChoice2P}</b></color>");
                    break;
                case 3:
                    Debug.Log($"<color=#0f0><b>3P:{carChoice3P}</b></color>");
                    break;
                case 4:
                    Debug.Log($"<color=#ff0><b>4P:{carChoice4P}</b></color>");
                    break;
            }
        }
    }
}