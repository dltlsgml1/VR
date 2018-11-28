using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabObject_old : MonoBehaviour {


    //コントローラーと当たっているオブジェクト
    [SerializeField]
    private GameObject collidingObject;
    //手に持っているオブジェクト
    private GameObject objectInHand;


    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Triggerを押されたらコントローラーと当たっているオブジェクト
        //(Rigidbody)があればオブジェクトを握る関数を呼び出す。
        if (Controller.GetHairTriggerDown())
        {
            Debug.Log("トリガー押した");

            if (collidingObject)
            {
                Debug.Log("つかむ");
                
                GrabObject();
            }
        }
        //Triggerを放して手にオブジェクトがあればオブジェクトが放される
        if (Controller.GetHairTriggerUp())
        {
            Debug.Log("トリガー放した");
            if (objectInHand)
            {
                Debug.Log("はなす");
                ReleaseObject();
            }
        }
    }


    private void SetCollidingObject(Collider col)
    {
        //常にプレイヤーが手に物を持っているまたは
        //当たっているオブジェクトはrigidbodyがない場合何もしない
        if (collidingObject || !col.GetComponent<Rigidbody>()) { return; }
        //掴むことが可能なオブジェクトとして取得する
        collidingObject = col.gameObject;
    }



    private void GrabObject()
    {
        //掴むことが可能なオブジェクトを手に持っている変数にコピーして
        //collidingObjectの参照を消す
        objectInHand = collidingObject;
        collidingObject = null;
        //オブジェクトをコントローラーにつけるためにジョイントを作り
        //オブジェクトのRigidbodyをジョイントにコピーする
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }
    //ジョイントを生成する
    //簡単に外せないようにBreakForceとbreaktorqueを高い値にする
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        //ジョイントがあることを確認する
        if (GetComponent<FixedJoint>())
        {
            //ジョイントについているRigidbodyをnullにして
            //ジョイントを削除する
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            //コントローラーのスピードと回転スピードをオブジェクトにコピーする
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        //手に持っているオブジェクトの参照をnullにする
        objectInHand = null;
}





    //コントローラーがColliderがついているオブジェクトをあたると
    //そのオブジェクトをつかめるようになる
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTiggerEnter");
        SetCollidingObject(other);
    }
    //１と同じ。バグ予防
    public void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTiggerStay");
        SetCollidingObject(other);
    }
    //オブジェクトと離れていて掴むことが可能なものの参照を消す
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTiggerExit");

        if (!collidingObject) { return; }

        collidingObject = null;
    }

}
