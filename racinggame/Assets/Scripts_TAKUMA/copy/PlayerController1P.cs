using UnityEngine;
using UnityEngine.Events;


public class PlayerController1P : MonoBehaviour
{

    // ラップ数.
    public int LapCount = 0;


    // ゴール周回数.
    public int GoalLap = 2;


    // 逆走を判定するためのスイッチ.
    bool lapSwitch = false;


    // プレイステート.
    public GameController1P.PlayState CurrentState = GameController1P.PlayState.None;


    // ラップイベント.
    public UnityEvent LapEvent = new UnityEvent();


    // ゴール時イベント,
    public UnityEvent GoalEvent = new UnityEvent();

    // 前方ゲートコール.
    public void OnFrontGateCall()
    {
        // 通常のゲート通過.
        if (lapSwitch == true)
        {
            LapCount++;
            lapSwitch = false;

            if (LapCount > GoalLap) OnGoal();
            else LapEvent?.Invoke();

        }
        else // 逆走ゲート通過.
        {
            LapCount--;
            if (LapCount < 0) LapCount = 0;
            

            LapEvent?.Invoke();

        }
    }


    // 後方ゲートコール.
    public void OnBackGateCall()
    {
        if (lapSwitch == false)
        {
            lapSwitch = true;
        }
    }


    void FixedUpdate()
    {
        MoveUpdate();
        RotationUpdate();
    }

    
    void MoveUpdate() //移動処理
    {
        if (CurrentState != GameController1P.PlayState.Play) return;
　　　　　　　　　　　　　　　　
    }

    
    void RotationUpdate() // 回転処理.
    {
        if (CurrentState != GameController1P.PlayState.Play) return;
        
    }


    public void OnGoal() // ゴール時処理.
    {
        LapCount = 0;
        CurrentState = GameController1P.PlayState.Finish;
        GoalEvent?.Invoke();
    }

}
