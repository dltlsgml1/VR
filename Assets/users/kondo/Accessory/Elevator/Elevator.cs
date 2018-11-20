using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    [SerializeField]
    bool isUp = false;
    [SerializeField]
    float m_Speed;
    [SerializeField]
    float m_MoveDistans = 0.8f;


    //--ドアの初期位置格納用--//
    private Vector3 m_ElevatorObj_old;


    // Use this for initialization
    void Start()
    {
        //--初期位置格納--//
        m_ElevatorObj_old = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {


        //--上下処理--//
        //--上昇--//
        if (isUp)
        {
            Vector3 vec;
            if (this.transform.localPosition.y < m_ElevatorObj_old.y + m_MoveDistans)
            {
                vec = this.transform.localPosition;
                vec.y += m_Speed * Time.deltaTime;
                this.transform.localPosition = vec;
            }
            else
            {
                vec = m_ElevatorObj_old ;
                vec.y = m_MoveDistans;
                this.transform.localPosition = vec;
            }
        }
        //--下降--//
        else
        {
            Vector3 vec;
            if (this.transform.localPosition.y > m_ElevatorObj_old.y)
            {
                vec = this.transform.localPosition;
                vec.y -= m_Speed * Time.deltaTime;
                this.transform.localPosition = vec;
            }
            else
            {
                this.transform.localPosition = m_ElevatorObj_old;
            }
        }



    }


    public void DoorOpen() { isUp = true; }
    public void DoorClose() { isUp = false; }
    public void IsDoor() { isUp = !isUp; }

}
