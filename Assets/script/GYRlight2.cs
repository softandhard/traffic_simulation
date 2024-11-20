using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GYRlight2 : MonoBehaviour
{
    [Tooltip("�ӳٿ�ʼ")]
    public float delaytime;

    [Tooltip("�ڼ�����")]
    public int i;

    [Tooltip("�Ƶ�����������")]
    public int[] Lighttime = new int[] { 4, 1, 5 };


    [Tooltip("�ж��Ƿ�ΪX����")]
    public bool x = false;

    public static GYRlight2 instance;

    //[Tooltip("������λ")]
    //public Transform[] carCreatpoints;

    [Tooltip("�Ƶ���ɫ��ʼ������")]
    public int initialsetting;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {

        //if (x == true)
        //{
        //    //print(i);
        //    //��ȡ�����������Ľڵ�
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
        //    //��һ��д��
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

        //    //��ȡ�����ڵ�

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
    }
    void Changecolor()
    {
        //Debug.Log(i);
        //����i��ֵ������ɫ
        //�̵�
        if (i == 0)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
        }

        //�Ƶ�
        if (i == 1)
        {
            transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else
        {
            transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.white;
        }

        //���
        if (i == 2)
        {
            transform.GetChild(2).GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            transform.GetChild(2).GetComponent<MeshRenderer>().material.color = Color.white;
        }

        //�ı�i�Ĵ�С
        //�̵�
        if (i == 0)
        {
            Invoke(nameof(Changecolor), Lighttime[0]);
        }
        //�Ƶ�
        else if (i == 1)
        {
            Invoke(nameof(Changecolor), Lighttime[1]);
        }
        //���
        else if (i == 2)
        {
            Invoke(nameof(Changecolor), Lighttime[2]);
        }
        i++;
        if (i == 3) i = 0;
    }
}
