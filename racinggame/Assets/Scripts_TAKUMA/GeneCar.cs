using SD;
using UnityEngine;

public class GeneCar : MonoBehaviour
{
    public GameObject car1, car2, car3, car4;
    public Transform grid1, grid2, grid3, grid4, grid1_3, grid2_3, grid3_3;
    //bool place1P, place2P, place3P, place4P;
    //bool b;

    [SerializeField] SaveData SD;

    void Awake()
    {
        SD = FindAnyObjectByType<SaveData>();
        //b = true;
    }

    void Start()
    {
        if (SD.carChoice4P != -999) GenerateCar(4);
        else if (SD.carChoice3P != -999) GenerateCar(3);
        else GenerateCar(2);

        /*
        Debug.Log("ゎや");
        GameObject cod1 = Instantiate(car1, grid1);
        Color32 col1 = new Color32(0, 156, 255, 255);
        Light[] lie1 = cod1.GetComponentsInChildren<Light>();
        foreach (Light shori in lie1)
        {
            shori.color = col1;
        }

        GameObject cod2 = Instantiate(car2, grid2);
        Color32 col2 = new Color32(255, 72, 0, 255);
        Light[] lie2 = cod2.GetComponentsInChildren<Light>();
        foreach (Light shori in lie2)
        {
            shori.color = col2;
        }

        GameObject cod3 = Instantiate(car3, grid3);
        Color32 col3 = new Color32(0, 255, 0, 255);
        Light[] lie3 = cod3.GetComponentsInChildren<Light>();
        foreach (Light shori in lie3)
        {
            shori.color = Color.green;
        }

        GameObject cod4 = Instantiate(car4, grid4);
        Color32 col4 = new Color32(255, 255, 0, 255);
        Light[] lie4 = cod4.GetComponentsInChildren<Light>();
        foreach (Light shori in lie4)
        {
            shori.color = Color.yellow;
        }
        */
        //Instantiate(car, grid1_3);
        //Instantiate(car, grid2_3);
        //Instantiate(car, grid3_3);
    }

    //void Update()
    //{
    //    if (b)
    //    {
    //        for (int i = 0; i < 5; i++)
    //        {
    //            Instantiate(car1, grid1);
    //            Instantiate(car2, grid2);
    //            Instantiate(car3, grid3);
    //            Instantiate(car4, grid4);
    //        }
    //        b = false;
    //    }
    //}

    void GenerateCar(int num)
    {
        GameObject cod1, cod2, cod3, cod4;
        Color32 col1, col2, col3, col4;
        Light[] lie1, lie2, lie3, lie4;

        switch (num)
        {
            case 2:
                if (SD.carChoice1P==0)  cod1 = Instantiate(car1, grid1);
                else if (SD.carChoice1P == 1)  cod1 = Instantiate(car2, grid1);
                else cod1 = Instantiate(car3, grid1);
                 col1 = new Color32(0, 156, 255, 255);
                 lie1 = cod1.GetComponentsInChildren<Light>();
                foreach (Light shori in lie1)
                {
                    shori.color = col1;
                }

                if (SD.carChoice2P == 0) cod2 = Instantiate(car1, grid2);
                else if (SD.carChoice2P == 1) cod2 = Instantiate(car2, grid2);
                else cod2 = Instantiate(car3, grid2);
                 col2 = new Color32(255, 72, 0, 255);
                 lie2 = cod2.GetComponentsInChildren<Light>();
                foreach (Light shori in lie2)
                {
                    shori.color = col2;
                }
            break;

            case 3:
                if (SD.carChoice1P == 0) cod1 = Instantiate(car1, grid1_3);
                else if (SD.carChoice1P == 1) cod1 = Instantiate(car2, grid1_3);
                else cod1 = Instantiate(car3, grid1_3);
                col1 = new Color32(0, 156, 255, 255);
                lie1 = cod1.GetComponentsInChildren<Light>();
                foreach (Light shori in lie1)
                {
                    shori.color = col1;
                }

                if (SD.carChoice2P == 0) cod2 = Instantiate(car1, grid2_3);
                else if (SD.carChoice2P == 1) cod2 = Instantiate(car2, grid2_3);
                else cod2 = Instantiate(car3, grid2_3);
                col2 = new Color32(255, 72, 0, 255);
                lie2 = cod2.GetComponentsInChildren<Light>();
                foreach (Light shori in lie2)
                {
                    shori.color = col2;
                }

                if (SD.carChoice3P == 0) cod3 = Instantiate(car1, grid3_3);
                else if (SD.carChoice3P == 1) cod3 = Instantiate(car2, grid3_3);
                else cod3 = Instantiate(car3, grid3_3);
                col3 = new Color32(0, 255, 0, 255);
                lie3 = cod3.GetComponentsInChildren<Light>();
                foreach (Light shori in lie3)
                {
                    shori.color = col3;
                }
            break;

            case 4:
                if (SD.carChoice1P == 0) cod1 = Instantiate(car1, grid1);
                else if (SD.carChoice1P == 1) cod1 = Instantiate(car2, grid1);
                else cod1 = Instantiate(car3, grid1);
                col1 = new Color32(0, 156, 255, 255);
                lie1 = cod1.GetComponentsInChildren<Light>();
                foreach (Light shori in lie1)
                {
                    shori.color = col1;
                }

                if (SD.carChoice2P == 0) cod2 = Instantiate(car1, grid2);
                else if (SD.carChoice2P == 1) cod2 = Instantiate(car2, grid2);
                else cod2 = Instantiate(car3, grid2);
                col2 = new Color32(255, 72, 0, 255);
                lie2 = cod2.GetComponentsInChildren<Light>();
                foreach (Light shori in lie2)
                {
                    shori.color = col2;
                }

                if (SD.carChoice3P == 0) cod3 = Instantiate(car1, grid3);
                else if (SD.carChoice3P == 1) cod3 = Instantiate(car2, grid3);
                else cod3 = Instantiate(car3, grid3);
                col3 = new Color32(0, 255, 0, 255);
                lie3 = cod3.GetComponentsInChildren<Light>();
                foreach (Light shori in lie3)
                {
                    shori.color = col3;
                }

                if (SD.carChoice4P == 0) cod4 = Instantiate(car1, grid4);
                else if (SD.carChoice4P == 1) cod4 = Instantiate(car2, grid4);
                else cod4 = Instantiate(car3, grid4);
                col4 = new Color32(255, 255, 0, 255);
                lie4 = cod4.GetComponentsInChildren<Light>();
                foreach (Light shori in lie4)
                {
                    shori.color = col4;
                }
            break;



        }
    }

    void kyoutuu()
    {

    }

}