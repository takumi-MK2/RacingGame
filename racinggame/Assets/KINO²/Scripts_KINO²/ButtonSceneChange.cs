using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSceneChange : MonoBehaviour
{
    [SerializeField] private Button Button_01;
    [SerializeField] private Button Button_02;

    void Start()
    {
        // ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚Æ‚«‚Ìˆ—‚ğ“o˜^
        Button_01.onClick.AddListener(GoToSelectStage);
        Button_02.onClick.AddListener(GoToTitle);
    }

    // Button_01 ¨ SelectStage
    private void GoToSelectStage()
    {
        SceneManager.LoadScene("SelectStage");
    }

    // Button_02 ¨ title
    private void GoToTitle()
    {
        SceneManager.LoadScene("title");
    }
}