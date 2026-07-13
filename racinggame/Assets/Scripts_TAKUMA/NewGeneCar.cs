using SD;
using Unity.VisualScripting;
using UnityEngine;

public class NewGeneCar : MonoBehaviour
{
    public GameObject car1, car2, car3;
    public Material color1p, color2p, color3p, color4p;
    public Transform grid1, grid2, grid3, grid4, grid1_3, grid2_3, grid3_3;

    [SerializeField] SaveData SD;

    [Header("プレイヤーマーク画像")]
    public Sprite mark1P;
    public Sprite mark2P;
    public Sprite mark3P;
    public Sprite mark4P;

    [SerializeField] private float markerHeight = 2.5f;

    void Awake()
    {
        SD = FindAnyObjectByType<SaveData>();
    }

    void Start()
    {
        if (SD.carChoice4P != -999) GenerateCar(4);
        else if (SD.carChoice3P != -999) GenerateCar(3);
        else GenerateCar(2);
    }

    void GenerateCar(int num)
    {
        GameObject cod1, cod2, cod3, cod4;
        Renderer gawa1, gawa2, gawa3, gawa4;
        Color32 col1, col2, col3, col4;
        Light[] lie1, lie2, lie3, lie4;

        switch (num)
        {
            case 2:
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

                //if (SD.carChoice1P == 0) cod1 = Instantiate(car1, grid1);
                //else if (SD.carChoice1P == 1) cod1 = Instantiate(car2, grid1);
                //else cod1 = Instantiate(car3, grid1);
                //gawa1 = cod1.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();

                if (gawa1 == null) Debug.Log("gawa1ないけど");

                gawa1.material = color1p;
                col1 = new Color32(0, 156, 255, 255);
                lie1 = cod1.GetComponentsInChildren<Light>();
                foreach (Light shori in lie1)
                {
                    shori.color = col1;
                }

                //if (SD.carChoice2P == 0) cod2 = Instantiate(car1, grid2);
                //else if (SD.carChoice2P == 1) cod2 = Instantiate(car2, grid2);
                //else cod2 = Instantiate(car3, grid2);
                //gawa2 = cod2.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
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
                break;

            case 3:
                //if (SD.carChoice1P == 0) cod1 = Instantiate(car1, grid1_3);
                //else if (SD.carChoice1P == 1) cod1 = Instantiate(car2, grid1_3);
                //else cod1 = Instantiate(car3, grid1_3);
                //gawa1 = cod1.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
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

                //if (SD.carChoice2P == 0) cod2 = Instantiate(car1, grid2_3);
                //else if (SD.carChoice2P == 1) cod2 = Instantiate(car2, grid2_3);
                //else cod2 = Instantiate(car3, grid2_3);
                //gawa2 = cod2.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
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

                //if (SD.carChoice3P == 0) cod3 = Instantiate(car1, grid3_3);
                //else if (SD.carChoice3P == 1) cod3 = Instantiate(car2, grid3_3);
                //else cod3 = Instantiate(car3, grid3_3);
                //gawa3 = cod3.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
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
                break;

            case 4:
                //if (SD.carChoice1P == 0) cod1 = Instantiate(car1, grid1);
                //else if (SD.carChoice1P == 1) cod1 = Instantiate(car2, grid1);
                //else cod1 = Instantiate(car3, grid1);
                //gawa1 = cod1.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
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

                //if (SD.carChoice2P == 0) cod2 = Instantiate(car1, grid2);
                //else if (SD.carChoice2P == 1) cod2 = Instantiate(car2, grid2);
                //else cod2 = Instantiate(car3, grid2);
                //gawa2 = cod2.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
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

                //if (SD.carChoice3P == 0) cod3 = Instantiate(car1, grid3);
                //else if (SD.carChoice3P == 1) cod3 = Instantiate(car2, grid3);
                //else cod3 = Instantiate(car3, grid3);
                //gawa3 = cod3.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
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

                //if (SD.carChoice4P == 0) cod4 = Instantiate(car1, grid4);
                //else if (SD.carChoice4P == 1) cod4 = Instantiate(car2, grid4);
                //else cod4 = Instantiate(car3, grid4);
                //gawa4 = cod4.transform.Find("body/mesh body/Jeep/Cylinder.018_Cylinder.007").GetComponent<Renderer>();
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
                break;
        }
    }

    void AttachPlayerMarker(GameObject carObject, Sprite markerSprite)
    {
        // 画像が登録されていない、または車がない場合は何もしない（安全装置）
        if (markerSprite == null || carObject == null) return;

        // 1. マーク用の空のオブジェクトを作る
        GameObject markerObj = new GameObject("PlayerMarkerObject");

        // 2. それを車の子要素にする
        markerObj.transform.SetParent(carObject.transform);

        // 3. 車の少し上に配置、向きをカメラ方向へ初期化
        markerObj.transform.localPosition = new Vector3(0, markerHeight, 0);
        markerObj.transform.localRotation = Quaternion.identity;
        markerObj.transform.localScale = Vector3.one;

        // 4. 画像を表示するための Sprite Renderer を追加して画像をセット
        SpriteRenderer sr = markerObj.AddComponent<SpriteRenderer>();
        sr.sprite = markerSprite;

        // 5. カメラの方向を向き、密集時に消えるスクリプトを追加（これだけ作っておいてください）
        markerObj.AddComponent<PlayerMarkerController>();
    }


}