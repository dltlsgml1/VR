using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour {

    [SerializeField]
    bool isOpen = false;
    [SerializeField]
    float m_OpenSpeed;

    [SerializeField]
    Transform m_LeftDoor;
    [SerializeField]
    Transform m_LeftDoorFixed;
    [SerializeField]
    Transform m_RightDoor;
    [SerializeField]
    Transform m_RightDoorFixed;

    //--ドアの初期位置格納用--//
    private Vector3 m_Leftpos_old;
    private Vector3 m_Rightpos_old;


    [Header("オプション")]
    [SerializeField]
    bool IsAutoClose = false;
    //--AutoClose用変数--//
    [SerializeField]
    float m_AutoCloseTime;
    float m_AutoCloseCount;


    // Use this for initialization
    void Start () {
        //--初期位置格納--//
        m_Leftpos_old = m_LeftDoor.localPosition;
        m_Rightpos_old = m_RightDoor.localPosition;
        m_AutoCloseCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(this.GetComponent<GimmickBaseClass>().isGimmickSet==true)
        {
            isOpen = true;
        }

        //--開閉処理--//
        //--開く--//
        if (isOpen)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            //--距離が離れているなら--//
            //LEFT
            float dis = Vector3.Distance(m_LeftDoor.localPosition, m_LeftDoorFixed.localPosition);
            if (dis > 0)
            {
                //--移動--//
                m_LeftDoor.localPosition = Vector3.Lerp(m_LeftDoor.localPosition, m_LeftDoorFixed.localPosition, m_OpenSpeed * Time.deltaTime);
            }

            //--距離が離れているなら--//
            dis = Vector3.Distance(m_RightDoor.localPosition, m_RightDoorFixed.localPosition);
            if (dis > 0)
            {
                //--移動--//
                m_RightDoor.localPosition = Vector3.Lerp(m_RightDoor.localPosition, m_RightDoorFixed.localPosition, m_OpenSpeed * Time.deltaTime);
            }
            if(dis<=0)
            {
               
            }
        }
        //--閉める--//
        else
        {
            //--距離が離れているなら--//
            //LEFT
            float dis = Vector3.Distance(m_LeftDoor.localPosition, m_Leftpos_old);
            if (dis > 0)
            {
                //--移動--//
                m_LeftDoor.localPosition = Vector3.Lerp(m_LeftDoor.localPosition, m_Leftpos_old, m_OpenSpeed * Time.deltaTime);
            }

            //--距離が離れているなら--//
            dis = Vector3.Distance(m_RightDoor.localPosition, m_Rightpos_old);
            if (dis > 0)
            {
                //--移動--//
                m_RightDoor.localPosition = Vector3.Lerp(m_RightDoor.localPosition, m_Rightpos_old, m_OpenSpeed * Time.deltaTime);
            }
        }

        AutoClose();

	}


    private void AutoClose()
    {
        //--自動モードでないとき　ドアが閉まっているときは無視--//
        if (!IsAutoClose || !isOpen)
        {
            m_AutoCloseCount = 0;
            return;
        }

        m_AutoCloseCount +=  1 * Time.deltaTime;

        if(m_AutoCloseCount > m_AutoCloseTime)
        {
            m_AutoCloseCount = 0;
            isOpen = false;
            this.GetComponent<BoxCollider>().enabled = true;
        }


    }


    public void DoorOpen() { isOpen = true; }
    public void DoorClose() { isOpen = false; }
    public void IsDoor() { isOpen = !isOpen; }

}
