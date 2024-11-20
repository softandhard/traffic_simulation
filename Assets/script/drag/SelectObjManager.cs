using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjManager : MonoBehaviour
{

    private static SelectObjManager _instance;
    public static SelectObjManager Instance
    {
        get { return _instance; }
    }
    //物体z轴距摄像机的长度
    public float _zDistance = 50f;
    //对象的缩放系数
    public float _scaleFactor = 1;
    //地面层级
    public LayerMask _groundLayerMask;
    int touchID;
    public bool isDragging = false;
    bool isTouchInput = false;
    //是否是有效的放置（如果放置在地面上返回True,否则为False）
    bool isPlaceSuccess = false;
    //当前要被放置的对象
    public GameObject currentPlaceObj = null;
    //坐标在Y轴上的偏移量
    public int _YOffset;

    //预制体节点管理
    public Transform prefabfold;


    void Awake()
    {
        _instance = this;
    }
    void Update()
    {
        if (currentPlaceObj == null) return;

        if (CheckUserInput())
        {
            MoveCurrentPlaceObj();
        }
        else if (isDragging)
        {
            CheckIfPlaceSuccess();
        }
    }
    /// <summary>
    ///检测用户当前输入
    /// </summary>
    /// <returns></returns>
    bool CheckUserInput()
    {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        if (Input.touches.Length > 0) {
            if (!isTouchInput) {
                isTouchInput = true;
                touchID = Input.touches[0].fingerId;
                return true;
            } else if (Input.GetTouch (touchID).phase == TouchPhase.Ended) {
                isTouchInput = false;
                return false;
            } else {
                return true;
            }
        }
        return false;
#else
        return Input.GetMouseButton(0);
#endif
    }
    /// <summary>
    ///让当前对象跟随鼠标移动
    /// </summary>
    void MoveCurrentPlaceObj()
    {
        isDragging = true;
        Vector3 point;
        Vector3 screenPosition;
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        Touch touch = Input.GetTouch (touchID);
        screenPosition = new Vector3 (touch.position.x, touch.position.y, 0);
#else
        screenPosition = Input.mousePosition;
#endif
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000, _groundLayerMask))
        {
            point = hitInfo.point;
            isPlaceSuccess = true;
        }
        else
        {
            point = ray.GetPoint(_zDistance);
            isPlaceSuccess = false;
        }
        currentPlaceObj.transform.position = point + new Vector3(0, _YOffset, 0);
        currentPlaceObj.transform.position = new Vector3(Mathf.RoundToInt(currentPlaceObj.transform.position.x / 10) * 10,
            _YOffset, Mathf.RoundToInt(currentPlaceObj.transform.position.z/ 10) * 10);
        currentPlaceObj.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    /// <summary>
    ///在指定位置化一个对象
    /// </summary>
    void CreatePlaceObj()
    {
        Debug.Log(currentPlaceObj.name);
        GameObject obj = Instantiate(currentPlaceObj,prefabfold);
        obj.transform.position = currentPlaceObj.transform.position;
        obj.transform.localEulerAngles = currentPlaceObj.transform.localEulerAngles;
        obj.transform.localScale *= _scaleFactor;
        obj.name= System.Guid.NewGuid().ToString("N");
        //改变这个对象的Layer为Drag，以便后续拖动检测
        obj.layer = LayerMask.NameToLayer("drag");

        RoadType roadType=new RoadType();


        switch (currentPlaceObj.name)
        {
            case "e1(Clone)":
                roadType = RoadType.e1;
                break;
            case "e2(Clone)":
                roadType = RoadType.e2;
                break;
            case "r2(Clone)":
                roadType = RoadType.r2;
                break;
            case "r3(Clone)":
                roadType = RoadType.r3;
                break;
        }

        if (roadType == RoadType.r2 || roadType == RoadType.r3)
        {
            Road road = new Road();
            road.RoadType = roadType;
            road.RoadPos = obj.transform.position;
            road.RoadRotation = obj.transform.localEulerAngles;
            road.RoadScale = obj.transform.localScale;
            road.RoadName = obj.name;
            road.layer = obj.layer;
            SaveMgr.Instance.AddRoad(road);
            obj.GetComponent<RoadBase>().Road = road;
        }

        if (roadType == RoadType.e1 || roadType == RoadType.e2)
        {
            RYBpole road = SaveMgr.Instance.CreatRYBpole(); 
            road.RoadType = roadType;
            road.RoadPos = obj.transform.position;
            road.RoadRotation = obj.transform.localEulerAngles;
            road.RoadScale = obj.transform.localScale;
            road.RoadName = obj.name;
            road.layer = obj.layer;
            SaveMgr.Instance.AddRYBpole(road);
            obj.GetComponent<RYBpoleBase>().RYBpole = road;
        }

    }




    /// <summary>
    ///检测是否放置成功
    /// </summary>
    void CheckIfPlaceSuccess()
    {
        if (isPlaceSuccess)
        {
            CreatePlaceObj();
        }
        isDragging = false;
        currentPlaceObj.SetActive(false);
        currentPlaceObj = null;
    }
    /// <summary>
    /// 将要创建的对象传递给当前对象管理器
    /// </summary>
    /// <param name="newObject"></param>
    public void AttachNewObject(GameObject newObject)
    {
        if (currentPlaceObj)
        {
            currentPlaceObj.SetActive(false);
        }
        currentPlaceObj = newObject;
    }
}