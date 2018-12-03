using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour {

    [SerializeField]
    bool isOn1 = false;
    [SerializeField]
    bool isOn2 = false;

    [SerializeField]
    float m_Speed;

    private bool isCatch = false;

    [SerializeField]
    GameObject m_CatchObject;
    [SerializeField]
    GameObject m_CraneObject;

    [SerializeField]
    GameObject m_MovePoint1;
    private GameObject[] m_MoveObj1;
    int m_Count1;

    [SerializeField]
    GameObject m_MovePoint2;
    private GameObject[] m_MoveObj2;
    int m_Count2;

    int m_count = 0;


    private Vector3 m_oldPos;

    private Vector3 m_NextMovePoint;

    [SerializeField]
    float DISTANCE = 0.5f;


	// Use this for initialization
	void Start () {

        m_oldPos = m_CraneObject.transform.localPosition;
        m_NextMovePoint = m_oldPos;

        var num = m_MovePoint1.transform.childCount;
        m_MoveObj1 = new GameObject[num];
        for(int i = 0; i < num; i++)
        {
            m_MoveObj1[i] = m_MovePoint1.transform.GetChild(i).gameObject;
        }
        num = m_MovePoint2.transform.childCount;
        m_MoveObj2 = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            m_MoveObj2[i] = m_MovePoint2.transform.GetChild(i).gameObject;
        }

        m_Count1 = 0;
        m_Count2 = 0;


	}
	
	// Update is called once per frame
	void Update () {

        //--距離が離れているなら--//
        float dis = Vector3.Distance(m_CraneObject.transform.localPosition, m_NextMovePoint);
        if (dis > DISTANCE)
        {
            //--移動--//
            m_CraneObject.transform.localPosition = Vector3.Lerp(m_CraneObject.transform.localPosition, m_NextMovePoint, m_Speed*m_Speed*Time.deltaTime);
            if (isCatch)
            {
                m_CatchObject.transform.localPosition = Vector3.Lerp(m_CatchObject.transform.localPosition, m_NextMovePoint, m_Speed*Time.deltaTime);
            }
        }
        else
        {
            if (isOn1)
            {
                if (m_Count1 < m_MoveObj1.Length)
                {
                    m_NextMovePoint = m_MoveObj1[m_Count1].transform.localPosition;
                    m_Count1++;
                    m_count = 0;
                }
                else if (m_Count1 >= m_MoveObj1.Length && m_count != m_MoveObj1.Length)
                {
                    if (!isCatch && m_count==0)
                    {
                        dis = Vector3.Distance(m_CraneObject.transform.localPosition, m_CatchObject.transform.localPosition);
                        if (dis < DISTANCE)
                        {
                            isCatch = true;
                        }
                    }
                    else if(isCatch && m_count == 0)
                    {
                        isCatch = false;
                    }
                    m_NextMovePoint = m_MoveObj1[(m_MoveObj1.Length - 1) - m_count].transform.localPosition;
                    m_count++;
                }
                else
                {
                    m_count = 0;
                    m_Count1 = 0;
                    isOn1 = false;
                    m_NextMovePoint = m_oldPos;
                }
            }
            if (isOn2)
            {
                if (m_Count2 < m_MoveObj2.Length)
                {
                    m_NextMovePoint = m_MoveObj2[m_Count2].transform.localPosition;
                    m_Count2++;
                    m_count = 0;
                }
                else if (m_Count2 >= m_MoveObj2.Length && m_count != m_MoveObj1.Length)
                {
                    if (!isCatch && m_count==0)
                    {
                        dis = Vector3.Distance(m_CraneObject.transform.localPosition, m_CatchObject.transform.localPosition);
                        if (dis < DISTANCE)
                        {
                            isCatch = true;
                        }
                    }
                    else if (isCatch && m_count == 0)
                    {
                        isCatch = false;
                    }

                    m_NextMovePoint = m_MoveObj2[(m_MoveObj2.Length - 1) - m_count].transform.localPosition;
                    m_count++;
                }
                else
                {
                    m_count = 0;
                    m_Count2 = 0;
                    isOn2 = false;
                    m_NextMovePoint = m_oldPos;
                }
            }
        }



    }


    public void SwitchOn1()
    {
        if (isOn2) return;
        isOn1 = true;
    }
    public void SwitchOn2()
    {
        if (isOn1) return;
        isOn2 = true;
    }
    public void ResetPos()
    {
        m_NextMovePoint = m_oldPos;
        isOn1 = false;
        isOn2 = false;
    }

}
