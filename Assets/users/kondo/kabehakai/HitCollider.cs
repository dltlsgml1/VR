using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    [SerializeField]
    GameObject m_Gimmick;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Test_YUKA")
        {
            m_Gimmick.GetComponent<kabehakai>().ClearOn();
        }
    }

}
