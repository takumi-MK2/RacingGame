using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSceneChange : MonoBehaviour
{
    [SerializeField] private Button Button_01;
    [SerializeField] private Button Button_02;

    void Start()
    {
        // ボタンが押されたときの処理を登録
        Button_01.onClick.AddListener(StageSelect);
        Button_02.onClick.AddListener(Title);
    }

    // Button_01 → ステージ選択
    private void StageSelect()
    {
        SceneManager.LoadScene("SelectStage");
    }

    // Button_02 → タイトル
    private void Title()
    {
        SceneManager.LoadScene("title");
    }
}