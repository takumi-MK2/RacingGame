using UnityEngine;
using SD;

public class ChangeCarColor : MonoBehaviour
{
    public GameObject cursor;
    public GameObject cube, sphere, quad;
    public Material playerColor;
    public int choice = 0;
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
        switch (choice)
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
        if (choice > 0 && Input.GetKeyDown(KeyCode.W))
        {
            choice--;
            cursor.transform.Translate(Vector3.up * 300);
        }

        if (choice < 2 && Input.GetKeyDown(KeyCode.S))
        {
            choice++;
            cursor.transform.Translate(Vector3.down * 300);
        }
    }
}
