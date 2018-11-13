using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class kon_tele : MonoBehaviour
{
    [SerializeField]
    bool isMouse;


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

    private bool shouldTeleport;

    GameObject m_NextObj;
    GameObject m_OldObj;

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

         m_camera = m_head.GetComponent<Camera>();
        m_headTransform = m_head.transform;

    }

    // Update is called once per frame
    void Update()
    {
        //--マウス操作時--//
        if(isMouse)
        {
            //--reserpoint--//
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray =   m_camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100, teleportMask))
                {
                    //Debug.Log("traclobj" + trackedObj.transform.position.ToString());
                    Debug.Log("hitPoint" + hit.point.ToString());

                    m_NextObj = hit.collider.gameObject;

                    hitPoint = hit.collider.gameObject.transform.position;
                    ShowLaser(hit);

                    reticle.SetActive(true);
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                    shouldTeleport = true;
                }
                else
                {
                    laser.SetActive(false);
                    reticle.SetActive(false);

                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Teleport();
            }


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

        if (!laser.activeSelf) return;
            
        shouldTeleport = false;
        reticle.SetActive(false);
        laser.SetActive(false);
        Vector3 difference = m_cameraRigTransform.position - m_headTransform.position;
        difference.y = 0.2f;
        m_cameraRigTransform.position = hitPoint + difference;

        if(m_OldObj!=null) m_OldObj.GetComponent<SphereCollider>().enabled = true;
        m_OldObj = m_NextObj;
        m_OldObj.GetComponent<SphereCollider>().enabled = false;




    }
}
