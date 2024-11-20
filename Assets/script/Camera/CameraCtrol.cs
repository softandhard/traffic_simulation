using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrol : MonoBehaviour
{
    /// <summary>
    /// 旋转的中心点
    /// </summary>
    public Transform pivot;

    public Vector3 pivotOffset = Vector3.zero;
    public Transform target;
    public float distance = 10.0f;
    public float minDistance = 2f;
    public float maxDistance = 15f;
    public float zoomSpeed = 1f;
    public float xSpeed = 250.0f;
    public float ySpeed = 250.0f;
    public bool allowYTilt = true;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;

    /// <summary>
    /// 是否初始化观看角度
    /// </summary>
    public bool ifInitRot;

    /// <summary>
    /// 要初始化的角度
    /// </summary>
    public Vector2 initVector = new Vector2(45, 45);

    /// <summary>
    /// 实时距离（相机距离物体），用来控制缩放的
    /// </summary>
    public float targetDistance = 0f;

    private float x = 0.0f;
    private float y = 0.0f;
    private float targetX = 0f;
    private float targetY = 0f;

    private float xVelocity = 1f;
    private float yVelocity = 1f;
    private float zoomVelocity = 1f;

    private Touch oldTouch1;  //上次触摸点1(手指1)  
    private Touch oldTouch2;  //上次触摸点2(手指2)  
    public float touchspeed = 0.005F;

    private void Start()
    {
        var angles = transform.eulerAngles;
        targetX = x = angles.x;
        targetY = y = ClampAngle(angles.y, yMinLimit, yMaxLimit);
        //targetDistance = distance;
    }

    private void LateUpdate()
    {
        //改变距离
        Changedis();
        targetDistance = distance;

        if (!pivot) return;
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0.0f) targetDistance -= zoomSpeed;
        else if (scroll < 0.0f)
            targetDistance += zoomSpeed;
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
        if (ifInitRot)
        {
            targetX = initVector.x;
            targetY = initVector.y;
            ifInitRot = false;
        }

        if (Input.GetMouseButton(0) || (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))))
        {
            //如果是触摸屏
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    targetX += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                    if (allowYTilt)
                    {
                        targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                        targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
                    }
                    Debug.Log("有触点" + targetX);
                }
            }
            //不是触摸屏
            else
            {
                targetX += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                if (allowYTilt)
                {
                    targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                    targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
                }
                Debug.Log("没触点" + targetX);
            }

        }

        x = Mathf.SmoothDampAngle(x, targetX, ref xVelocity, 0.3f);
        y = allowYTilt ? Mathf.SmoothDampAngle(y, targetY, ref yVelocity, 0.3f) : targetY;
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.5f);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + pivot.position + pivotOffset;
        transform.rotation = rotation;
        transform.position = position;
    }


    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void Changedis()
    {

        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        //    transform.Translate(touchDeltaPosition.x * touchspeed, touchDeltaPosition.y * touchspeed, 0);
        //}

        if (Input.touchCount > 1)
        {
            ////多点触摸, 放大缩小  
            Touch newTouch1 = Input.GetTouch(0);
            Touch newTouch2 = Input.GetTouch(1);

            //第2点刚开始接触屏幕, 只记录，不做处理  
            if (newTouch2.phase == TouchPhase.Began)
            {
                oldTouch2 = newTouch2;
                oldTouch1 = newTouch1;
                return;
            }
            //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
            float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

            //两个距离之差，为正表示放大手势， 为负表示缩小手势  
            float offset = newDistance - oldDistance;

            //放大因子， 一个像素按 0.01倍来算(100可调整)  
            float scaleFactor = offset / 100f;

            distance = offset * scaleFactor;

        }
    }
}
