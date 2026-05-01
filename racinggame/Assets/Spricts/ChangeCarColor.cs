using UnityEngine;
//using SD;

public class ChangeCarColor : MonoBehaviour
{
    public GameObject cursor;
    public GameObject cube, sphere, plain;
    public Material playerColor;
    public int choice;
    //SaveData SD;

    void Start()
    {
        cube.SetActive(true);
        sphere.SetActive(false);
        plain.SetActive(false);
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

    void Erabu()
    {
        if (choice > 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            choice--;
            cursor.transform.Translate(Vector3.up * 300);
        }

        if (choice < 2 && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            choice++;
            cursor.transform.Translate(Vector3.down * 300);
        }
    }
}
