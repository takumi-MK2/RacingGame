using SD;
using UnityEngine;

public class NewGeneCar : MonoBehaviour
{
    [Header("Carのプレハブ(上から トリッキー、スタンダード、ヘビー)")]
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    [Header("プレイヤーカラーのマテリアル")]
    public Material color1p;
    public Material color2p;
    public Material color3p;
    public Material color4p;
    [Header("Carの初期位置設定(２人・４人プレイ用)")]
    public Transform grid1;
    public Transform grid2;
    public Transform grid3;
    public Transform grid4;
    [Header("Carの初期位置設定(３人プレイ用)")]
    public Transform grid1_3;
    public Transform grid2_3;
    public Transform grid3_3;

    [Header("Carの選択情報とか(自動取得)")]
    [SerializeField] SaveData SD;

    [Header("プレイヤーマーク画像")]
    public Sprite mark1P;
    public Sprite mark2P;
    public Sprite mark3P;
    public Sprite mark4P;

    [Header("マーカーの調整")]
    [SerializeField] private float markerHeight = 2.5f;
    // 【変更点1】インスペクターで大きさを変えられる変数を追加（初期値は3倍）
    [SerializeField] private float markerScale = 3.0f;

    void Awake()
    {
        //シーン起動直後、すぐに車種選択情報を取得
        SD = FindAnyObjectByType<SaveData>();

        //プレイ人数に応じて、Carの生成を行う
        if (SD.carChoice4P != -999) GenerateCar(4);
        else if (SD.carChoice3P != -999) GenerateCar(3);
        else GenerateCar(2);
    }

    void Start()
    {

    }

    //車を生成するインスタンス//
    void GenerateCar(int num)
    {
        GameObject cod1, cod2, cod3, cod4;
        Renderer gawa1, gawa2, gawa3, gawa4;
        Color32 col1, col2, col3, col4;
        Light[] lie1, lie2, lie3, lie4;

        switch (num)
        {
            case 2:
                //選択した車種を生成後、色付けするためのレンダラーを取得
                if (SD.carChoice1P == 0)
                {
                    cod1 = Instantiate(car1, grid1);
                    gawa1 = cod1.transform.Find("body/mesh body/RED Car/Body").GetComponent<Renderer>();
                }
                else if (SD.carChoice1P == 1)
                {
                    cod1 = Instantiate(car2, grid1);
                    gawa1 = cod1.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
                }
                else
                {
                    cod1 = Instantiate(car3, grid1);
                    gawa1 = cod1.transform.Find("body/mesh body/Truck Small/Truck small").GetComponent<Renderer>();
                }

                if (gawa1 == null) Debug.Log("gawa1ないけど");

                //レンダラーにプレイヤーカラーをつける
                gawa1.material = color1p;

                //プレハブに仕込んであるライトにもプレイヤーカラーをつける
                col1 = new Color32(0, 156, 255, 255);
                lie1 = cod1.GetComponentsInChildren<Light>();
                foreach (Light shori in lie1)
                {
                    shori.color = col1;
                }

                cod1.AddComponent<PlayerController1P>();
                //cod1.AddComponent<GameController1P>();

                AttachPlayerMarker(cod1, mark1P);
                SetupCarRespawn(cod1);

                if (SD.carChoice2P == 0)
                {
                    cod2 = Instantiate(car1, grid2);
                    gawa2 = cod2.transform.Find("body/mesh body/RED Car/Body").GetComponent<Renderer>();
                }
                else if (SD.carChoice2P == 1)
                {
                    cod2 = Instantiate(car2, grid2);
                    gawa2 = cod2.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
                }
                else
                {
                    cod2 = Instantiate(car3, grid2);
                    gawa2 = cod2.transform.Find("body/mesh body/Truck Small/Truck small").GetComponent<Renderer>();
                }
                gawa2.material = color2p;
                col2 = new Color32(255, 72, 0, 255);
                lie2 = cod2.GetComponentsInChildren<Light>();
                foreach (Light shori in lie2)
                {
                    shori.color = col2;
                }

                //cod2.AddComponent<PlayerController2P>();
                //cod2.AddComponent<GameController2P>();

                AttachPlayerMarker(cod2, mark2P);
                SetupCarRespawn(cod2);

                break;

            case 3:
                if (SD.carChoice1P == 0)
                {
                    cod1 = Instantiate(car1, grid1_3);
                    gawa1 = cod1.transform.Find("body/mesh body/RED Car/Body").GetComponent<Renderer>();
                }
                else if (SD.carChoice1P == 1)
                {
                    cod1 = Instantiate(car2, grid1_3);
                    gawa1 = cod1.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
                }
                else
                {
                    cod1 = Instantiate(car3, grid1_3);
                    gawa1 = cod1.transform.Find("body/mesh body/Truck Small/Truck small").GetComponent<Renderer>();
                }
                gawa1.material = color1p;
                col1 = new Color32(0, 156, 255, 255);
                lie1 = cod1.GetComponentsInChildren<Light>();
                foreach (Light shori in lie1)
                {
                    shori.color = col1;
                }

                cod1.AddComponent<PlayerController1P>();
                //cod1.AddComponent<GameController1P>();

                AttachPlayerMarker(cod1, mark1P);
                SetupCarRespawn(cod1);

                if (SD.carChoice2P == 0)
                {
                    cod2 = Instantiate(car1, grid2_3);
                    gawa2 = cod2.transform.Find("body/mesh body/RED Car/Body").GetComponent<Renderer>();
                }
                else if (SD.carChoice2P == 1)
                {
                    cod2 = Instantiate(car2, grid2_3);
                    gawa2 = cod2.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
                }
                else
                {
                    cod2 = Instantiate(car3, grid2_3);
                    gawa2 = cod2.transform.Find("body/mesh body/Truck Small/Truck small").GetComponent<Renderer>();
                }
                gawa2.material = color2p;
                col2 = new Color32(255, 72, 0, 255);
                lie2 = cod2.GetComponentsInChildren<Light>();
                foreach (Light shori in lie2)
                {
                    shori.color = col2;
                }

                //cod2.AddComponent<PlayerController2P>();
                //cod2.AddComponent<GameController2P>();

                AttachPlayerMarker(cod2, mark2P);
                SetupCarRespawn(cod2);

                if (SD.carChoice3P == 0)
                {
                    cod3 = Instantiate(car1, grid3_3);
                    gawa3 = cod3.transform.Find("body/mesh body/RED Car/Body").GetComponent<Renderer>();
                }
                else if (SD.carChoice3P == 1)
                {
                    cod3 = Instantiate(car2, grid3_3);
                    gawa3 = cod3.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
                }
                else
                {
                    cod3 = Instantiate(car3, grid3_3);
                    gawa3 = cod3.transform.Find("body/mesh body/Truck Small/Truck small").GetComponent<Renderer>();
                }
                gawa3.material = color3p;
                col3 = new Color32(0, 255, 0, 255);
                lie3 = cod3.GetComponentsInChildren<Light>();
                foreach (Light shori in lie3)
                {
                    shori.color = col3;
                }

                //cod3.AddComponent<PlayerController3P>();
                //cod3.AddComponent<GameController3P>();

                AttachPlayerMarker(cod3, mark3P);
                SetupCarRespawn(cod3);

                break;

            case 4:
                if (SD.carChoice1P == 0)
                {
                    cod1 = Instantiate(car1, grid1);
                    gawa1 = cod1.transform.Find("body/mesh body/RED Car/Body").GetComponent<Renderer>();
                }
                else if (SD.carChoice1P == 1)
                {
                    cod1 = Instantiate(car2, grid1);
                    gawa1 = cod1.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
                }
                else
                {
                    cod1 = Instantiate(car3, grid1);
                    gawa1 = cod1.transform.Find("body/mesh body/Truck Small/Truck small").GetComponent<Renderer>();
                }
                gawa1.material = color1p;
                col1 = new Color32(0, 156, 255, 255);
                lie1 = cod1.GetComponentsInChildren<Light>();
                foreach (Light shori in lie1)
                {
                    shori.color = col1;
                }

                cod1.AddComponent<PlayerController1P>();
                //cod1.AddComponent<GameController1P>();

                AttachPlayerMarker(cod1, mark1P);
                SetupCarRespawn(cod1);

                if (SD.carChoice2P == 0)
                {
                    cod2 = Instantiate(car1, grid2);
                    gawa2 = cod2.transform.Find("body/mesh body/RED Car/Body").GetComponent<Renderer>();
                }
                else if (SD.carChoice2P == 1)
                {
                    cod2 = Instantiate(car2, grid2);
                    gawa2 = cod2.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
                }
                else
                {
                    cod2 = Instantiate(car3, grid2);
                    gawa2 = cod2.transform.Find("body/mesh body/Truck Small/Truck small").GetComponent<Renderer>();
                }
                gawa2.material = color2p;
                col2 = new Color32(255, 72, 0, 255);
                lie2 = cod2.GetComponentsInChildren<Light>();
                foreach (Light shori in lie2)
                {
                    shori.color = col2;
                }

                //cod2.AddComponent<PlayerController2P>();
                //cod2.AddComponent<GameController2P>();

                AttachPlayerMarker(cod2, mark2P);
                SetupCarRespawn(cod2);

                if (SD.carChoice3P == 0)
                {
                    cod3 = Instantiate(car1, grid3);
                    gawa3 = cod3.transform.Find("body/mesh body/RED Car/Body").GetComponent<Renderer>();
                }
                else if (SD.carChoice3P == 1)
                {
                    cod3 = Instantiate(car2, grid3);
                    gawa3 = cod3.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
                }
                else
                {
                    cod3 = Instantiate(car3, grid3);
                    gawa3 = cod3.transform.Find("body/mesh body/Truck Small/Truck small").GetComponent<Renderer>();
                }
                gawa3.material = color3p;
                col3 = new Color32(0, 255, 0, 255);
                lie3 = cod3.GetComponentsInChildren<Light>();
                foreach (Light shori in lie3)
                {
                    shori.color = col3;
                }

                //cod3.AddComponent<PlayerController3P>();
                //cod3.AddComponent<GameController3P>();

                AttachPlayerMarker(cod3, mark3P);
                SetupCarRespawn(cod3);

                if (SD.carChoice4P == 0)
                {
                    cod4 = Instantiate(car1, grid4);
                    gawa4 = cod4.transform.Find("body/mesh body/RED Car/Body").GetComponent<Renderer>();
                }
                else if (SD.carChoice4P == 1)
                {
                    cod4 = Instantiate(car2, grid4);
                    gawa4 = cod4.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
                }
                else
                {
                    cod4 = Instantiate(car3, grid4);
                    gawa4 = cod4.transform.Find("body/mesh body/Truck Small/Truck small").GetComponent<Renderer>();
                }
                gawa4.material = color4p;
                col4 = new Color32(255, 255, 0, 255);
                lie4 = cod4.GetComponentsInChildren<Light>();
                foreach (Light shori in lie4)
                {
                    shori.color = col4;
                }

                //cod4.AddComponent<PlayerController4P>();
                //cod4.AddComponent<GameController4P>();

                AttachPlayerMarker(cod4, mark4P);
                SetupCarRespawn(cod4);

                break;
        }
    }

    void AttachPlayerMarker(GameObject carObject, Sprite markerSprite)
    {
        if (markerSprite == null || carObject == null) return;

        GameObject markerObj = new GameObject("PlayerMarkerObject");
        markerObj.transform.SetParent(carObject.transform);

        markerObj.transform.localPosition = new Vector3(0, markerHeight, 0);
        markerObj.transform.localRotation = Quaternion.identity;

        // 【変更点2】Vector3.one だった部分を、上で設定した markerScale に変更しました
        markerObj.transform.localScale = new Vector3(markerScale, markerScale, markerScale);

        SpriteRenderer sr = markerObj.AddComponent<SpriteRenderer>();
        sr.sprite = markerSprite;

        markerObj.AddComponent<PlayerMarkerController>();
    }

    void SetupCarRespawn(GameObject carObject)
    {
        if (carObject == null) return;
        carObject.AddComponent<CarRespawnMemory>();
    }
}