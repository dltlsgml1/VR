using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour {

    public bool IsMouse;
    public bool MouseReverse;

    #region Teleport
    public GameObject laserPrefab;      //--Laser Prefab--//
    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;

    private GameObject reticle;
    private Transform teleportReticleTransform;
    private GameObject laser;       //--Laser Instance--//
    private Transform laserTransform;       //--Laser Transform--//
    private Vector3 hitPoint;       //-- Laser Hit Point Vecotr--//
    private bool shouldTeleport;
    #endregion

    public GameObject NextObj;
    public GameObject OldObj;

    public Camera PlayerCamera;

    public Vector2 RotSpeed;
    bool Reverse;
    private Vector2 LastMousePos;
    private Vector2 NewAngle = new Vector2(0, 0);

    #region SteamVRController
    SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    #endregion

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start()
    {
        
        laser = Instantiate(laserPrefab);   //レーザーのオブジェクトをプレハブから取得
                                              //Transformのコンポーネントを最初から取得
                                              //※アクセスしやすくするため
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        if (IsMouse == true)
        {
            if(Input.GetMouseButton(0))
            {
                Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100, teleportMask))
                {
                    NextObj = hit.collider.gameObject;
                    hitPoint = hit.collider.gameObject.transform.position;
                    ShowLaser(hit);
                    reticle.SetActive(true);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;

                }
                else
                {
                    laser.SetActive(false);
                    reticle.SetActive(false);
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                Teleport();
            }
            CameraRot();
            
        }
        else
        {
            if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {

                if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
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
        
    }




    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
       // laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, 0.5f);
        laserTransform.position = Vector3.Lerp(PlayerCamera.ScreenToWorldPoint(Input.mousePosition), hitPoint, 0.5f);
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

    private void CameraRot()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // カメラの角度を変数"newAngle"に格納
            NewAngle = PlayerCamera.transform.localEulerAngles;
            // マウス座標を変数"lastMousePosition"に格納
            LastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            //カメラ回転方向の判定フラグが"true"の場合
            if (MouseReverse==true)
            {
                // Y軸の回転：マウスドラッグと逆方向に視点回転
                NewAngle.y -= (Input.mousePosition.x - LastMousePos.x) * RotSpeed.y;
                // X軸の回転：マウスドラッグと逆方向に視点回転
                NewAngle.x -= (LastMousePos.y - Input.mousePosition.y) * RotSpeed.x;
                // "newAngle"の角度をカメラ角度に格納
                PlayerCamera.transform.localEulerAngles = NewAngle;
                // マウス座標を変数"lastMousePosition"に格納
                LastMousePos = Input.mousePosition;
            }
            // カメラ回転方向の判定フラグが"reverse"の場合
            else
            {
                // Y軸の回転：マウスドラッグ方向に視点回転
                // マウスの水平移動値に変数"rotationSpeed"を掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                NewAngle.y -= (LastMousePos.x - Input.mousePosition.x) * RotSpeed.y;
                // X軸の回転：マウスドラッグ方向に視点回転
                // マウスの垂直移動値に変数"rotationSpeed"を掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                NewAngle.x -= (Input.mousePosition.y - LastMousePos.y) * RotSpeed.x;
                // "newAngle"の角度をカメラ角度に格納
                PlayerCamera.transform.localEulerAngles = NewAngle;
                // マウス座標を変数"lastMousePosition"に格納
                LastMousePos = Input.mousePosition;

               
            }
        }
    }
}
