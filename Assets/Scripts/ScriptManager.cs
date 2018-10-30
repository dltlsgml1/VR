using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScriptManager : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(ControllerManager.Instance.Get((int)ControllerManager.KeyList.Trigger))
        {
            text.text = "Trigger";
        }

        else if(ControllerManager.Instance.Get((int)ControllerManager.KeyList.PadClick))
        {
            text.text = "Pad";
        }

        else
        {
            text.text = "Nothing";
        }
        
	}
}
