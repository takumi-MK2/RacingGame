using UnityEngine;
using SD;

public class ChangeCarColor : MonoBehaviour
{
    public int playerNum; //プレイヤー番号
    public GameObject cursor; //プレイヤーごとのカーソルをアタッチ
    public GameObject cube, sphere, plain; //車のモデル(今は仮オブジェクト)
    public Material playerColor; //プレイヤーごとの色マテリアル
    public int choice; //車種番号
    int vector;
    [SerializeField] SaveData SD;

    void Start()
    {
        //初期位置は一番上(choice:0)
        cube.SetActive(true);
        sphere.SetActive(false);
        plain.SetActive(false);

        if (playerNum == 1 || playerNum == 3) vector = 1;
        else if (playerNum == 2 || playerNum == 4) vector = -1;
    }

    void Update()
    {
        ChangeObject();

        Erabu();

        switch (playerNum)
        {
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
    }

    ////選択した車種を表示する////
    void ChangeObject()
    {
        switch (choice)
        {
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
                }
                //下キー
                if (choice < 2 && (Input.GetKeyDown(KeyCode.S)))
                {
                    choice++;
                    cursor.transform.Translate(Vector3.down * 300 * vector);
                }
                break;

            case 2:
                //上キー
                if (choice > 0 && (Input.GetKeyDown(KeyCode.R)))
                {
                    choice--;
                    cursor.transform.Translate(Vector3.up * 300 * vector);
                }
                //下キー
                if (choice < 2 && (Input.GetKeyDown(KeyCode.F)))
                {
                    choice++;
                    cursor.transform.Translate(Vector3.down * 300 * vector);
                }
                break;

            case 3:
                //上キー
                if (choice > 0 && (Input.GetKeyDown(KeyCode.Y)))
                {
                    choice--;
                    cursor.transform.Translate(Vector3.up * 300 * vector);
                }
                //下キー
                if (choice < 2 && (Input.GetKeyDown(KeyCode.H)))
                {
                    choice++;
                    cursor.transform.Translate(Vector3.down * 300 * vector);
                }
                break;

            case 4:
                //上キー
                if (choice > 0 && (Input.GetKeyDown(KeyCode.I)))
                {
                    choice--;
                    cursor.transform.Translate(Vector3.up * 300 * vector);
                }
                //下キー
                if (choice < 2 && (Input.GetKeyDown(KeyCode.K)))
                {
                    choice++;
                    cursor.transform.Translate(Vector3.down * 300 * vector);
                }
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
}