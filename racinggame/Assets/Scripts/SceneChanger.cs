using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string backSceneName;

    public void GoToNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        // backSceneName ‚ª‹ó‚¶‚á‚È‚¢‚¾‚¯ABƒ{ƒ^ƒ“”»’è‚ğ‚·‚é
        if (!string.IsNullOrEmpty(backSceneName))
        {
            if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                SceneManager.LoadScene(backSceneName);
            }
        }
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene("SelectNumber");
    }
}
