using UnityEngine;

public class tire_kaiten : MonoBehaviour
{
    [Header ("タイヤの画像")]
    public Transform tire;
    [Header("回転速度")]
    public float ver;

    void Start()
    {
        
    }

    void Update()
    {
        tire.Rotate(0, 0, -ver* Time.deltaTime);
    }
}
