using SD;
using UnityEngine;

public class GeneCar : MonoBehaviour
{
    public GameObject car1, car2, car3, car4;
    public Transform grid1, grid2, grid3, grid4, grid1_3, grid2_3, grid3_3;
    bool place1P, place2P, place3P, place4P;

    [SerializeField] SaveData SD;

    void Awake()
    {
        SD = FindAnyObjectByType<SaveData>();

    }

    void Start()
    {
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

        //Instantiate(car, grid1_3);
        //Instantiate(car, grid2_3);
        //Instantiate(car, grid3_3);
    }




}