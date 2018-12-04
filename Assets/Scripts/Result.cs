using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {
    public TextMesh ResultText;
	// Use this for initialization
	void Start () {
		if(Kon_GameManager.IsGameOver==true)
        {
            ResultText.text = "GAME OVER";
        }
        if(Kon_GameManager.IsGoal==true)
        {
            ResultText.text = "CLEAR!!";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
