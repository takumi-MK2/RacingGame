using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;

public class ButtonController : MonoBehaviour
{
    [Header("進行設定")]
    [Tooltip("次に進むシーン名を入力")]
    public string nextSceneName;

    [Tooltip("チェックを入れると、どのボタンを押しても次へ進みます（タイトル画面用）")]
    public bool anyButtonToNext;

    [Header("戻る設定")]
    [Tooltip("Bボタンで戻りたいシーン名を入力（空欄なら機能しません）")]
    public string backSceneName;

    void Update()
    {
        // --- 1. タイトル画面向けの「何でも次へ」判定 ---
        if (anyButtonToNext)
        {
            if (CheckAnyInput())
            {
                GoToNext();
            }
        }

        // --- 2. タイトル以外向けの「Bボタンで戻る」判定 ---
        if (!string.IsNullOrEmpty(backSceneName))
        {
            // GamepadのEastボタン（Bボタン/〇ボタン）を判定
            if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                GoBack();
            }
            // PCでのテスト用にEscキーでも戻れるように設定
            else if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                GoBack();
            }
        }
    }

    public void GoToNext()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            FadeManager.Instance.ChangeScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next Scene Name が設定されていません！");
        }
    }

    public void GoBack()
    {
        FadeManager.Instance.ChangeScene(backSceneName);
    }

    private bool CheckAnyInput()
    {
        // コントローラーのボタンが押されたか
        if (Gamepad.current != null && Gamepad.current.allControls.Any(x => x.IsPressed() && !x.synthetic))
        {
            return true;
        }
        // キーボードのキーが押されたか
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            return true;
        }
        return false;
    }
}