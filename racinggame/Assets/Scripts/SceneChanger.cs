using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string backSceneName;

    public void GoToNextScene(string sceneName)
    {
        FadeManager.Instance.ChangeScene(sceneName);
    }

    void Update()
    {
        // backSceneName ‚ª‹ó‚¶‚á‚È‚¢‚¾‚¯ABƒ{ƒ^ƒ“”»’è‚ğ‚·‚é
        if (!string.IsNullOrEmpty(backSceneName))
        {
            if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                FadeManager.Instance.ChangeScene(backSceneName);
            }
        }
    }

    public void GoToNextScene()
    {
        FadeManager.Instance.ChangeScene("SelectNumber");
    }
}
