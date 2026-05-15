using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class FreeCursorController : MonoBehaviour
{
    public float moveSpeed = 800f; // スピード
    private RectTransform _rectTransform;
    private Vector2 _moveInput;

    // UIを「突く」ためのコンポーネント
    private GraphicRaycaster _raycaster;
    private EventSystem _eventSystem;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        // シーン内のキャンバスにあるRaycasterを探す
        _raycaster = GetComponentInParent<GraphicRaycaster>();
        _eventSystem = EventSystem.current;
    }

    // スティック入力（Input Systemから自動で呼ばれる）
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        // これを足す
        Debug.Log($"入力が来ました！: {_moveInput}");
    }

    // Aボタン入力（Input Systemから自動で呼ばれる）
    public void OnFire(InputValue value)
    {
        if (value.isPressed) CheckAndClickButton();
    }

    void Update()
    {
        // 座標計算：現在地 + (入力 × 速さ × 時間)
        Vector2 nextPos = _rectTransform.anchoredPosition + (_moveInput * moveSpeed * Time.deltaTime);

        // 【画面外制限】
        // キャンバスのサイズに合わせて制限をかける（必要ならここでMathf.Clamp）
        _rectTransform.anchoredPosition = nextPos;
    }

    // 透明な指でボタンを突く処理
    private void CheckAndClickButton()
    {
        // --- 修正ポイント：毎回最新の状態をチェックする ---
        if (_eventSystem == null)
        {
            _eventSystem = EventSystem.current;
            // もしこれでも取れないなら、シーン全体から強引に探す
            if (_eventSystem == null)
            {
                _eventSystem = Object.FindAnyObjectByType<EventSystem>();
            }
        }

        if (_raycaster == null)
        {
            _raycaster = GetComponentInParent<GraphicRaycaster>();
        }

        // --- ここで最終チェック（これでもダメなら中断する） ---
        if (_eventSystem == null || _raycaster == null)
        {
            Debug.LogWarning("EventSystemまたはRaycasterがまだ見つかりません。");
            return;
        }

        // 56行目：ここで新しいデータを作る（_eventSystemが確実にある状態で！）
        PointerEventData pointerData = new PointerEventData(_eventSystem);
        pointerData.position = transform.position;

        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            Button btn = result.gameObject.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.Invoke();
                break;
            }
        }
    }
}
