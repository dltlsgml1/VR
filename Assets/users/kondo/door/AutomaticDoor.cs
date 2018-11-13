using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour {

    [SerializeField]
    bool isOpen = false;
    [SerializeField]
    float m_OpenSpeed;
    //[SerializeField]
    float m_OpenDistans =0.8f;

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


    // Use this for initialization
    void Start () {
        //--初期位置格納--//
        m_Leftpos_old = m_LeftDoor.localPosition;
        m_Rightpos_old = m_RightDoor.localPosition;
    }
	
	// Update is called once per frame
	void Update () {


        //--開閉処理--//
        //--開く--//
        if (isOpen)
        {
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



	}


    public void DoorOpen() { isOpen = true; }
    public void DoorClose() { isOpen = false; }
    public void IsDoor() { isOpen = !isOpen; }

}
