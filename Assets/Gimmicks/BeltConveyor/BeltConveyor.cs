using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor : MonoBehaviour {


    [SerializeField]
    bool isOn;
    [SerializeField]
    bool isReverse;
    [SerializeField]
    bool isInitialPos;

    [SerializeField]
    float m_Speed = 0.3f;

    private Material m_mat;
    private Vector2 m_Offset;

	// Use this for initialization
	void Start () {
        m_mat = this.GetComponent<MeshRenderer>().material;
        m_Offset = m_mat.mainTextureOffset;
	}
	
	// Update is called once per frame
	void Update () {

        if (!isInitialPos)
        {
            if (isOn)
            {
                if (!isReverse)
                {
                    m_Offset.x += m_Speed * Time.deltaTime;
                    if (m_Offset.x > 0.9f) m_Offset.x = -0.1f;
                    m_mat.mainTextureOffset = m_Offset;
                }
                else
                {
                    m_Offset.x -= m_Speed * Time.deltaTime;
                    if (m_Offset.x < -0.1f) m_Offset.x = 0.9f;
                    m_mat.mainTextureOffset = m_Offset;
                }
            }
        }
        else
        {
            if (!isReverse)
            {
                m_Offset.x += m_Speed * Time.deltaTime;
                if (m_Offset.x > 0.9f)
                {
                    m_Offset.x = -0.1f;
                    isInitialPos = false;
                }
                m_mat.mainTextureOffset = m_Offset;
            }
            else
            {
                m_Offset.x -= m_Speed * Time.deltaTime;
                if (m_Offset.x < -0.1f)
                {
                    m_Offset.x = 0.9f;
                    isInitialPos = false;
                }
                m_mat.mainTextureOffset = m_Offset;
            }
        }

	}



    public void SwitchOn() { isOn = true; }
    public void SwitchOff() { isOn = false; }
    public void BeltInitialPos() { isInitialPos = true; }


}
