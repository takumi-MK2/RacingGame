using AshVP;
using SD;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController4P : MonoBehaviour
{
    // ラップテキスト
    [SerializeField] Text lapText = null;

    // ゲームステート
    public enum PlayState4p
    {
        None,
        Ready,
        Play,
        Finish,
    }

    // 現在のステート
    public PlayState4p CurrentState = PlayState4p.None;

    // カウントダウンスタートタイム
    [SerializeField] int countStartTime = 5;

    // カウントダウンテキスト
    [SerializeField] Text countdownText = null;

    // タイマーテキスト
    [SerializeField] Text timerText = null;

    // カウントダウンの現在値
    float currentCountDown = 0;

    // ゲーム経過時間
    float timer = 0;

    // プレイヤー
    [SerializeField] PlayerController4P player4p = null;

    // 車の入力マネージャー
    [SerializeField] private InputManager_AshVP carInputManager = null;

    // ゴール後に表示するボタンの親オブジェクト
    [SerializeField] private GameObject button;

    [SerializeField] SaveData SD;

    void Awake()
    {
        SD = FindAnyObjectByType<SaveData>();
    }

    void Start()
    {
        lapText = GameObject.Find("LapText4p").GetComponent<Text>();
        countdownText = GameObject.Find("CountdownText").GetComponent<Text>();
        timerText = GameObject.Find("TimerText4p").GetComponent<Text>();
        
        player4p = GameObject.FindWithTag("Player4P").GetComponent<PlayerController4P>();
        carInputManager = GameObject.FindWithTag("Player4P").GetComponent<InputManager_AshVP>();

        //lapText = transform.parent.parent.Find("Canvas/Header").GetComponent<Text>();
        //countdownText = transform.Find("../../Canvas/CountdownText").GetComponent<Text>();
        //timerText = transform.Find("../../Canvas/Header/TimerText1p").GetComponent<Text>();
        //player1p = transform.Find("../../GeneCar/grid1_24").GetComponent<PlayerController1P>();
        //carInputManager = transform.Find("../../GeneCar/grid1_24").GetComponent<InputManager_AshVP>();

        CountDownStart();

        player4p.LapEvent.AddListener(OnLap);
        player4p.GoalEvent.AddListener(OnGoal);

        timerText.text = "Time:000.000s";
        lapText.text = "1/" + player4p.GoalLap;

        // 最初はボタンを非表示
        if (button != null)
        {
            button.SetActive(false);
        }
    }

    void Update()
    {
        // ステートがReadyのとき
        if (CurrentState == PlayState4p.Ready)
        {
            currentCountDown -= Time.deltaTime;

            if (currentCountDown > 0)
            {
                int intNum = Mathf.CeilToInt(currentCountDown);
                countdownText.text = intNum.ToString();
            }
            else
            {
                StartPlay();
                countdownText.text = "START";

                StartCoroutine(WaitErase());
            }
        }
        // ステートがPlayのとき
        else if (CurrentState == PlayState4p.Play)
        {
            timer += Time.deltaTime;
            timerText.text = "Time:" + timer.ToString("000.000") + "s";
        }
        else
        {
            timerText.text = "Time:" + timer.ToString("000.000") + "s";
        }
    }

    // カウントダウン開始
    void CountDownStart()
    {
        currentCountDown = countStartTime;
        SetPlayState(PlayState4p.Ready);
        countdownText.gameObject.SetActive(true);

        if (carInputManager != null)
            carInputManager.enabled = false;
    }

    // ゲーム開始
    void StartPlay()
    {
        SetPlayState(PlayState4p.Play);

        if (carInputManager != null)
            carInputManager.enabled = true;
    }

    // START表示を消す
    IEnumerator WaitErase()
    {
        yield return new WaitForSeconds(2f);
        countdownText.gameObject.SetActive(false);
    }

    // ステート設定
    void SetPlayState(PlayState4p state)
    {
        CurrentState = state;
        player4p.CurrentState = state;
    }

    // ラップ数更新
    void OnLap()
    {
        lapText.text = player4p.LapCount + "/" + player4p.GoalLap;
    }

    // ゴール時
    void OnGoal()
    {
        SetPlayState(PlayState4p.Finish);

        countdownText.text = "GOAL";
        countdownText.gameObject.SetActive(true);

        // 車を停止
        if (carInputManager != null)
        {
            carInputManager.enabled = false;

            if (carInputManager.carController != null)
            {
                carInputManager.carController.ProvideInputs(0f, 0f, 0f);
            }
        }

        // ボタンを表示
        if (button != null)
        {
            button.SetActive(true);
        }
    }
}