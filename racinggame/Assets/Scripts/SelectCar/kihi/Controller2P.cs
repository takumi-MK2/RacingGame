using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using kihi_CCC;
using SD;

public class Controller2P : MonoBehaviour
{
    public int pNum = 2;
    bool canOparate = true;

    int vector;
    public GameObject cursor; //プレイヤーごとのカーソルをアタッチ
    public Material playerColor; //プレイヤーごとの色マテリアル
    public GameObject Popup; //性能表示のパネル
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

        Popup.SetActive(false);
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

            SD.carChoice2P = choice;
        }
    }

    void CtrlbyKeyboard()
    {
        //上キー
        if (choice > 0 && (Input.GetKeyDown(KeyCode.R)))
        {
            CCC.CursorUp(cursor, pNum);

            choice--;
            //cursor.transform.Translate(Vector3.up * 300 * vector);
            //singleShori = true;
        }
        //下キー
        if (choice < 2 && (Input.GetKeyDown(KeyCode.F)))
        {
            CCC.CursorDown(cursor, pNum);

            choice++;
            //cursor.transform.Translate(Vector3.down * 300 * vector);
            //singleShori = true;
        }
        //性能表示キー
        if (Input.GetKey(KeyCode.V))
        {
            Popup.SetActive(true);
        }
        else Popup.SetActive(false);
    }


    void CtrlbyPad()
    {
        var gamepad = Gamepad.all[1];
        //if (gamepad == null) Debug.Log("コントローラーが接続されていません");
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
            Popup.SetActive(true);
        }
        else Popup.SetActive(false);

        if (notRen && Mathf.Abs(vertical) > 0.4f)
        {
            notRen = true;
        }
        else notRen = false;
    }

}