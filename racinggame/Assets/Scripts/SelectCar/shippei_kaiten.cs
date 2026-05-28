using UnityEngine;

public class shippei_kaiten : MonoBehaviour
{
    [Header ("像のしっぺい部分のみ")]
    public Transform shippei;
    [Header("回転速度")]
    public float ver;

    void Start()
    {
        
    }

    void Update()
    {
        shippei.Rotate(0, ver* Time.deltaTime , 0);
    }
}
