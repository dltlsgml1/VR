﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabehakai : MonoBehaviour {

    //--透明になるフラグ--//
    [SerializeField]
    bool isOn;
    //--リセットフラグ--//
    [SerializeField]
    bool isReset;


    //--デバッグ用（後で消す）--//
    [SerializeField]
    bool Debug_On;

    //--かべのマテリアル--//
    private Material m_mat;
    //--かべ--//
    [SerializeField]
    GameObject m_Kabe;
    //--がれきのマテリアル--//
    [SerializeField]
    Material m_GarekiMat;


    private Rigidbody m_RB;
    //--リセット用変数--//
    private Vector3 m_OldPos;
    private Quaternion m_OldRot;


	// Use m_Kabe for initialization
	void Start () {

        m_mat = m_Kabe.GetComponent<MeshRenderer>().material;
        //--まだ動かさないので動きを固定--//
        m_RB = m_Kabe.GetComponent<Rigidbody>();
        m_RB.constraints = RigidbodyConstraints.FreezeAll;
        //--リセット用の値取得--//
        m_OldPos = m_Kabe.transform.localPosition;
        m_OldRot = m_Kabe.transform.localRotation;

    }
	
	// Update is called once per frame
	void Update () {


        if (isOn)
        {
            var col = m_mat.color;
            col.a -= Time.deltaTime;
            if (col.a <= 0)
            {
                col.a = 0;
            }
            m_mat.color = col;

            col = m_GarekiMat.color;
            col.a += Time.deltaTime;
            if (col.a >= 1)
            {
                col.a = 1;
            }
            m_GarekiMat.color = col;
        }
        else
        {
            var col = m_mat.color;
            col.a += Time.deltaTime;
            if (col.a >= 1)
            {
                col.a = 1;
            }
            m_mat.color = col;

            col = m_GarekiMat.color;
            col.a -= Time.deltaTime;
            if (col.a <= 0)
            {
                col.a = 0;
            }
            m_GarekiMat.color = col;
        }


        //--リセット--//
        if (isReset)
        {
            //--色を戻す--//
            var col = m_mat.color;
            col.a = 1;
            m_mat.color = col;
            col = m_GarekiMat.color;
            col.a = 0;
            m_GarekiMat.color = col;

            //--ポジションを戻す--//
            m_RB = m_Kabe.GetComponent<Rigidbody>();
            m_RB.constraints = RigidbodyConstraints.FreezeAll;
            m_Kabe.transform.localPosition = m_OldPos;
            m_Kabe.transform.localRotation = m_OldRot;
            isReset = false;
            isOn = false;
        } 

        //--デバッグ用--//
        if (Debug_On)
        {
            SwitchOn();
            Debug_On = false;
        }

	}


    //--壁が動き出す--//
    public void SwitchOn()
    {
        Vector3 vec = new Vector3(0, 0, 10);
        m_Kabe.transform.rotation = Quaternion.Euler(vec);
        m_RB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ;
    }
    //--透明にする--//
    public void ClearOn()
    {
        isOn = true;
    }
    public void ClearOff()
    {
        isOn = false;
    }


    //--色を透明にする処理--//
    private void OnDestroy()
    {
        var col = m_GarekiMat.color;
        col.a = 0;
        m_GarekiMat.color = col;
    }

}
