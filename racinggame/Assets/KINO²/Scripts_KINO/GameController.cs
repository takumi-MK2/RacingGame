using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AshVP;
using UnityEngine.SceneManagement;

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

    // 現在のステート
    public PlayState CurrentState = PlayState.None;

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

    private int goalPlayerCount = 0;

    // プレイヤー01
    [SerializeField] PlayerController player01 = null;
    [SerializeField] private InputManager_AshVP carInputManager01 = null;

    // プレイヤー02
    [SerializeField] PlayerController02 player02 = null;
    [SerializeField] private InputManager_AshVP carInputManager02 = null;

    // プレイヤー03
    [SerializeField] PlayerController03 player03 = null;
    [SerializeField] private InputManager_AshVP carInputManager03 = null;

    // プレイヤー04
    [SerializeField] PlayerController04 player04 = null;
    [SerializeField] private InputManager_AshVP carInputManager04 = null;


    // ゴール後に表示するボタンの親オブジェクト
    [SerializeField] private GameObject button;

    void Start()
    {
        CountDownStart();

        // Player1
        player01.LapEvent.AddListener(OnLap);
        player01.GoalEvent.AddListener(OnGoalPlayer01);

        // Player2
        player02.LapEvent.AddListener(OnLap);
        player02.GoalEvent.AddListener(OnGoalPlayer02);

        // Player3
        player03.LapEvent.AddListener(OnLap);
        player03.GoalEvent.AddListener(OnGoalPlayer03);

        // Player4
        player04.LapEvent.AddListener(OnLap);
        player04.GoalEvent.AddListener(OnGoalPlayer04);

        timerText.text = "Time : 000.000 s";
        OnLap();

        if (button != null)
        {
            button.SetActive(false);
        }
    }


    void Update()
    {
        // ステートがReadyのとき
        if (CurrentState == PlayState.Ready)
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
        else if (CurrentState == PlayState.Play)
        {
            timer += Time.deltaTime;
            timerText.text = "Time : " + timer.ToString("000.000") + " s";
        }
        else
        {
            timerText.text = "Time : " + timer.ToString("000.000") + " s";
        }
    }


    // カウントダウン開始
    void CountDownStart()
    {
        currentCountDown = countStartTime;
        SetPlayState(PlayState.Ready);
        countdownText.gameObject.SetActive(true);

        if (carInputManager01 != null) carInputManager01.enabled = false;
        if (carInputManager02 != null) carInputManager02.enabled = false;
        if (carInputManager03 != null) carInputManager03.enabled = false;
        if (carInputManager04 != null) carInputManager04.enabled = false;
    }

    // ゲーム開始
    void StartPlay()
    {
        SetPlayState(PlayState.Play);

        if (carInputManager01 != null)
            carInputManager01.enabled = true;

        if (carInputManager02 != null)
            carInputManager02.enabled = true;

        if (carInputManager03 != null)
            carInputManager03.enabled = true;

        if (carInputManager04 != null)
            carInputManager04.enabled = true;
    }

    // START表示を消す
    IEnumerator WaitErase()
    {
        yield return new WaitForSeconds(2f);
        countdownText.gameObject.SetActive(false);
    }

    // ステート設定
    void SetPlayState(PlayState state)
    {
        CurrentState = state;
        player01.CurrentState = state;
        player02.CurrentState = state;
        player03.CurrentState = state;
        player04.CurrentState = state;
    }

    // ラップ数更新
    void OnLap()
    {
        lapText.text = player01.LapCount + "/" + player01.GoalLap;
    }

    // ゴール時
    void OnGoal(InputManager_AshVP input)
    {
        // このプレイヤーだけ停止
        if (input != null)
        {
            input.enabled = false;

            if (input.carController != null)
            {
                input.carController.ProvideInputs(0f, 0f, 0f);
            }
        }

        // ゴール人数を加算
        goalPlayerCount++;

        Debug.Log("Goal Player : " + goalPlayerCount);

        // 全員ゴールしたら終了
        if (goalPlayerCount >= 4)
        {
            SetPlayState(PlayState.Finish);

            countdownText.text = "FINISH";
            countdownText.gameObject.SetActive(true);

            if (button != null)
            {
                button.SetActive(true);
            }
        }
    }

    void OnGoalPlayer01()
    {
        OnGoal(carInputManager01);
    }

    void OnGoalPlayer02()
    {
        OnGoal(carInputManager02);
    }

    void OnGoalPlayer03()
    {
        OnGoal(carInputManager03);
    }

    void OnGoalPlayer04()
    {
        OnGoal(carInputManager04);
    }
}