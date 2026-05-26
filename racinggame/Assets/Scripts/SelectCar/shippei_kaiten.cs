using UnityEngine;

public class shippei_kaiten : MonoBehaviour
{
    [Header ("像のしっぺい部分のみ")]
    public Transform shippei;

    void Start()
    {
        
    }

    void Update()
    {
        shippei.Rotate(0, 12*Time.deltaTime , 0);
    }
}
