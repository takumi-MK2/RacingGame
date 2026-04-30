using UnityEngine;

public class ChangeCarColor : MonoBehaviour
{
    public GameObject cube, sphere, quad;
    public Material playerColor;
    public int choice = 0;

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
        if (choice > 0)
        {
            if (Input.GetKeyDown(KeyCode.W)) choice--;
        }
        if (choice < 2)
        {
            if (Input.GetKeyDown(KeyCode.S)) choice++;
        }

    }
}
