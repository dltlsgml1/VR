using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kon_MoveEffect : MonoBehaviour {


    //--カメラ--//
    private Camera m_camera;
    private float m_Fov_initial;
    private float m_Fov_Now;


    //--フェードイン・アウト用変数--//
    private Image m_FadePanel;
    private Color m_col;

    [SerializeField ,Range(0.1f,3f)]
    float m_FadeTime = 1;

    public bool isFade;// { get; set;}
    private bool isFadeReverse;



    // Use this for initialization
    void Start () {


        //--カメラ取得--//
        m_camera = this.transform.GetComponent<Camera>();
        m_Fov_initial = m_camera.fieldOfView;
        m_Fov_Now = m_Fov_initial;


        //--フェードパネル取得--//
        m_FadePanel = GameObject.Find("Canvas").transform.Find("Fade").GetComponent<Image>();
        m_col = m_FadePanel.color;


        isFade = false;
        isFadeReverse = false;

    }
	
	// Update is called once per frame
	void Update () {

        //--フェードする--//
        if (isFade)
        {
            //--暗くする--//
            if (!isFadeReverse)
            {

                m_col.a += m_FadeTime * Time.deltaTime;
                if(m_col.a >= 1)
                {
                    m_col.a = 1;
                    isFadeReverse = true;
                }

                m_FadePanel.color = m_col;


                if (m_Fov_Now > 0)
                {
                    m_Fov_Now -= (m_Fov_initial * m_FadeTime) * Time.deltaTime;
                }
                else
                {
                    m_Fov_Now = 0;
                }
                m_camera.fieldOfView = m_Fov_Now;


            }
            //--明るくする--//
            else
            {
                m_col.a -= m_FadeTime * Time.deltaTime;
                if (m_col.a <= 0)
                {
                    m_col.a = 0;
                    isFadeReverse = false;
                    isFade = false;
                }

                m_FadePanel.color = m_col;


                //m_Fov_Now += (m_Fov_initial / m_FadeTime) * Time.deltaTime;
                //m_camera.fieldOfView = m_Fov_Now;
                m_Fov_Now = m_Fov_initial;
                m_camera.fieldOfView = m_Fov_initial;
            }
        }
        else
        {
            m_col.a = 0;
            m_FadePanel.color = m_col;

            m_camera.fieldOfView = m_Fov_initial;
            m_Fov_Now = m_Fov_initial;
            isFadeReverse = false;
        }
	}
}
