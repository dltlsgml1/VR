using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControllerManager : MonoBehaviour {
    public enum KeyList { Trigger,PadClick}
    public static ControllerManager Instance = null;
    public bool MainTrigger;
    public bool PadTrigger;

    private OVRTrackedRemote oVRTrackedRemote;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }


    private void Awake()
    {
        
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    
    public bool Get(int Input)
    {
        switch(Input)
        {
            case (int)KeyList.Trigger:
                if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    return true;
                }
                break;
            case (int)KeyList.PadClick:
                if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
                {
                    return true;
                }
                break;
        }
        return false;
    }
    public bool GetDown(int Input)
    {
        switch (Input)
        {
            case (int)KeyList.Trigger:
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    return true;
                }
                break;
            case (int)KeyList.PadClick:
                if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
                {
                    return true;
                }
                break;
        }
        return false;
    }

}
