using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    [SerializeField]
    GameObject Player;
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
        m_ElevatorObj_old = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance((Player.transform.position), (m_ElevatorObj_old)) < 3.0f)
        {
            isUp = true;
        }
        

        //--上下処理--//
        //--上昇--//
        if (isUp)
        {
            Vector3 vec;
            Vector3 vec2;
            if (this.transform.position.y < m_ElevatorObj_old.y + m_MoveDistans)
            {
                vec = this.transform.position;
                vec2 = Player.transform.position;
                vec.y += m_Speed * Time.deltaTime;
                vec2.y += m_Speed * Time.deltaTime;
                this.transform.position = vec;
                Player.transform.position = vec2;
            }
            else
            {
                vec = m_ElevatorObj_old ;
                vec.y = m_ElevatorObj_old.y +  m_MoveDistans;
                this.transform.position = vec;
            }
        }
        //--下降--//
        else
        {
            Vector3 vec;
            if (this.transform.position.y > m_ElevatorObj_old.y)
            {
                vec = this.transform.position;
                vec.y -= m_Speed * Time.deltaTime;
                this.transform.position = vec;
            }
            else
            {
                this.transform.position = m_ElevatorObj_old;
            }
        }



    }


    public void DoorOpen() { isUp = true; }
    public void DoorClose() { isUp = false; }
    public void IsDoor() { isUp = !isUp; }

}
