using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    [Header("����������")]
    public GameObject line;
    public GameObject ball;
    private Rigidbody ball_rb;
    public Vector3 forceDirection; // ʩ�����ķ���  
    public float forceMagnitude = 10f; // ���Ĵ�С  
    public float duration = 0.5f; // ����ʱ��  

    [Header("������ק����")]
    [SerializeField] private Vector3 originalPosition; // �����ԭʼλ��  
    [SerializeField] private Vector3 dragOffset; // ��קʱ��ƫ��  
    private Camera mainCamera; // �������  
    [SerializeField] private bool isDragging = false; // ��ק״̬  
    [SerializeField] private float maxRayDistance = 100f;

    void Start()
    {
        if (ball_rb == null)
        {
            ball_rb = GetComponent<Rigidbody>();
        }
        // ������ʼ��Ϊ0
        ball_rb.drag = 0f;
        originalPosition = transform.position; // �洢����ԭʼλ��  
        mainCamera = Camera.main; // ��ȡ�������  

        // ��ʼʩ����  
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
        // ʩ����  
        ball_rb.AddForce(forceDirection.normalized * forceMagnitude, ForceMode.Impulse);

        // �ȴ�ָ����ʱ��  
        yield return new WaitForSeconds(duration);
    }

    private void DragObject()
    {
        if (Input.GetMouseButtonDown(0)) // ����������  
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRayDistance)) // ����Ƿ���������  
            {
                if (hit.transform == transform) // ȷ��������Ǳ�����  
                {
                    isDragging = true; // ��ʼ��ק  
                    dragOffset = transform.position - hit.point; // ����ƫ�� 
                }
            }
        }

        if (isDragging) // ���������ק  
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            ball_rb.drag = 0f;

            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {
                // �������ƶ������λ�ã�����ƫ��, ������İ뾶  
                transform.position = hit.point + dragOffset + new Vector3(0, ball.GetComponent<SphereCollider>().radius, 0);
            }
            DrawLine();
        }

        if (Input.GetMouseButtonUp(0)) // �������ͷ�  
        {

            if (isDragging)
            {
                isDragging = false; // ֹͣ��ק  
                //transform.position = originalPosition; // ����ָ�ԭλ  
            }
            // �����������ɵ�ǰ��ָ��ԭʼ��
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
