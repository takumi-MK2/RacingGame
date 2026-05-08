using UnityEngine;
using SD;

public class ShakoDashi : MonoBehaviour
{
    public Transform spawner1P, spawner2P, spawner3P, spawner4P;
    public GameObject car1, car2, car3;
    [SerializeField] SaveData SD;

    void Start()
    {
        SD = FindAnyObjectByType<SaveData>();

        switch (SD.carChoice1P)
        {
            case 0: Instantiate(car1, spawner1P, false);
                break;
            case 1: Instantiate(car2, spawner1P, false);
                break;
            case 2: Instantiate(car3, spawner1P, false);
                break;
        }

        switch (SD.carChoice2P)
        {
            case 0: Instantiate(car1, spawner2P, false);
                break;
            case 1: Instantiate(car2, spawner2P, false);
                break;
            case 2: Instantiate(car3, spawner2P, false);
                break;
        }

        switch (SD.carChoice3P)
        {
            case 0: Instantiate(car1, spawner3P, false);
                break;
            case 1: Instantiate(car2, spawner3P, false);
                break;
            case 2: Instantiate(car3, spawner3P, false);
                break;
        }

        switch (SD.carChoice4P)
        {
            case 0: Instantiate(car1, spawner4P, false);
                break;
            case 1: Instantiate(car2, spawner4P, false);
                break;
            case 2: Instantiate(car3, spawner4P, false);
                break;
        }

        //Instantiate(car1, spawner1P, false);
        //Instantiate(car2, spawner2P, false);
        //Instantiate(car3, spawner3P, false);
        //Instantiate(car1, spawner4P, false);
    }

    void Update()
    {
        
    }
}