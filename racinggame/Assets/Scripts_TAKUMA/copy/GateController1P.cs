using UnityEngine;


public class GateController1P : MonoBehaviour
{
    // 前方のコライダーコール.
    [SerializeField] ColliderCallReceiver frontColliderCall = null;


    //後方のコライダーコール.
    [SerializeField] ColliderCallReceiver backColliderCall = null;


    void Start()
    {
        frontColliderCall.TriggerEnterEvent.AddListener(OnFrontTriggerEnter);
        backColliderCall.TriggerEnterEvent.AddListener(OnBackTriggerEnter);
    }



    // 前方トリガーエンターコール.
    void OnFrontTriggerEnter(Collider col)
    {
        // 侵入したコライダーのゲームオブジェクトのタグがPlayer.
        if (col.gameObject.tag == "Player1P")
        {
            var player = col.gameObject.GetComponent<PlayerController1P>();
            player.OnFrontGateCall();
        }
    }


    
    // 後方トリガーエンターコール.
    void OnBackTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player1P")
        {
            var player = col.gameObject.GetComponent<PlayerController1P>();
            player.OnBackGateCall();
        }
    }


}