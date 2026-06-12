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
        public int carChoice1P, carChoice2P, carChoice3P, carChoice4P; //選んだ車の種類
        //ChangeCarColor CCC;
        //RestrictOperationFromNums ROFN;

        void Start()
        {
            carChoice1P = 0;
            carChoice2P = 0;
            carChoice3P = 0;
            carChoice4P = 0;

            //ROFN.DOIT();

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