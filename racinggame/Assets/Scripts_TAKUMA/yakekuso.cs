using UnityEngine;
using UnityEngine.InputSystem;

public class yakekuso : MonoBehaviour
{
    [Header("操作を許可するコントローラーの番号 (1台目=0, 2台目=1)")]
    [SerializeField] private int allowedGamepadIndex = 0;

    /// <summary>
    /// 現在の操作が許可されているかチェックするヘルパープロパティ
    /// </summary>
    public bool CanProcessInput
    {
        get; private set;
    }

    /// <summary>
    /// 許可されたGamepadインスタンスを取得
    /// </summary>
    public Gamepad AllowedGamepad { get; private set; }

    private void Update()
    {
        var gamepads = Gamepad.all;

        // 指定したインデックスのゲームパッドが存在するかチェック
        if (gamepads.Count > allowedGamepadIndex)
        {
            AllowedGamepad = gamepads[allowedGamepadIndex];
            CanProcessInput = true;
        }
        else
        {
            AllowedGamepad = null;
            CanProcessInput = false;
        }
    }
}