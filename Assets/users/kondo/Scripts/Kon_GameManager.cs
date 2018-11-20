using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kon_GameManager : MonoBehaviour {


    [Header("Stage関係")]
    //--テレポートスクリプト--//
    [SerializeField]
    kon_tele m_Kontere;

    //--ステージのスタート位置--//
    [SerializeField]
    GameObject[] m_StageStart;
    enum STAGENO
    {
        STAGE01=0,
        STAGE02,

        STAGEMAX
    }
    int m_NowStage;

    [Header("UI関係")]
    //--UI--//
    [SerializeField]
    Text m_CountText;
    [SerializeField]
    Text m_ClearText;



    //--時間管理--//
    [SerializeField]
    Image m_FadeObj;
    [SerializeField]
    Image m_FadeObj_RedColor;
    [SerializeField]
    float m_FadeTime;
    bool isFadeOut;
    bool isFadeOut_Red;
    





	// Use this for initialization
	void Start () {
        m_NowStage = (int)STAGENO.STAGE01;
        m_Kontere.SetNextStage(m_StageStart[m_NowStage]);
        isFadeOut = false;
    }
	
	// Update is called once per frame
	void Update () {

        Color col = new Color(1, 1, 1, 1);
        if(m_Kontere.m_LastMoveCount==0)
        {
            col.g = col.b = 0;
        }
        if (m_Kontere.m_LastMoveCount == 1)
        {
            col.b = 0;
        }
        m_CountText.color = col;
        m_CountText.text = "残り移動回数 : " + m_Kontere.m_LastMoveCount.ToString();


        //--クリア フェードアウト--//
        if (m_Kontere.ISGoal())
        {
            isFadeOut = true;
            m_ClearText.enabled = true;
        }
        //--ゲームオーバー フェードアウト--//
        if (m_Kontere.ISGameOver())
        {
            isFadeOut_Red = true;
            m_FadeObj_RedColor.enabled = true;
        }

        Fade();
        FadeRed();



	}


    private void Fade()
    {
        if (isFadeOut)
        {
            //--フェード終了　ステージ遷移--//
            if (m_FadeObj.color.a >= 1)
            {
                m_NowStage += 1;
                //--最終ステージクリア--//
                if (m_NowStage >= (int)STAGENO.STAGEMAX)
                {
                    SceneManager.LoadScene("kon_result");
                    m_NowStage = 0;
                }
                m_Kontere.SetNextStage(m_StageStart[m_NowStage], m_StageStart[m_NowStage].GetComponent<kon_StageStartMoveCount>().m_StageStartMoveCount);
                isFadeOut = false;
                m_ClearText.enabled = false;

                var col = m_FadeObj.color;
                col.a = 1;
                m_FadeObj.color = col;
            }
            else
            {
                var col = m_FadeObj.color;
                col.a = col.a + (m_FadeTime * Time.deltaTime);
                m_FadeObj.color = col;
            }

        }
        //--フェードイン--//
        else
        {
            if (m_FadeObj.color.a <= 0)
            {
                var col = m_FadeObj.color;
                col.a = 0;
                m_FadeObj.color = col;
            }
            else
            {
                var col = m_FadeObj.color;
                col.a = col.a - (m_FadeTime * Time.deltaTime);
                m_FadeObj.color = col;
            }
        }
    }

    private void FadeRed()
    {
        if (isFadeOut_Red)
        {
            //--フェード終了　スタートへ--//
            if (m_FadeObj_RedColor.color.a >= 1)
            {
                m_Kontere.ResetStage(m_StageStart[m_NowStage], m_StageStart[m_NowStage].GetComponent<kon_StageStartMoveCount>().m_StageStartMoveCount);
                isFadeOut = false;
                m_ClearText.enabled = false;

                var col = m_FadeObj_RedColor.color;
                col.a = 0;
                m_FadeObj_RedColor.color = col;
                isFadeOut_Red = false;
            }
            else
            {
                var col = m_FadeObj_RedColor.color;
                col.a = col.a + (m_FadeTime * Time.deltaTime);
                m_FadeObj_RedColor.color = col;
            }

        }
        ////--フェードイン--//
        //else
        //{
        //    if (m_FadeObj_RedColor.color.a <= 0)
        //    {
        //        var col = m_FadeObj_RedColor.color;
        //        col.a = 0;
        //        m_FadeObj_RedColor.color = col;
        //    }
        //    else
        //    {
        //        var col = m_FadeObj_RedColor.color;
        //        col.a = col.a - (m_FadeTime * Time.deltaTime);
        //        m_FadeObj_RedColor.color = col;
        //    }
        //}
    }

}


