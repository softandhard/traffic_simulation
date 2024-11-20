using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GYRlight1 : MonoBehaviour
{
    //传值x00x11
    public int whichlight;

    [Tooltip("延迟开始")]
    public float delaytime;

    [Tooltip("第几个灯")]
    public int i;

    [Tooltip("灯的秒数的数组")]
    public int[] Lighttime = new int[] {4,1,5};


    //[Tooltip("判断是否为X方向")]
    //public bool x=false;

    public static GYRlight1 instance;

    //[Tooltip("汽车点位")]
    //public Transform[] carCreatpoints;

    [Tooltip("灯的颜色初始化设置")]
    public int  initialsetting;

    RYBpoleBase rYBpoleBase;
    private RYBpole RYBpole;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (transform.parent.parent.GetComponent<RYBpoleBase>() != null)
        {
            rYBpoleBase = transform.parent.parent.GetComponent<RYBpoleBase>();
            //放在start里面初始化值awake太早调用其他脚本
            RYBpole = rYBpoleBase.RYBpole;
            if (RYBpole != null)
            {
                print(transform.parent.parent.name + "绿灯是" + RYBpole.LightStraight_gt[whichlight]);
                Lighttime[0] = RYBpole.LightStraight_gt[whichlight];
                Lighttime[2] = RYBpole.LightCycle - RYBpole.LightStraight_gt[whichlight];
                i = RYBpole.i[whichlight];
                delaytime = RYBpole.dt[whichlight];
                initialsetting = RYBpole.sc[whichlight];
            }
        }
        if (initialsetting == 2)
        {
            transform.GetChild(2).GetComponent<MeshRenderer>().material.color = Color.red;
        }
        if (initialsetting == 1)
        {
            transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        if (initialsetting == 0)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
        }
        Invoke(nameof(Changecolor), delaytime);
    }


    void Changecolor()
    {
        //Debug.Log(i);
        //根据i的值决定颜色
        //绿灯
        if (i == 0)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
        }

        //黄灯
        if (i == 1)
        {
            transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else
        {
            transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.white;
        }
        //红灯
        if (i == 2)
        {
            transform.GetChild(2).GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            transform.GetChild(2).GetComponent<MeshRenderer>().material.color = Color.white;
        }
        //改变i的大小
        //绿灯
        if (i == 0)
        {
            Invoke(nameof(Changecolor), Lighttime[0]);
        }
        //黄灯
        else if (i == 1)
        {
            Invoke(nameof(Changecolor), Lighttime[1]);
        }
        //红灯
        else if (i == 2)
        {
            Invoke(nameof(Changecolor), Lighttime[2]);
        }    
        i++;
        if (i == 3) i = 0;
    }
}

//void Update()
//{
//if (x == true)
//{
//    //print(i);
//    //获取到生成汽车的节点
//    GameObject[] targets = GameObject.FindGameObjectsWithTag("X");
//    foreach(GameObject target in targets)
//    {
//        //print(target.name);
//        foreach(Transform child in target.transform)
//        {
//            //print(child.name);
//            //child.GetComponent<CarMove>().X(i);
//        }
//        //target.transform.GetComponent<CarMove>().X(i);
//    }
//    //另一种写法
//    //Transform xcar1 = this.transform.Find("/X/point1");
//    //foreach(Transform child in xcar1)
//    //{
//    //    child.GetComponent<CarMove>().X(i);
//    //}
//    //Transform xcar2 = this.transform.Find("/X/point1");
//    //foreach (Transform child in xcar2)
//    //{
//    //    child.GetComponent<CarMove>().X(i);
//    //}

//}
//if (x == false)
//{

//    //获取汽车节点

//    GameObject[] targets = GameObject.FindGameObjectsWithTag("Z");
//    foreach (GameObject target in targets)
//    {
//        //print(target.name);
//        foreach (Transform child in target.transform)
//        {
//            //print(child.name);
//            //child.GetComponent<CarMove>().Z(i);
//        }
//        //target.transform.GetComponent<CarMove>().X(i);
//    }
//    //Transform zcar3 = this.transform.Find("/Z/point3");
//    //foreach (Transform child in zcar3)
//    //{
//    //    child.GetComponent<CarMove>().Z(i);
//    //}
//    //Transform zcar4 = this.transform.Find("/Z/point4");
//    //foreach (Transform child in zcar4)
//    //{
//    //    child.GetComponent<CarMove>().Z(i);
//    //}

//}
//}