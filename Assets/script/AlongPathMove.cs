
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public enum MoveType { 
    Once,
    Loop,
    Yoyo,
}


public class AlongPathMove : MonoBehaviour
{
    private GameObject[] RybCenter;
    private List<float> rybcenterlist;
    private Dictionary<float, GameObject> rybcenterdic;
    private Vector3 centerposition;

    //�˴�v1Ϊ��ӵĵȺ�ƴ������
    //public float v1=20;
    //public float v=20;//km/h  1000/3600s=10/36  �õ�ÿ����ٶ�
    //public Path path;

    //public MoveType moveType;
    //private List<Vector3> pathPoint = new List<Vector3>();

    //public float totalLength;//·���ܳ���

    //private float currentS;//��ǰ�Ѿ��߹���·��

    //private int index = 0;

    //private Vector3 dir;
    //private Vector3 pos;
    //private float s;

    //��������
    public Vector3 CarSlowArea;
    public Vector3 offset;
    private int targercount = 0;
    private bool Xred0;
    private bool Xred1;
    private bool Zred0;
    private bool Zred1;

    [Tooltip("��ת�źŵ�")]
    public GameObject turnleft,yleft;
    public LayerMask car;

    //Ani
    public Animation ani;
    private float aniSpeed = 1;

    void Start()
    {

        rybcenterlist = new List<float>();
        rybcenterdic = new Dictionary<float, GameObject>();

        InvokeRepeating(nameof(Stopmove), 0, 0.1f);
        centerposition = transform.parent.parent.position;
        //Once();
    }

    private void Update()
    {
        SetAnimationSpeed();
        if (turnleft.activeSelf||yleft.activeSelf)
        {
            Xred0 = true;
            Xred1 = true;
            Zred0 = true;
            Zred1 = true;
        }
        else
        {
            Xred0 = false;
            Xred1 = false;
            Zred0 = false;
            Zred1 = false;
        }
        //getcenter();
        //getArea();
        //Stopmove();
    }

    //private void FixedUpdate()
    //{     
    //    s += (v * 10 / 36) * Time.fixedDeltaTime;//�õ�Ӧ�����е�·��

    //    if (currentS < totalLength)
    //    {
    //        for (int i = index; i < pathPoint.Count - 1; i++)
    //        {
    //            currentS += (pathPoint[i + 1] - pathPoint[i]).magnitude;//������һ�����·��

    //            if (currentS > s)
    //            {
    //                index = i;
    //                currentS -= (pathPoint[i + 1] - pathPoint[i]).magnitude;
    //                dir = (pathPoint[i + 1] - pathPoint[i]).normalized;
    //                pos = pathPoint[i] + dir * (s - currentS);
    //                break;

    //            }
    //        }
    //        transform.position = pos;
    //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, transform.up), Time.deltaTime * 5);
    //    }
    //    else
    //    {
    //        //Debug.Log("�ִ��յ�");

    //        switch (moveType)
    //        {
    //            case MoveType.Once:
    //                break;
    //            case MoveType.Loop:
    //                Loop();
    //                break;
    //            case MoveType.Yoyo:
    //                Yoyo();

    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    //private void Yoyo() {
    //    currentS = 0;
    //    totalLength = 0;
    //    index = 0;
    //    s = 0;
    //    pathPoint = path.GetPathPoint();
    //    if (Vector3.SqrMagnitude(pathPoint[0] - transform.position) > Vector3.SqrMagnitude(pathPoint[pathPoint.Count - 1] - transform.position))
    //    {
    //        pathPoint.Reverse();
    //    }
    //    for (int i = 1; i < pathPoint.Count; i++)
    //    {
    //        totalLength += (pathPoint[i] - pathPoint[i - 1]).magnitude;
    //    }
    //}

    //private void Once() {
    //    currentS = 0;
    //    totalLength = 0;
    //    index = 0;
    //    s = 0;

    //    pathPoint = path.GetPathPoint();


    //    for (int i = 1; i < pathPoint.Count; i++)
    //    {
    //        totalLength += (pathPoint[i] - pathPoint[i - 1]).magnitude;
    //    }
    //}
    //private void Loop()
    //{
    //    currentS = 0;
    //    totalLength = 0;
    //    index = 0;
    //    s = 0;

    //    pathPoint = path.GetPathPoint();
    //    for (int i = 1; i < pathPoint.Count; i++)
    //    {
    //        totalLength += (pathPoint[i] - pathPoint[i - 1]).magnitude;
    //    }
    //}



    ////������
    //public float getdis()
    //{
    //    float x = 100;
    //    RaycastHit hit;
    //    Ray ray = new(transform.position, transform.forward);
    //    if (Physics.SphereCast(ray, 1f, out hit) && hit.collider.gameObject.CompareTag("car"))
    //    {
    //        x = hit.distance;
    //    }
    //    return x;
    //}


    //��ȡ����
    //public Vector3 getcenter()
    //{
    //    rybcenterlist.Clear();
    //    rybcenterdic.Clear();
    //    RybCenter = GameObject.FindGameObjectsWithTag("RYBpole");
    //    for (int i = 0; i < RybCenter.Length; i++)
    //    {
    //        //if (RybCenter[i] == null) continue;
    //        Vector3 direction = RybCenter[i].transform.position - transform.position; //λ�ò����     
    //        //if (Vector3.Dot(transform.forward, direction) > 0)
    //        //{
    //            //float dis = Vector3.Distance(RybCenter[i].transform.position, transform.position);
    //            //rybcenterdic.Add(dis, RybCenter[i]);
    //            //if (!rybcenterlist.Contains(dis))
    //            //{
    //            //    rybcenterlist.Add(dis);
    //            //}
    //        //}
    //        float dis = Vector3.Distance(RybCenter[i].transform.position, transform.position);
    //        //if (!rybcenterdic.ContainsKey(dis))
    //        //{
    //        //    rybcenterdic.Add(dis, RybCenter[i]);
    //        //}
    //        rybcenterdic.Add(dis, RybCenter[i]);
    //        if (!rybcenterlist.Contains(dis))
    //        {
    //            rybcenterlist.Add(dis);
    //        }
    //    }
    //    rybcenterlist.Sort();
    //    if (rybcenterlist.Count != 0)
    //    {
    //        rybcenterdic.TryGetValue(rybcenterlist[0], out GameObject obj);
    //        centerposition = obj.transform.position;
    //    }
    //    else
    //    {
    //        centerposition = Vector3.zero;
    //    }
    //    return centerposition;
    //}

    //�ж������ڵĳ���
    public int getArea()
    {
        Collider[] hitcolliders = Physics.OverlapBox(centerposition + offset, CarSlowArea, Quaternion.identity, layerMask:car);
        targercount = hitcolliders.Length;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            foreach(Collider col in hitcolliders)
            {
                print("��ǰ����Ϊ" + this.transform.name + "   ����" + targercount + col.name);
            }
            
        }
        //foreach (Collider col in hitcolliders)
        //{
        //    if (col.gameObject.CompareTag("car"))
        //    {
        //        targercount++;
        //        if (Input.GetKeyDown(KeyCode.Return))
        //        {
        //            print("��ǰ����" + this.transform.name + "����" + targercount);
        //            print(col.transform.name);
        //        }
        //    }
        //}
        return targercount;
    }
    

    //�Ⱥ��������߼�
    void Stopmove()
    {
        if (this.transform.name.Equals("xcar0"))
        {
            if(transform.position.x < centerposition.x - 10 && transform.position.x > centerposition.x - 14)
            {
                if (Xred0 == true)
                {
                    //v = 0;
                    aniSpeed = 0;
                }
                else
                {
                    //v = v1;
                    aniSpeed = 1;
                }
            }
            if (transform.position.x < centerposition.x - 1 && transform.position.x > centerposition.x - 5)
            {
                getArea();
                if (targercount >= 1)
                {
                    //v = 0;
                    aniSpeed = 0;
                }
                else
                {
                    //v = v1;
                    aniSpeed = 1;
                }
            }

        }
        
        //x1
        if (this.transform.name == "xcar1" && transform.position.x < centerposition.x + 14 && transform.position.x > centerposition.x + 10)
        {
            if (Xred1 == true)
            {
                //v = 0;
                aniSpeed = 0;
            }
            else
            {
                //v = v1;
                aniSpeed = 1;
            }
        }
        if (this.transform.name.Equals("xcar1") && transform.position.x < centerposition.x + 5 && transform.position.x > centerposition.x+1)
        {
            getArea();
            if (targercount >= 1)
            {
                //v = 0;
                aniSpeed = 0;
            }
            else
            {
                //v = v1;
                aniSpeed = 1;
            }
        }
        //z0
        if (this.transform.name == "zcar0" && transform.position.z < centerposition.z - 10 && transform.position.z > centerposition.z - 14)
        {
            if (Zred0 == true)
            {
                //v = 0;
                aniSpeed = 0;
            }
            else
            {
                //v = v1;
                aniSpeed = 1;
            }
        }
        if (this.transform.name.Equals("zcar0") && transform.position.z < centerposition.z-1  && transform.position.z > centerposition.z - 5)
        {
            getArea();
            if (targercount >= 1)
            {               
                aniSpeed = 0;//v = 0;
            }
            else
            {
                aniSpeed = 1;//v = v1;
            }
        }
        //z1
        if (this.transform.name == "zcar1" && transform.position.z < centerposition.z + 14 && transform.position.z > centerposition.z + 10)
        {
            if (Zred1 == true)
            {
                aniSpeed = 0;// v = 0;
            }
            else
            {
                aniSpeed = 1;//v = v1;
            }
        }
        if (this.transform.name.Equals("zcar1") && transform.position.z < centerposition.z + 5 && transform.position.z > centerposition.z+1)
        {
            getArea();
            if (targercount >= 1)
            {
                //ani.Stop();
                aniSpeed = 0;// v = 0;
            }
            else
            {
                //ani.Play();
                aniSpeed = 1;//v = v1;
            }
        }
    }

    public void SetAnimationSpeed()
    {
        if (null == ani) return;
        AnimationState state = ani["carTurnLeft"];
        state.speed = aniSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(centerposition + offset, CarSlowArea);
    }
}


