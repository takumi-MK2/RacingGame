using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using kihi_CCC;
using SD;

public class Controller1P : MonoBehaviour
{
    public int pNum = 1;
    bool canOparate = true;

    int vector;
    public GameObject cursor; //プレイヤーごとのカーソルをアタッチ
    public Material playerColor; //プレイヤーごとの色マテリアル
    public GameObject popupTrk, popupStd, popupHvy; //性能表示のパネル
    bool singleShori; //無駄な処理を減らすためのbool
    bool notRen; //コントローラーの連続処理防止用bool
    public int choice; //車種番号

    public kihi_ChangeCarColor CCC;
    [SerializeField] PlayerDataManager PDM;
    public SaveData SD;

    void Start()
    {
        PDM = FindAnyObjectByType<PlayerDataManager>();

        //if (PDM.playerCount<pNum) canOparate = false;

        if (canOparate)
        {
            //カーソルの動きを制御するためのやつ
            if (pNum == 1 || pNum == 3) vector = 1;
            else if (pNum == 2 || pNum == 4) vector = -1;
        }

        popupTrk.SetActive(false);
        popupStd.SetActive(false);
        popupHvy.SetActive(false);
    }

    void Update()
    {
        if (canOparate)
        {
            var gamepad = Gamepad.current;
            if (gamepad == null)
            {
                Debug.Log($"{pNum}P:コントローラーが接続されていません");
                CtrlbyKeyboard();
            }
            else
            {
                CtrlbyPad();
            }

            SD.carChoice1P = choice;
        }
    }

    void CtrlbyKeyboard()
    {
        //上キー
        if (choice > 0 && (Input.GetKeyDown(KeyCode.W)))
        {
            Debug.Log($"Up,{cursor.name}, {pNum}");

            CCC.CursorUp(cursor, pNum);

            choice--;
            //cursor.transform.Translate(Vector3.up * 300 * vector);
            //singleShori = true;
        }
        //下キー
        if (choice < 2 && (Input.GetKeyDown(KeyCode.S)))
        {
            Debug.Log($"Down,{cursor.name}, {pNum}");
            CCC.CursorDown(cursor, pNum);

            choice++;
            //cursor.transform.Translate(Vector3.down * 300 * vector);
            //singleShori = true;
        }
        //性能表示キー
        if (Input.GetKey(KeyCode.X))
        {
            switch (choice)
            {
                case 0:
                    popupTrk.SetActive(true);
                    break;
                case 1:
                    popupStd.SetActive(true);
                    break;
                case 2:
                    popupHvy.SetActive(true);
                    break;
            }
        }
        else
        {
            popupTrk.SetActive(false);
            popupStd.SetActive(false);
            popupHvy.SetActive(false);
        }

if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("SelectStage");
        }
    }


    void CtrlbyPad()
    {
        var gamepad = Gamepad.all[0];
        if (gamepad == null) Debug.Log("コントローラーが接続されていません");
        float vertical = gamepad.leftStick.y.ReadValue();

        if (((vertical > 0.4f && !notRen) || gamepad.dpad.up.wasPressedThisFrame) && choice > 0)
        {
            CCC.CursorUp(cursor, pNum);

            choice--;
            //cursor.transform.Translate(Vector3.up * 300 * vector);
            //singleShori = true;

            notRen = true;
        }
        if (((vertical < -0.4f && !notRen) || gamepad.dpad.down.wasPressedThisFrame) && choice < 2)
        {
            CCC.CursorDown(cursor, pNum);

            choice++;
            //cursor.transform.Translate(Vector3.down * 300 * vector);
            //singleShori = true;

            notRen = true;
        }
        if (gamepad.buttonSouth.isPressed)
        {
            switch (choice)
            {
                case 0:
                    popupTrk.SetActive(true);
                    break;
                case 1:
                    popupStd.SetActive(true);
                    break;
                case 2:
                    popupHvy.SetActive(true);
                    break;
            }
        }
        else
        {
            popupTrk.SetActive(false);
            popupStd.SetActive(false);
            popupHvy.SetActive(false);
        }

        if (gamepad.buttonEast.wasPressedThisFrame)
        {
            SceneManager.LoadScene("SelectStage");
        }

        if (notRen && Mathf.Abs(vertical) > 0.4f)
        {
            notRen = true;
        }
        else notRen = false;
    }
}