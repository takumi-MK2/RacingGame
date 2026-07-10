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

    // プレイヤー
    [SerializeField] PlayerController player = null;

    // 車の入力マネージャー
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

        if (carInputManager != null)
            carInputManager.enabled = false;
    }

    // ゲーム開始
    void StartPlay()
    {
        Debug.Log("START");
        SetPlayState(PlayState.Play);

        if (carInputManager != null)
            carInputManager.enabled = true;
    }

    // START表示を消す
    IEnumerator WaitErase()
    {
        yield return new WaitForSeconds(2f);
        countdownText.gameObject.SetActive(false);
    }

    // ゴール後3秒待ってシーン遷移
    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("SelectStage");
    }

    // ステート設定
    void SetPlayState(PlayState state)
    {
        CurrentState = state;
        player.CurrentState = state;
    }

    // ラップ数更新
    void OnLap()
    {
        lapText.text = player.LapCount + "/" + player.GoalLap;
    }

    // ゴール時
    void OnGoal()
    {
        SetPlayState(PlayState.Finish);

        countdownText.text = "GOAL";
        countdownText.gameObject.SetActive(true);

        if (carInputManager != null)
        {
            carInputManager.enabled = false;

            if (carInputManager.carController != null)
            {
                carInputManager.carController.ProvideInputs(0f, 0f, 0f);
            }
        }

        // 3秒後にシーン遷移
        StartCoroutine(ChangeSceneAfterDelay());
    }
}