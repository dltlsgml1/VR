using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceLaser : MonoBehaviour {

    [SerializeField]
    GameObject m_LaserObj;
    [SerializeField,Range(1,5)]
    int m_LaserNum;

    private GameObject[] m_Laser;
    private float m_Distance;

    [SerializeField]
    GameObject m_Start;
    [SerializeField]
    GameObject m_End;

    [SerializeField]
    bool isOn; 


    // Use this for initialization
    void Start () {

        m_Distance = m_End.transform.localPosition.z - m_Start.transform.localPosition.z;
        m_Distance = Mathf.Abs(m_Distance);

        m_Laser = new GameObject[5];
        for (int i = 0; i < m_Laser.Length; i++)
        {
            m_Laser[i] = Instantiate(m_LaserObj,this.transform);
            var scale = m_Laser[i].transform.localScale;
            scale.z = m_Distance;
            m_Laser[i].transform.localScale = scale;
            m_Laser[i].SetActive(false);
        }




    }
	
	// Update is called once per frame
	void Update () {


        if (isOn)
        {
            var pos = m_Start.transform.localPosition;
                pos.z -= m_Distance / 2;
            if (m_LaserNum == 1)
            {
                m_Laser[0].transform.localPosition = pos;
            }
            if (m_LaserNum == 2)
            {
                pos.y = m_Start.transform.localScale.y / 4;
                m_Laser[0].transform.localPosition = pos;
                pos.y = -(m_Start.transform.localScale.y / 4);
                m_Laser[1].transform.localPosition = pos;
            }
            if (m_LaserNum == 3)
            {
                m_Laser[0].transform.localPosition = pos;
                pos.y = m_Start.transform.localScale.y / 4;
                m_Laser[1].transform.localPosition = pos;
                pos.y = -(m_Start.transform.localScale.y / 4);
                m_Laser[2].transform.localPosition = pos;
            }
            if (m_LaserNum == 4)
            {
                pos.y = m_Start.transform.localScale.y / 2;
                m_Laser[0].transform.localPosition = pos;
                pos.y = m_Start.transform.localScale.y / 5;
                m_Laser[1].transform.localPosition = pos;
                pos.y = -(m_Start.transform.localScale.y / 5);
                m_Laser[2].transform.localPosition = pos;
                pos.y = -(m_Start.transform.localScale.y/2);
                m_Laser[3].transform.localPosition = pos;
            }
            if (m_LaserNum == 5)
            {
                m_Laser[0].transform.localPosition = pos;
                pos.y = m_Start.transform.localScale.y / 2;
                m_Laser[1].transform.localPosition = pos;
                pos.y = m_Start.transform.localScale.y / 4;
                m_Laser[2].transform.localPosition = pos;
                pos.y = -(m_Start.transform.localScale.y / 4);
                m_Laser[3].transform.localPosition = pos;
                pos.y = -(m_Start.transform.localScale.y / 2);
                m_Laser[4].transform.localPosition = pos;

            }

            for (int i = 0; i < m_Laser.Length; i++)
            {
                if (i < m_LaserNum)
                {
                    m_Laser[i].SetActive(true);
                }
                else m_Laser[i].SetActive(false);
            }
        }
        else
        {
            for(int i = 0; i < m_Laser.Length; i++)
            {
                m_Laser[i].SetActive(false);
            }
        }

		
	}


    public void SwitchOn() { isOn = true; }
    public void SwitchOff() { isOn = false; }

}
