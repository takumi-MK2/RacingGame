using UnityEngine;
using SD;

public class ChangeCarColor : MonoBehaviour
{
    public GameObject cube, sphere, quad;
    public Material playerColor;
    //public int choice = 0;
    SaveData SD;

    void Start()
    {
        cube.SetActive(true);
        sphere.SetActive(false);
        quad.SetActive(false);
    }

    void Update()
    {
        ChangeObject();

        Erabu();
    }

    void ChangeObject()
    {
        switch (SD.carChoice1P)
        {
            case 0:
                cube.SetActive(true);
                sphere.SetActive(false);
                quad.SetActive(false);
                break;
            case 1:
                cube.SetActive(false);
                sphere.SetActive(true);
                quad.SetActive(false);
                break;
            case 2:
                cube.SetActive(false);
                sphere.SetActive(false);
                quad.SetActive(true);
                break;
        }
    }

    void Erabu()
    {
        if (SD.carChoice1P > 0 && Input.GetKeyDown(KeyCode.W)) SD.carChoice1P--;

        if (SD.carChoice1P < 2 && Input.GetKeyDown(KeyCode.S)) SD.carChoice1P++;
    }
}
