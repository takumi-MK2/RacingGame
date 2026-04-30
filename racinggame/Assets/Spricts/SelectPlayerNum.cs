using UnityEngine;
using SD;
using UnityEngine.SceneManagement;

public class SelectPlayerNum : MonoBehaviour
{
    SaveData SD;
    public int chooseNum = 2;

    void Update()
    {
        if (chooseNum > 2 && Input.GetKeyDown(KeyCode.A)) chooseNum--;

        if (chooseNum < 4 && Input.GetKeyDown(KeyCode.D)) chooseNum++;

        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            SD.playNum = chooseNum;
            SceneManager.LoadScene("SelectCar");
        }
    }
}