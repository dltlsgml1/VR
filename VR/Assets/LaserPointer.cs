using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour {
    
    //--Laser Prefab--//
    public GameObject laserPrefab;
    //--Laser Instance--//
    private GameObject laser;
    //--Laser Transform--//
    private Transform laserTransform;
    //-- Laser Hit Point Vecotr--//
    private Vector3 hitPoint;

    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    private GameObject reticle;
    private Transform teleportReticleTransform;

    public Transform headTransform;

    public Vector3 teleportReticleOffset;

    public LayerMask teleportMask;

    private bool shouldTeleport;

    SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start()
    {

        //レーザーのオブジェクトをプレハブから取得
        laser = Instantiate(laserPrefab);
        //Transformのコンポーネントを最初から取得
        //※アクセスしやすくするため
        laserTransform = laser.transform;

        reticle = Instantiate(teleportReticlePrefab);

        teleportReticleTransform = reticle.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100,teleportMask))
            {
                Debug.Log("traclobj" + trackedObj.transform.position.ToString());
                Debug.Log("hitPoint" + hit.point.ToString());

                hitPoint = hit.point;
                ShowLaser(hit);

                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }

        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
        {
            Teleport();
        }
    }




    private void ShowLaser(RaycastHit hit)
    {
        //--Laser Active--//
        laser.SetActive(true);
        //----//
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, 0.5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }




    private void Teleport()
    {
        shouldTeleport = false;
        reticle.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;

    }
}
