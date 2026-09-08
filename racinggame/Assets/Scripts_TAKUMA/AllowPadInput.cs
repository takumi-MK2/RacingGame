using UnityEngine;
using UnityEngine.InputSystem;

public class AllowPadInput : MonoBehaviour
{
    public int padNum;
    public bool CanInput
    {
        get; private set;
    }

    Gamepad gamepad;

    void Start()
    {
        gamepad = Gamepad.all[padNum];
    }

    void Update()
    {
        //var gamepad = Gamepad.all[padNum];

        Debug.Log(padNum);

        //if (gamepad == Gamepad.all[padNum])
        //{
        //    CanInput = true;
        //}
        //else CanInput = false;
        CanInput = false;
    }
}