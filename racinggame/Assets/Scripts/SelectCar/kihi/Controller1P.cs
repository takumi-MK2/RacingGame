using UnityEngine;
using UnityEngine.InputSystem;
using kihi_CCC;

public class Controller1P : kihi_ChangeCarColor
{
    public int pNum = 1;
    bool canOparate = true;

    int vector;
    public GameObject cursor; //プレイヤーごとのカーソルをアタッチ
    public Material playerColor; //プレイヤーごとの色マテリアル
    public GameObject Popup; //性能表示のパネル
    bool singleShori; //無駄な処理を減らすためのbool
    bool notRen; //コントローラーの連続処理防止用bool

    kihi_ChangeCarColor CCC;
    [SerializeField] PlayerDataManager PDM;

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


        }
    }

    void CtrlbyKeyboard()
    {
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
    }


    void CtrlbyPad()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) Debug.Log("コントローラーが接続されていません");
        float vertical = gamepad.leftStick.y.ReadValue();

        if (((vertical > 0.4f && !notRen) || gamepad.dpad.up.wasPressedThisFrame) && choice > 0)
        {
            CCC.CursorUp(cursor, pNum);

            //choice--;
            //cursor.transform.Translate(Vector3.up * 300 * vector);
            //singleShori = true;
            Debug.Log($"上へ入力: {vertical:F2}");

            notRen = true;
        }
        if (((vertical < -0.4f && !notRen) || gamepad.dpad.down.wasPressedThisFrame) && choice < 2)
        {
            Debug.Log("CursorDown実行したいね");

            CCC.CursorDown(cursor, pNum);

            //choice++;
            //cursor.transform.Translate(Vector3.down * 300 * vector);
            //singleShori = true;
            Debug.Log($"下へ入力: {vertical:F2}");

            notRen = true;
        }
    }

}