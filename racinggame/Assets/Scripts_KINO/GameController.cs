using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AshVP; //InputManagerのネームスペースを利用する

public class GameController : MonoBehaviour
{

    // ラップテキスト
    [SerializeField] Text lapText = null;


    // ゲームステート
    public enum PlayState
    {
        None,
        Ready,
        Play,
        Finish,
    }

    // 現在のステート.
    public PlayState CurrentState = PlayState.None;

    // カウントダウンスタートタイム.
    [SerializeField] int countStartTime = 5;

    // カウントダウンテキスト.
    [SerializeField] Text countdownText = null;

    // タイマーテキスト.
    [SerializeField] Text timerText = null;

    // カウントダウンの現在値.
    float currentCountDown = 0;

    // ゲーム経過時間現在値.
    float timer = 0;

    // プレイヤー.
    [SerializeField] PlayerController player = null;

    // 車の入力マネージャーへの参照
    [SerializeField] private InputManager_AshVP carInputManager = null;


    void Start()
    {
        CountDownStart();

        player.LapEvent.AddListener(OnLap);
        player.GoalEvent.AddListener(OnGoal);

        timerText.text = "Time : 000.000 s";
        lapText.text = "1/" + player.GoalLap;
    }


    void Update()
    {
        timerText.text = "Time : 000.000 s";

        // ステートがReadyのとき
        if (CurrentState == PlayState.Ready)
        {
            // 時間を引いていく
            currentCountDown -= Time.deltaTime;

            int intNum = 0;

            // カウントダウン中
            if (currentCountDown <= (float)countStartTime && currentCountDown > 0)
            {
                // int(整数)に
                intNum = (int)Mathf.Ceil(currentCountDown);
                countdownText.text = intNum.ToString();
            }
            else if (currentCountDown <= 0)
            {
                // 開始
                StartPlay();
                intNum = 0;
                countdownText.text = "START";

                // Start表示を少しして消す
                StartCoroutine(WaitErase());
            }
        }
        // ステートがPlayのとき
        else if (CurrentState == PlayState.Play)
        {
            timer += Time.deltaTime;
            timerText.text = "Time : " + timer.ToString("000.000") + " s";
        }
        else
        {
            timer = 0;
            timerText.text = "Time : 000.000 s";
        }
        
    }


    // カウントダウンスタート.
    void CountDownStart()
    {
        currentCountDown = (float)countStartTime;
        SetPlayState(PlayState.Ready);
        countdownText.gameObject.SetActive(true);

        // カウントダウン中は入力を受け付けないようにコンポーネントをOFFにする
        if (carInputManager != null) carInputManager.enabled = false;
    }



    // ゲームスタート.
    void StartPlay()
    {
        Debug.Log("START");
        SetPlayState(PlayState.Play);

        // スタートしたら入力を受け付けるようにコンポーネントをONにする
        if (carInputManager != null) carInputManager.enabled = true;
    }



    // 少し待ってからStart表示を消す.
    IEnumerator WaitErase()
    {
        yield return new WaitForSeconds(2f);
        countdownText.gameObject.SetActive(false);
    }



    /// 現在のステートの設定.
    void SetPlayState(PlayState state)
    {
        CurrentState = state;
        player.CurrentState = state;
    }



    // ラップ数変化時処理.
    void OnLap()
    {
        var current = player.LapCount;
        var goalLap = player.GoalLap;

        lapText.text = "" + current + "/" + goalLap;
    }



    /// ゴール時処理.
    void OnGoal()
    {
        CurrentState = PlayState.Finish;
        countdownText.text = "GOAL";
        countdownText.gameObject.SetActive(true);

        // ゴールしたら入力をカットするためにコンポーネントをOFFにする
        if (carInputManager != null)
        {
            carInputManager.enabled = false;

            if (carInputManager.carController != null)
            {
                carInputManager.carController.ProvideInputs(0f, 0f, 0f);
            }

        }

    }

}