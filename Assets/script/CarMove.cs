using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public static CarMove instance;
    [Tooltip("车的存活时间")]
    public float carLifetime;

    [Tooltip("车运行速度")]
    public float speed = 10;

    public float truespeed;

    private GameObject[] Xtarget0;
    private List<float> Xlist0;
    private Dictionary<float, GameObject> Xdic0;
    private bool Xred0 = false;


    private GameObject[] Xtarget1;
    private List<float> Xlist1;
    private Dictionary<float, GameObject> Xdic1;
    private bool Xred1 = false;


    private GameObject[] Ztarget0;
    private List<float> Zlist0;
    private Dictionary<float, GameObject> Zdic0;
    private bool Zred0 = false;

    private GameObject[] Ztarget1;
    private List<float> Zlist1;
    private Dictionary<float, GameObject> Zdic1;
    private bool Zred1 = false;

    private GameObject[] RybCenter;
    private List<float> rybcenterlist;
    private Dictionary<float, GameObject> rybcenterdic;

    private GameObject[] roadCenter;
    private List<float> roadcenterlist;
    private Dictionary<float, GameObject> roadcenterdic;

    private int safedis;
    //路口,道路,中心
    Vector3 pos;
    Vector3 posroad;
    // Start is called before the first frame update
    void Start()
    {

        safedis = Random.Range(3, 7);
        //print(this.transform.parent.name);
        instance = this;
        Invoke(nameof(SelfDestroy), carLifetime);

        Xtarget0 = GameObject.FindGameObjectsWithTag("Xred1");
        Xlist0 = new List<float>();
        Xdic0 = new Dictionary<float, GameObject>();

        Xtarget1 = GameObject.FindGameObjectsWithTag("Xred0");
        Xlist1 = new List<float>();
        Xdic1 = new Dictionary<float, GameObject>();

        Ztarget0 = GameObject.FindGameObjectsWithTag("Zred1");
        Zlist0 = new List<float>();
        Zdic0 = new Dictionary<float, GameObject>();


        Ztarget1 = GameObject.FindGameObjectsWithTag("Zred0");
        Zlist1 = new List<float>();
        Zdic1 = new Dictionary<float, GameObject>();

        RybCenter = GameObject.FindGameObjectsWithTag("RYBpole");
        rybcenterlist = new List<float>();
        rybcenterdic = new Dictionary<float, GameObject>();

        roadCenter = GameObject.FindGameObjectsWithTag("road");
        roadcenterlist = new List<float>();
        roadcenterdic = new Dictionary<float, GameObject>();

        InvokeRepeating(nameof(countdistance), 0, 0.5f);
        InvokeRepeating(nameof(GetCenterpoint), 0, 0.5f);
        InvokeRepeating(nameof(CheckSelfPos), 5, 1);
        truespeed = speed;

        StartCoroutine(CheckDisChange());

    }



    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, truespeed * Time.deltaTime);
        getdis();
        CarSlowdown();
        //CarAccelerate();
        StopMove();
    }

    IEnumerator CheckDisChange()
    {
        float previousValue = 0;
        while (true)
        {
            float currentValue = getdis();
            float reactiondis = Random.Range(0.1f, 0.5f);
            if (Mathf.Abs(currentValue - previousValue) > reactiondis)
            {
                if (truespeed < 0.5f * speed)
                {
                    //truespeed += speed * Time.deltaTime;
                    truespeed += 1;
                }

            }
            previousValue = currentValue;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public float getdis()
    {
        float x;
        Ray ray = new(transform.position, transform.forward);
        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit) && (hit.collider.gameObject.CompareTag("car") || hit.collider.gameObject.CompareTag("Knife")))
        {
            x = hit.distance;
        }
        else
        {
            x = 0;
        }
        return x;
    }
    //void CarAccelerate()
    //{

    //}
    void CarSlowdown()
    {
        float distance = getdis();

        //射线检测与前车的距离，
        if (distance < 15 && distance >= 10)
        {
            if (truespeed > 4)
            {
                //print("正在减速");
                truespeed -= 6 * Time.deltaTime;

            }
            //Debug.Log("射线碰撞对象：" + hit.collider.gameObject.name);
            //truespeed =0.4f;
        }
        else if (distance < 10 && distance >= safedis)
        {
            if (truespeed > 2)
            {
                truespeed -= 3 * Time.deltaTime;
            }
            //Debug.Log("射线碰撞对象：" + hit.collider.gameObject.name);
            //truespeed =0.1f;
        }
        else if (distance < safedis && distance > 0)
        {
            //Debug.Log("射线碰撞对象：" + hit.collider.gameObject.name);
            truespeed = 0;
        }
        else if (distance == 0)
        {
            if (truespeed <= speed)
            {
                truespeed += 5 * Time.deltaTime;
            }
        }
        else
        {
            if (truespeed <= speed)
            {
                truespeed += 5 * Time.deltaTime;
            }
        }
    }


    void StopMove()
    {

        if (this.transform.parent.name == "xpoint0" && transform.position.x < pos.x - 10 && transform.position.x > pos.x - 14)
        {
            if (Xred0 == true)
            {
                transform.Translate(0, 0, -truespeed * Time.deltaTime);
            }
        }

        if (this.transform.parent.name == "xpoint1" && transform.position.x < pos.x + 14 && transform.position.x > pos.x + 10)
        {
            if (Xred1 == true)
            {
                transform.Translate(0, 0, -truespeed * Time.deltaTime);
            }
        }

        if (this.transform.parent.name == "zpoint0" && transform.position.z < pos.z - 10 && transform.position.z > pos.z - 14)
        {
            if (Zred0 == true)
            {
                transform.Translate(0, 0, -truespeed * Time.deltaTime);
            }
        }

        if (this.transform.parent.name == "zpoint1" && transform.position.z < pos.z + 14 && transform.position.z > pos.z + 10)
        {
            if (Zred1 == true)
            {
                transform.Translate(0, 0, -truespeed * Time.deltaTime);
            }
        }
    }

    void GetCenterpoint()
    {
        rybcenterlist.Clear();
        rybcenterdic.Clear();

        for (int i = 0; i < RybCenter.Length; i++)
        {
            //Vector3 direction = RybCenter[i].transform.position - transform.position; //位置差，方向     
            //if (Vector3.Dot(transform.forward, direction) > 0)
            //{
            //    float dis = Vector3.Distance(RybCenter[i].transform.position, transform.position);
            //    rybcenterdic.Add(dis, RybCenter[i]);
            //    if (!rybcenterlist.Contains(dis))
            //    {
            //        rybcenterlist.Add(dis);
            //    }
            //}
            float dis = Vector3.Distance(RybCenter[i].transform.position, transform.position);
            rybcenterdic.Add(dis, RybCenter[i]);
            if (!rybcenterlist.Contains(dis))
            {
                rybcenterlist.Add(dis);
            }
        }

        rybcenterlist.Sort();

        if (rybcenterlist.Count != 0)
        {
            rybcenterdic.TryGetValue(rybcenterlist[0], out GameObject obj);
            if(obj != null)
                pos = obj.transform.position;

        }
        else
        {
            pos = Vector3.zero;
        }


        //road
        roadcenterlist.Clear();
        roadcenterdic.Clear();

        for (int i = 0; i < roadCenter.Length; i++)
        {
            Vector3 direction = roadCenter[i].transform.position - transform.position; //位置差，方向     
            float dis = Vector3.Distance(roadCenter[i].transform.position, transform.position);
            roadcenterdic.Add(dis, roadCenter[i]);
            if (!roadcenterlist.Contains(dis))
            {
                roadcenterlist.Add(dis);
            }
        }

        roadcenterlist.Sort();

        if (roadcenterlist.Count != 0)
        {
            roadcenterdic.TryGetValue(roadcenterlist[0], out GameObject objroad);
            posroad = objroad.transform.position;
        }
        else
        {
            posroad = Vector3.zero;
        }
    }
    void countdistance()
    {
        //X0
        Xdic0.Clear();
        Xlist0.Clear();

        for (int i = 0; i < Xtarget0.Length; i++)
        {
            Vector3 direction = Xtarget0[i].transform.position - transform.position; //位置差，方向     
            if (Vector3.Dot(transform.forward, direction) > 0)
            {
                float dis = Vector3.Distance(Xtarget0[i].transform.position, transform.position);
                Xdic0.Add(dis, Xtarget0[i]);
                if (!Xlist0.Contains(dis))
                {
                    Xlist0.Add(dis);
                }
            }
        }
        Xlist0.Sort();
        if (Xlist0.Count != 0)
        {
            Xdic0.TryGetValue(Xlist0[0], out GameObject obj);
            //Debug.Log("最近的X灯"+obj.transform.parent.parent.name);
            if (obj.transform.GetComponent<MeshRenderer>().material.color == Color.red)
            {
                Xred0 = true;
                //Debug.Log("x是红灯");
                //SendMessage(nameof(X),Xred);
            }
            else
            {
                Xred0 = false;
            }
        }

        //X1
        Xdic1.Clear();
        Xlist1.Clear();

        for (int i = 0; i < Xtarget1.Length; i++)
        {
            Vector3 direction = Xtarget1[i].transform.position - transform.position;
            if (Vector3.Dot(transform.forward, direction) > 0)
            {
                float dis = Vector3.Distance(Xtarget1[i].transform.position, transform.position);
                Xdic1.Add(dis, Xtarget1[i]);
                if (!Xlist1.Contains(dis))
                {
                    Xlist1.Add(dis);
                }
            }
        }
        Xlist1.Sort();
        if (Xlist1.Count != 0)
        {
            Xdic1.TryGetValue(Xlist1[0], out GameObject xobj1);
            //Debug.Log("最近的X灯"+obj.transform.parent.parent.name);
            if (xobj1.transform.GetComponent<MeshRenderer>().material.color == Color.red)
            {
                Xred1 = true;
                //Debug.Log("x是红灯");
                //SendMessage(nameof(X),Xred);
            }
            else
            {
                Xred1 = false;
            }
        }

        //z0
        Zdic0.Clear();
        Zlist0.Clear();

        for (int l = 0; l < Ztarget0.Length; l++)
        {
            Vector3 direction = Ztarget0[l].transform.position - transform.position;
            if (Vector3.Dot(transform.forward, direction) > 0)
            {
                float zdis = Vector3.Distance(Ztarget0[l].transform.position, transform.position);
                Zdic0.Add(zdis, Ztarget0[l]);
                if (!Zlist0.Contains(zdis))
                {
                    Zlist0.Add(zdis);
                }
            }
        }
        Zlist0.Sort();
        if (Zlist0.Count != 0)
        {
            Zdic0.TryGetValue(Zlist0[0], out GameObject zobj);
            //Debug.Log("最近的Z灯"+zobj.transform.parent.parent.name);
            if (zobj.transform.GetComponent<MeshRenderer>().material.color == Color.red)
            {
                Zred0 = true;
                //print("z是红灯不可以走");

            }
            else
            {
                Zred0 = false;
                //print("z不是红灯，可以走");
            }

        }



        //z1
        Zdic1.Clear();
        Zlist1.Clear();

        for (int l = 0; l < Ztarget1.Length; l++)
        {
            Vector3 direction = Ztarget1[l].transform.position - transform.position;
            if (Vector3.Dot(transform.forward, direction) > 0)
            {
                float zdis = Vector3.Distance(Ztarget1[l].transform.position, transform.position);
                Zdic1.Add(zdis, Ztarget1[l]);
                if (!Zlist1.Contains(zdis))
                {
                    Zlist1.Add(zdis);
                }
            }
        }
        Zlist1.Sort();
        if (Zlist1.Count != 0)
        {
            Zdic1.TryGetValue(Zlist1[0], out GameObject zobj1);
            //Debug.Log("最近的Z灯"+zobj.transform.parent.parent.name);
            if (zobj1.transform.GetComponent<MeshRenderer>().material.color == Color.red)
            {
                Zred1 = true;
                //print("z是红灯不可以走");

            }
            else
            {
                Zred1 = false;
                //print("z不是红灯，可以走");
            }

        }

    }
    public void CheckSelfPos()
    {
        //超出范围自动销毁
        if ((transform.position.x < pos.x - 70 || transform.position.x > pos.x + 70 ||
            transform.position.z < pos.z - 70 || transform.position.z > pos.z + 70) &&
            (transform.position.x < posroad.x - 70 || transform.position.x > posroad.x + 70 ||
            transform.position.z < posroad.z - 70 || transform.position.z > posroad.z + 70))
        {
            Invoke(nameof(SelfDestroy), 1);
            //Debug.Log("xiaohu");
        }
    }
    //销毁
    private void SelfDestroy()
    {
        Object.Destroy(this.gameObject);
    }
}





