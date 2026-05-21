using SD;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace kihi_CCC
{
    public class kihi_ChangeCarColor : MonoBehaviour
    {
        [Header("ヘッダーいれなきゃ")]
        //public int playerNum; //プレイヤー番号
        //public GameObject cursor; //プレイヤーごとのカーソルをアタッチ
        public GameObject cube, sphere, plain; //車のモデル(今は仮オブジェクト)
        //public Material playerColor; //プレイヤーごとの色マテリアル
        public int choice; //車種番号
        int vector;
        //public GameObject Popup; //性能表示のパネル
        [SerializeField] SaveData SD; //他シーンに飛ばせる！保存データ
        /*bool singleShori; //無駄な処理を減らすためのbool*/
        /*public Renderer rnd;*/
        /*bool notRen; //コントローラーの連続処理防止用bool*/
        public int nunumm; //ひとつのコントローラーで４人の操作を切り替えられるわよ！

        void Start()
        {
            //nunumm = 1;
            ChangeObject();

            /*singleShori = true;*/

            //初期位置は一番上(choice:0)
            //cube.SetActive(true);
            //sphere.SetActive(false);
            //plain.SetActive(false);

            ////カーソルの動きを制御するためのやつ
            //if (playerNum == 1 || playerNum == 3) vector = 1;
            //else if (playerNum == 2 || playerNum == 4) vector = -1;

            //Popup.SetActive(false);
            /*notRen = false;*/

            if (nunumm % 2 == 0) vector = -1;
            else vector = 1;
            Debug.Log($"{vector}");
        }

        /*
        void Update()
        {
            //Erabu();
            PadCtrl();

            //if (singleShori) ChangeObject();

            //他シーンに保存データ持ってく用のやつ//
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("とぶで");
                SceneManager.LoadScene("Assets/Scenes/SampleScene.unity");
            }
        }
        */

        ////車種を選択する動作////
        void Erabu()
        {
            /*
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
                */
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
        }
            */

        ////選択した車種を表示する////
        public void ChangeObject()
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


            /*
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

            Debug.Logで車種変更履歴を表示
            SD.CCLog(playerNum);

            rnd.material = playerColor;*/

            /*singleShori = false;*/
        }

        //コントローラー操作用//
        void PadCtrl()
        {
            /*
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
            }
            */
            
            // 1. コントローラーが接続されているかチェック
            var gamepad = Gamepad.current;
            if (gamepad == null) Debug.Log("GamePad Error");

            if (gamepad.buttonNorth.wasPressedThisFrame)
            {
                nunumm++;
                if (nunumm > 4) nunumm = 1;
                Debug.Log($"{nunumm}");
            }
            if (gamepad.buttonWest.wasPressedThisFrame)
            {
                nunumm--;
                if (nunumm < 1) nunumm = 4;
                Debug.Log($"{nunumm}");
            }

            //if (playerNum == nunumm)
            //{
            //    // 2. 上下方向の入力を取得（左スティック）
            //    // ReadValueで -1.0(下) から 1.0(上) の値が取れるわ
            //    float vertical = gamepad.leftStick.y.ReadValue();

            //    if (((vertical > 0.4f && !notRen) || gamepad.dpad.up.wasPressedThisFrame) && choice > 0)
            //    {
            //        choice--;
            //        cursor.transform.Translate(Vector3.up * 300 * vector);
            //        singleShori = true;
            //        Debug.Log($"上へ入力: {vertical:F2}");

            //        notRen = true;
            //    }
            //    if (((vertical < -0.4f && !notRen) || gamepad.dpad.down.wasPressedThisFrame) && choice < 2)
            //    {
            //        choice++;
            //        cursor.transform.Translate(Vector3.down * 300 * vector);
            //        singleShori = true;
            //        Debug.Log($"下へ入力: {vertical:F2}");

            //        notRen = true;
            //    }

            //    // 3. Aボタン（F310の「A」、内部的にはbuttonSouth）
            //    if (gamepad.buttonSouth.isPressed)
            //    {
            //        Debug.Log("<color=green>Aボタン（決定）が押されたわよ！</color>");
            //        Popup.SetActive(true);
            //    }
            //    else Popup.SetActive(false);

                // 4. Bボタン（F310の「B」、内部的にはbuttonEast）
                if (gamepad.buttonEast.wasPressedThisFrame)
                {
                    SceneManager.LoadScene("Assets/Scenes/SampleScene.unity");

                    Debug.Log("<color=red>Bボタン（キャンセル）が押されたわよ！</color>");
                }

                /*
                if (notRen && Mathf.Abs(vertical) > 0.4f)
                {
                    notRen = true;
                }
                else notRen = false;
                */



        }

        public void CursorUp(GameObject kaso,int pn)
        {
            Debug.Log(vector);
            kaso.transform.Translate(Vector3.up * 300 * vector);
            choice--;
            ChangeObject();
        }

        public void CursorDown(GameObject kaso, int pn)
        {
            kaso.transform.Translate(Vector3.down * 300 * vector);
            choice++;
            ChangeObject();
        }



    }
}