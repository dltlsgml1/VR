using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour {

    [SerializeField]
    bool isOpen = false;
    [SerializeField]
    float m_OpenSpeed;
    [SerializeField]
    float m_OpenDistans;

    [SerializeField]
    Transform m_LeftDoor;
    [SerializeField]
    Transform m_RightDoor;

    Vector3 m_Leftpos_old;
    Vector3 m_Rightpos_old;


    // Use this for initialization
    void Start () {
        m_Leftpos_old = m_LeftDoor.localPosition;
        m_Rightpos_old = m_RightDoor.localPosition;
    }
	
	// Update is called once per frame
	void Update () {



        if (isOpen)
        {
            Vector3 vec;
            if(m_LeftDoor.localPosition.x < m_Leftpos_old.x + m_OpenDistans)
            {
                vec = m_LeftDoor.localPosition;
                vec.x += m_OpenSpeed * Time.deltaTime;
                m_LeftDoor.localPosition = vec; 
            }
            if (m_RightDoor.localPosition.x > m_Rightpos_old.x - m_OpenDistans)
            {
                vec = m_RightDoor.localPosition;
                vec.x -= m_OpenSpeed * Time.deltaTime;
                m_RightDoor.localPosition = vec;
            }
        }
        else
        {
            Vector3 vec;
            if (m_LeftDoor.localPosition.x > m_Leftpos_old.x)
            {
                vec = m_LeftDoor.localPosition;
                vec.x -= m_OpenSpeed * Time.deltaTime;
                m_LeftDoor.localPosition = vec;
            }
            if (m_RightDoor.localPosition.x < m_Rightpos_old.x)
            {
                vec = m_RightDoor.localPosition;
                vec.x += m_OpenSpeed * Time.deltaTime;
                m_RightDoor.localPosition = vec;
            }
        }



	}


    public void DoorOpen() { isOpen = true; }
    public void DoorClose() { isOpen = false; }

}
