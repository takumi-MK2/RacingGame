using UnityEngine;
using UnityEngine.SceneManagement;
using SD;
using UnityEngine.UIElements;

namespace CCC
{
    public class ChangeCarColor : MonoBehaviour
    {
        public int playerNum; //プレイヤー番号
        public GameObject cursor; //プレイヤーごとのカーソルをアタッチ
        public GameObject cube, sphere, plain; //車のモデル(今は仮オブジェクト)
        public Material playerColor; //プレイヤーごとの色マテリアル
        public int choice; //車種番号
        int vector;
        public GameObject Popup; //性能表示のパネル
        [SerializeField] SaveData SD; //他シーンに飛ばせる！保存データ
        bool singleShori; //無駄な処理を減らすためのbool
        //public Renderer rnd;

        void Start()
        {
            singleShori = true;

            //初期位置は一番上(choice:0)
            cube.SetActive(true);
            sphere.SetActive(false);
            plain.SetActive(false);

            //カーソルの動きを制御するためのやつ
            if (playerNum == 1 || playerNum == 3) vector = 1;
            else if (playerNum == 2 || playerNum == 4) vector = -1;

            Popup.SetActive(false);
        }

        void Update()
        {
            Erabu();

            if (singleShori) ChangeObject();

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("とぶで");
                SceneManager.LoadScene("Assets/Scenes/SampleScene.unity");

            }
        }

        ////車種を選択する動作////
        void Erabu()
        {
            //playerNumごとに操作できるキーを変える
            switch (playerNum)
            {
                case 1:
                    //上キー
                    if (choice > 0 && (Input.GetKeyDown(KeyCode.W)))
                    {
                        choice--;
                        cursor.transform.Translate(Vector3.up * 300 * vector);
                        singleShori = true;
                    }
                    //下キー
                    if (choice < 2 && (Input.GetKeyDown(KeyCode.S)))
                    {
                        choice++;
                        cursor.transform.Translate(Vector3.down * 300 * vector);
                        singleShori = true;
                    }
                    //性能表示キー
                    if (Input.GetKey(KeyCode.X))
                    {
                        Popup.SetActive(true);
                    }
                    else Popup.SetActive(false);
                    break;

                case 2:
                    //上キー
                    if (choice > 0 && (Input.GetKeyDown(KeyCode.R)))
                    {
                        choice--;
                        cursor.transform.Translate(Vector3.up * 300 * vector);
                        singleShori = true;
                    }
                    //下キー
                    if (choice < 2 && (Input.GetKeyDown(KeyCode.F)))
                    {
                        choice++;
                        cursor.transform.Translate(Vector3.down * 300 * vector);
                        singleShori = true;
                    }
                    //性能表示キー
                    if (Input.GetKey(KeyCode.V))
                    {
                        Popup.SetActive(true);
                    }
                    else Popup.SetActive(false);
                    break;

                case 3:
                    //上キー
                    if (choice > 0 && (Input.GetKeyDown(KeyCode.Y)))
                    {
                        choice--;
                        cursor.transform.Translate(Vector3.up * 300 * vector);
                        singleShori = true;
                    }
                    //下キー
                    if (choice < 2 && (Input.GetKeyDown(KeyCode.H)))
                    {
                        choice++;
                        cursor.transform.Translate(Vector3.down * 300 * vector);
                        singleShori = true;
                    }
                    //性能表示キー
                    if (Input.GetKey(KeyCode.N))
                    {
                        Popup.SetActive(true);
                    }
                    else Popup.SetActive(false);
                    break;

                case 4:
                    //上キー
                    if (choice > 0 && (Input.GetKeyDown(KeyCode.I)))
                    {
                        choice--;
                        cursor.transform.Translate(Vector3.up * 300 * vector);
                        singleShori = true;
                    }
                    //下キー
                    if (choice < 2 && (Input.GetKeyDown(KeyCode.K)))
                    {
                        choice++;
                        cursor.transform.Translate(Vector3.down * 300 * vector);
                        singleShori = true;
                    }
                    //性能表示キー
                    if (Input.GetKey(KeyCode.M))
                    {
                        Popup.SetActive(true);
                    }
                    else Popup.SetActive(false);
                    break;
            }

            /*
            if (choice > 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                choice--;
                cursor.transform.Translate(Vector3.up * 300 * vector);
            }
            if (choice < 2 && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            {
                choice++;
                cursor.transform.Translate(Vector3.down * 300 * vector);
            }
            */
        }

        //車の性能を表示するぜ！Foooooooo//
        void DispPopup()
        {

        }

        ////選択した車種を表示する////
        void ChangeObject()
        {
            switch (choice)
            {
                //今はあらかじめ用意したオブジェクトを切り替えて表示している
                case 0:
                    cube.SetActive(true);
                    sphere.SetActive(false);
                    plain.SetActive(false);
                    break;
                case 1:
                    cube.SetActive(false);
                    sphere.SetActive(true);
                    plain.SetActive(false);
                    break;
                case 2:
                    cube.SetActive(false);
                    sphere.SetActive(false);
                    plain.SetActive(true);
                    break;
            }

            switch (playerNum)
            {
                //SaveDataへ車種番号を通達
                case 1:
                    SD.carChoice1P = choice;
                    break;
                case 2:
                    SD.carChoice2P = choice;
                    break;
                case 3:
                    SD.carChoice3P = choice;
                    break;
                case 4:
                    SD.carChoice4P = choice;
                    break;
            }

            //Debug.Logで車種変更履歴を表示
            SD.CCLog(playerNum);
            
            //rnd.material = playerColor;

            singleShori = false;
        }
    }
}