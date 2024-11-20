using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrol : MonoBehaviour
{
    /// <summary>
    /// ��ת�����ĵ�
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
    /// �Ƿ��ʼ���ۿ��Ƕ�
    /// </summary>
    public bool ifInitRot;

    /// <summary>
    /// Ҫ��ʼ���ĽǶ�
    /// </summary>
    public Vector2 initVector = new Vector2(45, 45);

    /// <summary>
    /// ʵʱ���루����������壩�������������ŵ�
    /// </summary>
    public float targetDistance = 0f;

    private float x = 0.0f;
    private float y = 0.0f;
    private float targetX = 0f;
    private float targetY = 0f;

    private float xVelocity = 1f;
    private float yVelocity = 1f;
    private float zoomVelocity = 1f;

    private Touch oldTouch1;  //�ϴδ�����1(��ָ1)  
    private Touch oldTouch2;  //�ϴδ�����2(��ָ2)  
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
        //�ı����
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
            //����Ǵ�����
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
                    Debug.Log("�д���" + targetX);
                }
            }
            //���Ǵ�����
            else
            {
                targetX += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                if (allowYTilt)
                {
                    targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                    targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
                }
                Debug.Log("û����" + targetX);
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
            ////��㴥��, �Ŵ���С  
            Touch newTouch1 = Input.GetTouch(0);
            Touch newTouch2 = Input.GetTouch(1);

            //��2��տ�ʼ�Ӵ���Ļ, ֻ��¼����������  
            if (newTouch2.phase == TouchPhase.Began)
            {
                oldTouch2 = newTouch2;
                oldTouch1 = newTouch1;
                return;
            }
            //�����ϵ����������µ��������룬���Ҫ�Ŵ�ģ�ͣ���СҪ����ģ��  
            float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

            //��������֮�Ϊ����ʾ�Ŵ����ƣ� Ϊ����ʾ��С����  
            float offset = newDistance - oldDistance;

            //�Ŵ����ӣ� һ�����ذ� 0.01������(100�ɵ���)  
            float scaleFactor = offset / 100f;

            distance = offset * scaleFactor;

        }
    }
}
