using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Sousa : MonoBehaviour
{
    public GameObject car;
    public Transform groundCheck;
    public Text tuyosa;

    Rigidbody rb;

    void Awake()
    {
        rb=GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        var gamepad = Gamepad.all[0];

        if(gamepad == Gamepad.all[0])
        {
            Operate();
        }
    }

    void Operate()
    {
        var gamepad = Gamepad.all[0];

        if (gamepad.rightTrigger.ReadValue() != 0)
        {
            //rb.AddForceAtPosition(transform.forward * gamepad.rightTrigger.ReadValue(), groundCheck.position);
            car.transform.forward = new(gamepad.rightTrigger.ReadValue()*0.001f, 0, 0);
            tuyosa.text = gamepad.rightTrigger.ReadValue().ToString();
        }
    }





}