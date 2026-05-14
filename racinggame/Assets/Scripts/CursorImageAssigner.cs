using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorImageAssigner : MonoBehaviour
{
    [Header("1P〜4Pの画像を順番にセット")]
    public Sprite[] cursorSprites;

    void Start()
    {
        // 子要素にあるImageコンポーネントを探す
        Image myImage = GetComponentInChildren<Image>();
        var input = GetComponent<PlayerInput>();

        // 自分が何番目のプレイヤーか（0=1P, 1=2P...）
        int index = input.playerIndex;

        if (index < cursorSprites.Length)
        {
            myImage.sprite = cursorSprites[index];
        }
    }
}