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
            case 0:
                GameObject obj0 = Instantiate(car1, spawner1P, false);
                Renderer rnd0 = obj0.GetComponent<Renderer>();
                rnd0.material = SD.color1P;
                break;
            case 1:
                GameObject obj1 = Instantiate(car2, spawner1P, false);
                Renderer rnd1 = obj1.GetComponent<Renderer>();
                rnd1.material = SD.color1P;
                break;
            case 2:
                GameObject obj2 = Instantiate(car3, spawner1P, false);
                Renderer rnd2 = obj2.GetComponent<Renderer>();
                rnd2.material = SD.color1P;
                break;
        }

        switch (SD.carChoice2P)
        {
            case 0:
                GameObject obj0 = Instantiate(car1, spawner2P, false);
                Renderer rnd0 = obj0.GetComponent<Renderer>();
                rnd0.material = SD.color2P;
                break;
            case 1:
                GameObject obj1 = Instantiate(car2, spawner2P, false);
                Renderer rnd1 = obj1.GetComponent<Renderer>();
                rnd1.material = SD.color2P;
                break;
            case 2:
                GameObject obj2 = Instantiate(car3, spawner2P, false);
                Renderer rnd2 = obj2.GetComponent<Renderer>();
                rnd2.material = SD.color2P;
                break;
        }

        switch (SD.carChoice3P)
        {
            case 0:
                GameObject obj0 = Instantiate(car1, spawner3P, false);
                Renderer rnd0 = obj0.GetComponent<Renderer>();
                rnd0.material = SD.color3P;
                break;
            case 1:
                GameObject obj1 = Instantiate(car2, spawner3P, false);
                Renderer rnd1 = obj1.GetComponent<Renderer>();
                rnd1.material = SD.color3P;
                break;
            case 2:
                GameObject obj2 = Instantiate(car3, spawner3P, false);
                Renderer rnd2 = obj2.GetComponent<Renderer>();
                rnd2.material = SD.color3P;
                break;
        }

        switch (SD.carChoice4P)
        {
            case 0:
                GameObject obj0 = Instantiate(car1, spawner4P, false);
                Renderer rnd0 = obj0.GetComponent<Renderer>();
                rnd0.material = SD.color4P;
                break;
            case 1:
                GameObject obj1 = Instantiate(car2, spawner4P, false);
                Renderer rnd1 = obj1.GetComponent<Renderer>();
                rnd1.material = SD.color4P;
                break;
            case 2:
                GameObject obj2 = Instantiate(car3, spawner4P, false);
                Renderer rnd2 = obj2.GetComponent<Renderer>();
                rnd2.material = SD.color4P;
                break;
        }

        /*
        Instantiate(car1, spawner1P, false);
        Instantiate(car2, spawner2P, false);
        Instantiate(car3, spawner3P, false);
        Instantiate(car1, spawner4P, false);
        */
    }

    void Update()
    {
        
    }
}