using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class FreeCursorController : MonoBehaviour
{
    public float moveSpeed = 800f;
    [SerializeField] private RectTransform _rectTransform;

    // ★修正ポイント：インスペクターから入力を直接紐付けます
    [SerializeField] private InputActionProperty moveAction;
    [SerializeField] private InputActionProperty fireAction;

    private GraphicRaycaster _raycaster;
    private EventSystem _eventSystem;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _raycaster = GetComponentInParent<GraphicRaycaster>();
        _eventSystem = EventSystem.current;
    }

    // ★修正ポイント：ゲーム中、常にボタンが押されたかを監視する形に変えます
    void Update()
    {
        // 1. スティックの入力を直接読み取る
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        // 座標計算
        Vector2 nextPos = _rectTransform.anchoredPosition + (moveInput * moveSpeed * Time.deltaTime);
        _rectTransform.anchoredPosition = nextPos;

        // 2. ボタンの入力を直接読み取る（押した瞬間だけ実行）
        if (fireAction.action.WasPressedThisFrame())
        {
            CheckAndClickButton();
        }
    }

    // ゲーム開始時にボタン入力を有効にする
    void OnEnable()
    {
        moveAction.action.Enable();
        fireAction.action.Enable();
    }

    // ゲーム終了時に無効にする
    void OnDisable()
    {
        moveAction.action.Disable();
        fireAction.action.Disable();
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