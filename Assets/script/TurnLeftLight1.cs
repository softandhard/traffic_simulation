using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnLeftLight1 : MonoBehaviour
{
    //��ֵx00x11
    public int whichlight;

    [Tooltip("�ӳٿ�ʼ")]
    public float delaytime;

    [Tooltip("�ڼ�����")]
    public int m_index ;

    [Tooltip("��ת�ĵ�")]
    public GameObject[] turnleftobj;

    [Tooltip("��תʱ��")]
    public int[] Turnlefttime = new int[] {4,1,5};

    //[Tooltip("�ж��Ƿ�ΪX����")]
    //public bool x = false;
    //public Transform[] XturnleftCar;
    //public Transform[] ZturnleftCar;

    [Tooltip("�Ƶ���ɫ��ʼ������")]
    public int initialsetting;

    RYBpoleBase rYBpoleBase;
    private RYBpole RYBpole;
    void Start()
    {
        if (transform.parent.parent.GetComponent<RYBpoleBase>() != null)
        {
            rYBpoleBase = transform.parent.parent.GetComponent<RYBpoleBase>();
            RYBpole = rYBpoleBase.RYBpole;
            if (RYBpole != null)
            {
                Turnlefttime[0] = RYBpole.LightLeft_gt[whichlight];
                Turnlefttime[2] = RYBpole.LightCycle - RYBpole.LightLeft_gt[whichlight];
                m_index = RYBpole.li[whichlight];
                delaytime = RYBpole.ldt[whichlight];
                initialsetting = RYBpole.lsc[whichlight];
            }
        }

        if (initialsetting == 2)
        {
            turnleftobj[2].SetActive(true);
        }
        if (initialsetting == 1)
        {
            turnleftobj[1].SetActive(true);
        }
        if (initialsetting == 0)
        {
            turnleftobj[0].SetActive(true);
        }
        Invoke(nameof(Turnleft), delaytime);
    }
    //private void Update()
    //{
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
           
        //    GlobalVariable globalVariable = transform.parent.parent.GetComponent<GlobalVariable>();
        //    if(globalVariable == null)
        //    {

        //    }
        //    else
        //    {
        //        if (whichlight == 0)
        //        {
        //            //print(transform.parent.parent.name);              
        //            Turnlefttime[0] = globalVariable.num1[5];
        //            Turnlefttime[2] = globalVariable.x0lr;
        //            //print("GYR" + Lighttime[2]);
        //            m_index = globalVariable.num1[47];
        //            delaytime = globalVariable.num1[48];
        //            initialsetting = globalVariable.num1[12];
        //        }
        //        if (whichlight == 1)
        //        {
        //            Turnlefttime[0] = globalVariable.num1[6];
        //            Turnlefttime[2] = globalVariable.x1lr;
        //            m_index = globalVariable.num1[49];
        //            delaytime = globalVariable.num1[50];
        //            initialsetting = globalVariable.num1[16];
        //        }
        //        if (whichlight == 2)
        //        {
        //            Turnlefttime[0] = globalVariable.num1[7];
        //            Turnlefttime[2] = globalVariable.z0lr;
        //            m_index = globalVariable.num1[51];
        //            delaytime = globalVariable.num1[52];
        //            initialsetting = globalVariable.num1[20];
        //        }
        //        if (whichlight == 3)
        //        {
        //            Turnlefttime[0] = globalVariable.num1[8];
        //            Turnlefttime[2] = globalVariable.z1lr;
        //            m_index = globalVariable.num1[53];
        //            delaytime = globalVariable.num1[54];
        //            initialsetting = globalVariable.num1[24];
        //        }
        //    }
            
        //}
    //}

    
    void Turnleft()
    {
        //�̵�
        if (m_index == 0)
        {
            turnleftobj[0].SetActive(true);
        }
        else
        {
            turnleftobj[0].SetActive(false);
        }

        //�Ƶ�
        if (m_index == 1)
        {
            turnleftobj[1].SetActive(true);
        }
        else
        {
            turnleftobj[1].SetActive(false);
        }

        //���
        if (m_index == 2)
        {
            turnleftobj[2].SetActive(true);
        }
        else
        {
            turnleftobj[2].SetActive(false);
        }

        //�ı�m_index�Ĵ�С
        //�̵�
        if (m_index== 0)
        {
            Invoke(nameof(Turnleft), Turnlefttime[0]);
        }
        //�Ƶ�
        else if (m_index == 1)
        {
            Invoke(nameof(Turnleft), Turnlefttime[1]);

        }
        //���
        else if (m_index == 2)
        {
            Invoke(nameof(Turnleft), Turnlefttime[2]);
        }
        m_index++;
        if (m_index == 3) m_index = 0; 
    }
}


//void Update()
// {

//��д���ˣ���bug
//if (x == true&&XturnleftCar.Length!=0)
//{
//    //��ȡ�����ڵ�
//    foreach(Transform q in XturnleftCar)
//    {
//        q.GetComponent<AlongPathMove>().X(m_index);
//    }


//    //����д����Bug��out of array
//    //for(int q = 0; q <= XturnleftCar.Length; q++)
//    //{
//    //    //if (q == XturnleftCar.Length)  q = 0;
//    //    XturnleftCar[q].GetComponent<AlongPathMove>().X(m_index);
//    //}

//    //transform.Find("/Car/car0").GetComponent<AlongPathMove>().X(m_index);
//    //transform.Find("/Car/car2").GetComponent<AlongPathMove>().X(m_index);
//}
//if (x == false&&ZturnleftCar.Length!=0)
//{

//    foreach (Transform q in ZturnleftCar)
//    {
//        q.GetComponent<AlongPathMove>().Z(m_index);
//    }
//    //for (int q = 0; q <= ZturnleftCar.Length; q++)
//    //{
//    //    //if(q == XturnleftCar.Length)  q = 0;
//    //    ZturnleftCar[q].GetComponent<AlongPathMove>().Z(m_index);
//    //}
//    //transform.Find("/Car/car1").GetComponent<AlongPathMove>().Z(m_index);
//    //transform.Find("/Car/car3").GetComponent<AlongPathMove>().Z(m_index);
//}
//}