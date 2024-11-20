using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    [Header("作用力设置")]
    public GameObject line;
    public GameObject ball;
    private Rigidbody ball_rb;
    public Vector3 forceDirection; // 施加力的方向  
    public float forceMagnitude = 10f; // 力的大小  
    public float duration = 0.5f; // 持续时间  

    [Header("物体拖拽设置")]
    [SerializeField] private Vector3 originalPosition; // 物体的原始位置  
    [SerializeField] private Vector3 dragOffset; // 拖拽时的偏移  
    private Camera mainCamera; // 主摄像机  
    [SerializeField] private bool isDragging = false; // 拖拽状态  
    [SerializeField] private float maxRayDistance = 100f;

    void Start()
    {
        if (ball_rb == null)
        {
            ball_rb = GetComponent<Rigidbody>();
        }
        // 阻力初始化为0
        ball_rb.drag = 0f;
        originalPosition = transform.position; // 存储物体原始位置  
        mainCamera = Camera.main; // 获取主摄像机  

        // 开始施加力  
        StartCoroutine(ApplyShortForce());
    }

    private void OnCollisionEnter(Collision collision)
    {
        ball_rb.drag = 1f;
    }

    private void OnCollisionExit(Collision collision)
    {
        ball_rb.drag = 0f;
    }

    private System.Collections.IEnumerator ApplyShortForce()
    {
        // 施加力  
        ball_rb.AddForce(forceDirection.normalized * forceMagnitude, ForceMode.Impulse);

        // 等待指定的时间  
        yield return new WaitForSeconds(duration);
    }

    private void DragObject()
    {
        if (Input.GetMouseButtonDown(0)) // 鼠标左键按下  
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRayDistance)) // 检测是否点击了物体  
            {
                if (hit.transform == transform) // 确保点击的是本物体  
                {
                    isDragging = true; // 开始拖拽  
                    dragOffset = transform.position - hit.point; // 计算偏移 
                }
            }
        }

        if (isDragging) // 如果正在拖拽  
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            ball_rb.drag = 0f;

            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {
                // 将物体移动到鼠标位置，加上偏移, 加上球的半径  
                transform.position = hit.point + dragOffset + new Vector3(0, ball.GetComponent<SphereCollider>().radius, 0);
            }
            DrawLine();
        }

        if (Input.GetMouseButtonUp(0)) // 鼠标左键释放  
        {

            if (isDragging)
            {
                isDragging = false; // 停止拖拽  
                //transform.position = originalPosition; // 物体恢复原位  
            }
            // 作用力方向由当前点指向原始点
            forceDirection = originalPosition - transform.position;
            StartCoroutine(ApplyShortForce());
        }
    }

    private void DrawLine()
    {
        float ball_velocity = forceMagnitude * duration / ball_rb.mass;
        float y_vector = forceDirection.normalized.y;
        float x_distance = (y_vector * ball_velocity / Physics.gravity.y) * 2 * forceDirection.normalized.x * ball_velocity;
        float x_unit = (x_distance / 2);

        for(int i=0; i<x_unit;i++)
        {
            Instantiate(line, new Vector3(transform.position.x + x_unit * i, 
                Mathf.Sqrt(2*transform.position.x + x_unit * i /Physics.gravity.y),
                transform.position.z
                ), new Quaternion(0,0,0,0));
        }

    }

    private void Update()
    {
        DragObject();
    }

}
