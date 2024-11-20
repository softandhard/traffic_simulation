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
        knifeDic = new Dictionary<float, GameObject>();//初始化       
        KnifeList = new List<float>();
        targetArr = GameObject.FindGameObjectsWithTag("Knife");//寻找带标签的对象，做成集合
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
            Debug.Log("距离是" + dis);
            if (!KnifeList.Contains(dis))
            {
                KnifeList.Add(dis);
            }
        }
        KnifeList.Sort();//对距离进行排序
        Debug.Log("***" + KnifeList[0]);
        GameObject obj;
        knifeDic.TryGetValue(KnifeList[0], out obj);//获取距离最近的对象
        Debug.Log("最近物体名称是" + obj.name);
    }
}