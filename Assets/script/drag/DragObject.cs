using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public static DragObject instance;

    //ֻ���ָ���Ĳ㼶�����϶�
    public LayerMask _dragLayerMask;
    //ָ����ǰҪ�϶��Ķ���
    public Transform currentTransform;
    //�Ƿ�����϶���ǰ����
    public bool isDrag = false;
    //���ڴ洢��ǰ��Ҫ�϶��Ķ�������Ļ�ռ��е�����
    Vector3 screenPos = Vector3.zero;
    //��ǰ��Ҫ�϶������������������������ռ������е�ƫ����
    Vector3 offset = Vector3.zero;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //����������ת��Ϊһ������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            //�����ǰ������ָ���Ĳ㼶������ײ����ʾ��ǰ������Ա��϶�
            if (Physics.Raycast(ray, out hitinfo, 1000f, _dragLayerMask))
            {
                isDrag = true;
                //����ǰ��Ҫ�϶��Ķ���ֵΪ������ײ���Ķ���
                currentTransform = hitinfo.transform;
                //����ǰ�������������ת��Ϊ��Ļ����
                screenPos = Camera.main.WorldToScreenPoint(currentTransform.position);
                //��������Ļ����ת��Ϊ����ռ����꣬���뵱ǰҪ�϶��Ķ���������ߵ�ƫ����
                offset = currentTransform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
            }
            else
            {
                isDrag = false;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (isDrag == true)
            {
                var currentScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
                //������Ļ�ռ�����ת��Ϊ�������꣬������ƫ����
                var currentPos = Camera.main.ScreenToWorldPoint(currentScreenPos) + offset;
                currentTransform.position = new Vector3(Mathf.RoundToInt(currentPos.x / 10) * 10, 0, Mathf.RoundToInt(currentPos.z / 10) * 10);

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            currentTransform = null;
        }
    }
}