using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kon_title : MonoBehaviour {

    [SerializeField]
    bool isResultMode;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isResultMode)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("kondo_test");
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("kon_title");
            }
        }

	}
}
