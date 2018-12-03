using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kon_GameManager : MonoBehaviour {

    public GameObject Player;

    [Header("Stage関係")]
    //--テレポートスクリプト--//


    //--ステージのスタート位置--//
    [SerializeField]
    GameObject[] m_StageStart;
    enum STAGENO
    {
        STAGE01=0,
        STAGE02,

        STAGEMAX
    }

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
      
        isFadeOut = false;
    }
	
	// Update is called once per frame
	void Update () {

        

        int MoveCount = Player.GetComponent<LaserPointer>().MoveCount;
        Color col = new Color(1, 1, 1, 1);
        if(MoveCount == 0)
        {
            col.g = col.b = 0;
        }
        if (MoveCount == 1)
        {
            col.b = 0;
        }
        m_CountText.color = col;
        m_CountText.text = "残り移動回数 : " + MoveCount.ToString();


        //--クリア フェードアウト--//
        if (Player.GetComponent<LaserPointer>().IsGoal)
        {
            isFadeOut = true;
            m_ClearText.enabled = true;
        }
        //--ゲームオーバー フェードアウト--//
        if (Player.GetComponent<LaserPointer>().IsGameOver)
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

                SceneManager.LoadScene("kon_result");
                
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


