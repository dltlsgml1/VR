using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearVRController : MonoBehaviour {

    public GameObject Camera;
    public Text InputText;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        Vector2 InputPoint = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        Vector3 InputVec;
        InputVec.x = InputPoint.x;
        InputVec.y = 0.0f;
        InputVec.z = InputPoint.y;
        Vector3.Normalize(InputVec);
        InputVec = InputVec * 0.1f;
        Camera.transform.position += (Vector3)InputVec;
        
        
	}
}
