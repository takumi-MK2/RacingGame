using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class FreeCursorController : MonoBehaviour
{
    public float moveSpeed = 800f;

    // 【修正】インスペクターでセットしたオブジェクトが固定されるようにする
    [SerializeField] private RectTransform _rectTransform;

    private Vector2 _moveInput;
    private bool _isFirePressed;

    private GraphicRaycaster _raycaster;
    private EventSystem _eventSystem;

    void Start()
    {
        // もしインスペクターが空っぽだったら、自動で自分自身を設定する（保険）
        if (_rectTransform == null)
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        _raycaster = GetComponentInParent<GraphicRaycaster>();
        _eventSystem = EventSystem.current;
    }

    void Update()
    {
        // 座標計算
        Vector2 nextPos = _rectTransform.anchoredPosition + (_moveInput * moveSpeed * Time.deltaTime);
        _rectTransform.anchoredPosition = nextPos;

        // ★【座標確認用】スティックが動いている間だけ、現在の座標をログに出す
        if (_moveInput.magnitude > 0.01f)
        {
            Debug.Log($"{gameObject.name} の現在の座標: {_rectTransform.anchoredPosition}");
        }

        // ボタンが押されたらクリック処理
        if (_isFirePressed)
        {
            CheckAndClickButton();
            _isFirePressed = false; // 1回押したらリセット
        }
    }

    // ★マルチプレイ用の入力受け取り口（Player Inputから自動で呼ばれる）
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        // ボタンを押した瞬間だけtrueにする
        if (value.isPressed)
        {
            _isFirePressed = true;
        }
    }

    private void CheckAndClickButton()
    {
        if (_eventSystem == null) _eventSystem = EventSystem.current;
        if (_eventSystem == null || _raycaster == null) return;

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