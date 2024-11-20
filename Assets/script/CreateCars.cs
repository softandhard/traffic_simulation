
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCars : MonoBehaviour
{
    //传值x00x11
    public int whichlight;
    [Tooltip("车辆")]
    public GameObject[] cars ;

    [Tooltip("车辆生成点位")]
    public Transform []carCreatPoints;

    [Tooltip("车的存活时间")]
    public int carLifetime = 10;

    [Tooltip("车运行速度")]
    public int speed = 10;

    public float startinterval = 0;

    public int T = 30;
    public int interval = 3;
    public int intervalmax = 10;

    [Tooltip("是否为x方向")]
    public bool x=false;

    public bool creatcar = true;

    [Tooltip("碰撞检测范围")]
    public Vector3 size = new Vector3(4, 1, 4);
    Collider[] colliders;

    [Tooltip("达到汽车生成警戒值上限")]
    public int limitcars=15;

    [Tooltip("速度差值")]
    public int min;
    [Tooltip("速度差值")]
    public int max;

    //public bool[] creatcardir;

    RYBpoleBase rYBpoleBase;
    private RYBpole RYBpole;

    void Start()
    {
        if (transform.parent.GetComponent<RYBpoleBase>() != null)
        {
            rYBpoleBase = transform.parent.GetComponent<RYBpoleBase>();
            RYBpole = rYBpoleBase.RYBpole;
            if (RYBpole != null)
            {
                carLifetime = RYBpole.life[whichlight];
                speed = RYBpole.speed[whichlight];
                interval = RYBpole.minT[whichlight];
                intervalmax = RYBpole.maxT[whichlight];
                T = RYBpole.cTt[whichlight];
            }
        }
        StartCoroutine(ChangeValue());
        StartCoroutine(Creatcar());
        //InvokeRepeating(nameof(CreatCars), startinterval, interval);
    }
    // Update is called once per frame
    void Update()
    {
        //datatrans();
        //检查子节点的个数
        //for (int i = 0; i < carCreatPoints.Length; i++)
        //{
        //    if (carCreatPoints[i].childCount > 5)
        //    {
        //        creatcar = false;
        //    }
        //    else
        //    {
        //        creatcar = true;
        //    }
        //}           

        for (int i = 0; i < carCreatPoints.Length; i++)
        {
            Collider[] colliders = Physics.OverlapBox(carCreatPoints[i].position, size);
            int targercount = 0;
            foreach (Collider col in colliders)
            {
                if (col.gameObject.CompareTag("car"))
                {
                    targercount++;
                }
            }
            //float t ;
            //if (targercount >= 1)
            //{
            //    t = Time.deltaTime;
            //    if (t > 0.5f)
            //    {

            //        creatcar = false;
            //    }
            //    if (Input.GetMouseButtonDown(0))
            //    {
            //        print("当前的时间" + t);
            //    }

            //}
            //else
            //{
            //    t = 0;
            //    creatcar = true;
            //}

            //Debug.Log(targercount);
            if (targercount >= limitcars)
            {
                creatcar = false;
            }
            else
            {
                creatcar = true;
            }
            //if (Input.GetKeyDown(KeyCode.K))
            //{
            //    print(carCreatPoints[i].name + "   当前是否生成汽车" + creatcar +  "    &&&&&当前汽车数量为" + targercount);
            //}
            //print("当前是否生成汽车" +creatcar + "    &&&&&当前汽车数量为" + targercount);
        }


        //foreach (Collider col in colliders)
        //{
        //    GameObject obj = col.gameObject;
        //    if (obj.CompareTag("car"))
        //    {

        //        //i += 1;
        //        //Debug.Log("Found object: "+obj.name);
        //        //print(i);
        //        //if (i > 3)
        //        //{
        //        //    print("fffff") ;
        //        //}

        //        //float t;
        //        //t = Time.deltaTime;
        //        //for (int i = 0; i < carCreatPoints.Length; i++)
        //        //{
        //        //    float dis = Vector3.Distance(obj.transform.position, carCreatPoints[i].position);
        //        //    List<float> lis=new List<float>();
        //        //    lis.Add(dis);
        //        //    lis.Sort();
        //        //    if (lis[0]> 1)
        //        //    {
        //        //        t = 0;
        //        //    }
        //        //}    

        //        //if (t > 2)
        //        //{
        //        //    creatcar = false;
        //        //}
        //        //else
        //        //{
        //        //    creatcar = true;
        //        //}
        //    }

        //    //print("报警，已超出范围");

        //}

    }

    //生成间隔周期变化
    private IEnumerator ChangeValue()
    {
        int intervalValue = interval;
        interval = 4;
        bool increasing = false;

        while (true)
        {
            if (increasing)
            {
                interval = intervalmax;
                increasing = false;
                //interval += Time.deltaTime;
                //if (interval >= 6f)
                //{
                //    interval = intervalmax;
                //    increasing = false;

                //}
            }
            else
            {
                interval = intervalValue;
                increasing = true;
                //interval -= Time.deltaTime;
                //if (interval <= 8f)
                //{
                //    interval = intervalValue;
                //    increasing = true;
                //}
            }

            //Debug.Log(interval);

            yield return new WaitForSeconds(T);
        }
    }
    IEnumerator Creatcar()
    {
        while (true)
        {
            int n = Random.Range(0, cars.Length);
            int m = Random.Range(0, carCreatPoints.Length);

            if (creatcar == true)
            {
                GameObject car = Instantiate(cars[n], carCreatPoints[m]);
                car.GetComponent<CarMove>().carLifetime = carLifetime;
                car.GetComponent<CarMove>().speed = speed + Random.Range(-min, max + 1);
            }
            yield return new WaitForSeconds(interval);
        }

       
    }


    //void CreatCars()
    //{
    //    int n = Random.Range(0, cars.Length);
    //    int m = Random.Range(0, carCreatPoints.Length);

    //    //GameObject[] movingcars = GameObject.FindGameObjectsWithTag("car");
    //    //for (int a=0; a < movingcars.Length; a++)
    //    //{
    //    //    for(int b = 0; b < carCreatPoints.Length; b++)
    //    //    {
    //    //        if (Vector3.Distance(movingcars[a].transform.position, carCreatPoints[b].position)>3)
    //    //        {
    //    //            GameObject car = Instantiate(cars[n], carCreatPoints[m]);
    //    //            car.GetComponent<CarMove>().carLifetime = carLifetime;
    //    //            car.GetComponent<CarMove>().speed = speed;
    //    //        }

    //    //    }
    //    //}

    //    if (creatcar==true)
    //    {
    //        GameObject car = Instantiate(cars[n], carCreatPoints[m]);
    //        car.GetComponent<CarMove>().carLifetime = carLifetime;
    //        car.GetComponent<CarMove>().speed = speed + Random.Range(-min, max+1);
    //    }
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < carCreatPoints.Length; i++)
        {
            Gizmos.DrawWireCube(carCreatPoints[i].position, size);
        }
        
    }
    //void datatrans()
    //{
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    GlobalVariable globalVariable = transform.parent.GetComponent<GlobalVariable>();
        //    if (whichlight == 0)
        //    {
        //        carLifetime = globalVariable.num1[25];
        //        speed = globalVariable.num1[26];
        //        interval = globalVariable.num1[27];
        //        intervalmax = globalVariable.num1[28];
        //        T = globalVariable.num1[29];
        //    }
        //    if (whichlight == 1)
        //    {
        //        carLifetime = globalVariable.num1[30];
        //        speed = globalVariable.num1[31];
        //        interval = globalVariable.num1[32];
        //        intervalmax = globalVariable.num1[33];
        //        T = globalVariable.num1[34];
        //    }
        //    if (whichlight == 2)
        //    {
        //        carLifetime = globalVariable.num1[35];
        //        speed = globalVariable.num1[36];
        //        interval = globalVariable.num1[37];
        //        intervalmax = globalVariable.num1[38];
        //        T = globalVariable.num1[39];
        //    }
        //    if (whichlight == 3)
        //    {
        //        carLifetime = globalVariable.num1[40];
        //        speed = globalVariable.num1[41];
        //        interval = globalVariable.num1[42];
        //        intervalmax = globalVariable.num1[43];
        //        T = globalVariable.num1[44];
        //    }
        //}
    //}
}
