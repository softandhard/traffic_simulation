using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectImage : MonoBehaviour, IPointerDownHandler
{
    //��Ҫ��ʵ������Ԥ��
    public GameObject inistatePrefab;
    //ʵ������Ķ���
    private GameObject inistateObj;

    // Use this for initialization
    void Start()
    {
        if (inistatePrefab == null) return;
        //ʵ����Ԥ��
        inistateObj = Instantiate(inistatePrefab);
        inistateObj.SetActive(false);
    }
    //ʵ����갴�µĽӿ�
    public void OnPointerDown(PointerEventData eventData)
    {
        inistateObj.SetActive(true);
        //����ǰ��Ҫ��ʵ�����Ķ��󴫵ݵ���������
        SelectObjManager.Instance.AttachNewObject(inistateObj);
    }
}