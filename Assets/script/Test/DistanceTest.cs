using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTest : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] targetArr;

    private List<float> KnifeList;

    private Dictionary<float, GameObject> knifeDic;

    void Start()
    {
        knifeDic = new Dictionary<float, GameObject>();//��ʼ��       
        KnifeList = new List<float>();
        targetArr = GameObject.FindGameObjectsWithTag("Knife");//Ѱ�Ҵ���ǩ�Ķ������ɼ���
        InvokeRepeating(nameof(CountDistacne),0,0.5f);
    }



    void CountDistacne()
    {
        KnifeList.Clear();
        knifeDic.Clear();
        for (int i = 0; i < targetArr.Length; i++)
        {
            float dis = Vector3.Distance(targetArr[i].transform.localPosition, transform.localPosition);
            knifeDic.Add(dis, targetArr[i].gameObject);
            Debug.Log("������" + dis);
            if (!KnifeList.Contains(dis))
            {
                KnifeList.Add(dis);
            }
        }
        KnifeList.Sort();//�Ծ����������
        Debug.Log("***" + KnifeList[0]);
        GameObject obj;
        knifeDic.TryGetValue(KnifeList[0], out obj);//��ȡ��������Ķ���
        Debug.Log("�������������" + obj.name);
    }
}