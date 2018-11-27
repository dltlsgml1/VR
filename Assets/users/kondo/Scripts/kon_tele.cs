using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class kon_tele : MonoBehaviour
{

    [SerializeField]
    GameObject Goal;
    [SerializeField]
    bool isMouse;

    #region Teleport
    //--Laser Prefab--//
    [SerializeField]
    GameObject laserPrefab;
    //--Laser Instance--//
    private GameObject laser;
    //--Laser Transform--//
    private Transform laserTransform;
    //-- Laser Hit Point Vecotr--//
    private Vector3 hitPoint;

    [SerializeField]
    Transform m_cameraRigTransform;
    [SerializeField]
    GameObject teleportReticlePrefab;

    private GameObject reticle;
    private Transform teleportReticleTransform;

    [SerializeField]
    GameObject m_head;
    Transform m_headTransform;
    Camera m_camera;

    [SerializeField]
    Vector3 teleportReticleOffset;

    [SerializeField]
    LayerMask teleportMask;
    #endregion

    [SerializeField]
    GameObject m_NextObj;
    [SerializeField]
    GameObject m_OldObj;

    public int m_LastMoveCount;

    private bool isGoal;
    [SerializeField]
    private bool isGameOver;


    #region Camera
    //////////////////////////////////
    // カメラの回転速度を格納する変数
    [SerializeField]
    Vector2 rotationSpeed;
    // マウス移動方向とカメラ回転方向を反転する判定フラグ
    [SerializeField]
    bool reverse;
    // マウス座標を格納する変数
    private Vector2 lastMousePosition;
    // カメラの角度を格納する変数（初期値に0,0を代入）
    private Vector2 newAngle = new Vector2(0, 0);
    /////////////////////////////////////
    #endregion


    #region VR_Controller
    SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    #endregion


    // Use this for initialization
    void Start()
    {
        isGoal = false;
        isGameOver = false;

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
        m_camera = m_head.GetComponent<Camera>();
        m_headTransform = m_head.transform;

        //--マウス操作時--//
        if (isMouse)
        {
            //--reserpoint--//
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray =   m_camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100, teleportMask))
                {
                    m_NextObj = hit.collider.gameObject;

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
            if (Input.GetMouseButtonUp(0))
            {
                GimmickCheck();
                Teleport();
            }


            //--マウスでカメラを動かすための関数--//
            CameraRot();
           

        }
        else
        {
            //--reserpoint--//
            if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                RaycastHit hit;
                Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100, teleportMask))
                {
                    m_NextObj = hit.collider.gameObject;

                    hitPoint = hit.collider.gameObject.transform.position;
                    ShowLaser(hit);

                    reticle.SetActive(true);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                }
                else
                {
                    //laser.SetActive(false);
                    reticle.SetActive(false);

                }

            }

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                GimmickCheck();
                Teleport();
            }
        }


        if(Vector3.Distance(m_cameraRigTransform.position,Goal.transform.position)<0.5f)
        {
            isGoal = true;
        }
    }




    private void ShowLaser(RaycastHit hit)
    {
        //--Laser Active--//
        laser.SetActive(true);
        //----//
        laserTransform.position = Vector3.Lerp(m_camera.ScreenToWorldPoint(Input.mousePosition), hitPoint, 0.5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }




    private void Teleport()
    {

        if (!reticle.activeSelf || m_LastMoveCount<=0) return;

        m_LastMoveCount -= 1;

        Debug.Log(m_NextObj + m_NextObj.tag);
        Debug.Log("Old" + m_OldObj);

        reticle.SetActive(false);
        laser.SetActive(false);
        Vector3 difference = m_cameraRigTransform.position - m_headTransform.position;
        difference.y = 0.2f;
        m_cameraRigTransform.position = m_NextObj.transform.position + difference;



        if (m_OldObj.tag == "Gimmick")
        {
            m_OldObj.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            m_OldObj.GetComponent<SphereCollider>().enabled = true;
        }

        if(m_NextObj.tag == "Gimmick")
        {
            m_OldObj = m_NextObj;
            m_OldObj.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            m_OldObj = m_NextObj;
            m_OldObj.GetComponent<SphereCollider>().enabled = false;
        }


    }



    private void GimmickCheck()
    {
        if(m_NextObj !=null)
        {
            if (m_NextObj.tag == "Gimmick")
            {
                if (m_NextObj.name == "Goal")
                {
                    if (m_LastMoveCount <= 0)
                    {
                        isGameOver = true;
                        laser.SetActive(false);
                    }
                    else
                    {
                        isGoal = true;
                    }
                }
                if (m_NextObj.name == "Door")
                {
                    m_NextObj.GetComponent<AutomaticDoor>().DoorOpen();
                    m_NextObj.GetComponent<BoxCollider>().enabled = false;
                    m_NextObj = m_OldObj;
                }
            }
            else
            {
                if (m_LastMoveCount <= 0)
                {
                    isGameOver = true;
                    laser.SetActive(false);
                }
            }
        }
        
    }



    //--マウスでカメラを動かすための関数--//
    private void CameraRot()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // カメラの角度を変数"newAngle"に格納
            newAngle = m_camera.transform.localEulerAngles;
            // マウス座標を変数"lastMousePosition"に格納
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            //カメラ回転方向の判定フラグが"true"の場合
            if (!reverse)
            {
                // Y軸の回転：マウスドラッグ方向に視点回転
                // マウスの水平移動値に変数"rotationSpeed"を掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * rotationSpeed.y;
                // X軸の回転：マウスドラッグ方向に視点回転
                // マウスの垂直移動値に変数"rotationSpeed"を掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * rotationSpeed.x;
                // "newAngle"の角度をカメラ角度に格納
                m_camera.transform.localEulerAngles = newAngle;
                // マウス座標を変数"lastMousePosition"に格納
                lastMousePosition = Input.mousePosition;
            }
            // カメラ回転方向の判定フラグが"reverse"の場合
            else if (reverse)
            {
                // Y軸の回転：マウスドラッグと逆方向に視点回転
                newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.y;
                // X軸の回転：マウスドラッグと逆方向に視点回転
                newAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.x;
                // "newAngle"の角度をカメラ角度に格納
                m_camera.transform.localEulerAngles = newAngle;
                // マウス座標を変数"lastMousePosition"に格納
                lastMousePosition = Input.mousePosition;
            }
        }
    }









    public bool ISGoal()
    {
        return isGoal;
    }
    public bool ISGameOver()
    {
        return isGameOver;
    }


    public void SetNextStage(GameObject obj, int count = 4)
    {
        m_cameraRigTransform.position = obj.transform.position;
        if(m_OldObj!=null)m_OldObj.GetComponent<BoxCollider>().enabled = true;
        m_OldObj = obj;
        m_OldObj.GetComponent<BoxCollider>().enabled = false;
        isGoal = false;
        isGameOver = false;
        m_NextObj = null;

        m_LastMoveCount = count;

    }
    public void ResetStage(GameObject obj, int count = 4)
    {
        m_cameraRigTransform.position = obj.transform.position;

        if (m_OldObj.GetComponent<BoxCollider>() != null)
        {
            m_OldObj.GetComponent<BoxCollider>().enabled = true;
        }
        if (m_OldObj.GetComponent<SphereCollider>() != null)
        {
            m_OldObj.GetComponent<SphereCollider>().enabled = true;
        }


         m_OldObj = obj;
        m_OldObj.GetComponent<BoxCollider>().enabled = false;
        isGoal = false;
        isGameOver = false;
        m_NextObj = null;

        m_LastMoveCount = count;

    }
}
