using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// コライダーコールバックの受信ユーティリティクラス.
public class ColliderCallReceiver : MonoBehaviour
{

    // コライダーイベント定義クラス
    public class CollisionEvent : UnityEvent<Collision> { }

    // コライダーエンターイベント
    public CollisionEvent CollisionEnterEvent = new CollisionEvent();
    // コライダーステイイベント
    public CollisionEvent CollisionStayEvent = new CollisionEvent();
    // コライダーイグジットイベント
    public CollisionEvent CollisionExitEvent = new CollisionEvent();





    // トリガーイベント定義クラス
    public class TriggerEvent : UnityEvent<Collider> { }

    // トリガーエンターイベント
    public TriggerEvent TriggerEnterEvent = new TriggerEvent();
    // トリガーステイイベント
    public TriggerEvent TriggerStayEvent = new TriggerEvent();
    // トリガーイグジットイベント
    public TriggerEvent TriggerExitEvent = new TriggerEvent();



    void Start()
    {

    }



    // コライダーエンターコールバック.
    void OnCollisionEnter(Collision col)
    {
        CollisionEnterEvent?.Invoke(col);
    }


   
    // コライダーステイコールバック.
    void OnCollisionStay(Collision col)
    {
        CollisionStayEvent?.Invoke(col);
    }


    
    // コライダーイグジットコールバック.
    void OnCollisionExit(Collision col)
    {
        CollisionExitEvent?.Invoke(col);
    }

   

    // トリガーエンターコールバック.
    void OnTriggerEnter(Collider other)
    {
        TriggerEnterEvent?.Invoke(other);
    }



    /// トリガーステイコールバック.
    void OnTriggerStay(Collider other)
    {
        TriggerStayEvent?.Invoke(other);
    }


   
    // トリガーイグジットコールバック.
    void OnTriggerExit(Collider other)
    {
        TriggerExitEvent?.Invoke(other);
    }



}