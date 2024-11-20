using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Saveprefab : MonoBehaviour
{
    //预制体
    public GameObject e1;
    public GameObject e2;
    public GameObject road2;
    public GameObject road3;
    //名称
    string name1;
    public string[] Prename1;
    public string[] Prename2;
    public string[] Prename3;
    public string[] Prename4;

    //数量
    int prefabCount;

    //位置
    public Transform prefabfold;
    float posx;
    float posy;
    float posz;

    private void Awake()
    {
        for (int i = 0; i < 100; i++)
        {
            Prename1[i] += i.ToString();
            Prename2[i] += i.ToString();
            Prename3[i] += i.ToString();
            Prename4[i] += i.ToString();

        }
        //createPrefab();
    }
    void Update()
    {
        
    }
    //public void createPrefab()
    //{
        //for (int i = 0; i < 100; i++)
        //{
        //    if (PlayerPrefs.HasKey(Prename1[i]))
        //    {
        //        //print("保存了物体名称");
        //        Instantiate(e1, prefabfold);
        //        e1.name = PlayerPrefs.GetString(Prename1[i]);
        //        if (PlayerPrefs.HasKey(Prename1[i] + "x") && PlayerPrefs.HasKey(Prename1[i] + "y") && PlayerPrefs.HasKey(Prename1[i] + "z"))
        //        {
        //            //print("有位置坐标被保存");
        //            posx = PlayerPrefs.GetFloat(Prename1[i] + "x", 0);
        //            posy = PlayerPrefs.GetFloat(Prename1[i] + "y", 0);
        //            posz = PlayerPrefs.GetFloat(Prename1[i] + "z", 0);
        //            e1.transform.position = new Vector3(posx, posy, posz);
        //        }
        //    }
        //    else if (PlayerPrefs.HasKey(Prename2[i]))
        //    {
        //        Instantiate(e2, prefabfold);
        //        e2.name = PlayerPrefs.GetString(Prename2[i]);
        //        if (PlayerPrefs.HasKey(Prename2[i] + "x") && PlayerPrefs.HasKey(Prename2[i] + "y") && PlayerPrefs.HasKey(Prename2[i] + "z"))
        //        {
        //            posx = PlayerPrefs.GetFloat(Prename2[i] + "x", 0);
        //            posy = PlayerPrefs.GetFloat(Prename2[i] + "y", 0);
        //            posz = PlayerPrefs.GetFloat(Prename2[i] + "z", 0);
        //            e2.transform.position = new Vector3(posx, posy, posz);
        //        }
        //    }
        //    else if (PlayerPrefs.HasKey(Prename3[i]))
        //    {
        //        Instantiate(road2, prefabfold);
        //        road2.name = PlayerPrefs.GetString(Prename3[i]);
        //        if (PlayerPrefs.HasKey(Prename3[i] + "x") && PlayerPrefs.HasKey(Prename3[i] + "y") && PlayerPrefs.HasKey(Prename3[i] + "z"))
        //        {
        //            posx = PlayerPrefs.GetFloat(Prename3[i] + "x", 0);
        //            posy = PlayerPrefs.GetFloat(Prename3[i] + "y", 0);
        //            posz = PlayerPrefs.GetFloat(Prename3[i] + "z", 0);
        //            road2.transform.position = new Vector3(posx, posy, posz);
        //        }
        //    }
        //    else if (PlayerPrefs.HasKey(Prename4[i]))
        //    {
        //        Instantiate(road3, prefabfold);
        //        road3.name = PlayerPrefs.GetString(Prename4[i]);
        //        if (PlayerPrefs.HasKey(Prename4[i] + "x") && PlayerPrefs.HasKey(Prename4[i] + "y") && PlayerPrefs.HasKey(Prename4[i] + "z"))
        //        {
        //            posx = PlayerPrefs.GetFloat(Prename4[i] + "x", 0);
        //            posy = PlayerPrefs.GetFloat(Prename4[i] + "y", 0);
        //            posz = PlayerPrefs.GetFloat(Prename4[i] + "z", 0);
        //            road3.transform.position = new Vector3(posx, posy, posz);
        //        }
        //    }

        //}
    //}
    //public void SavePrefab()
    //{
    //    print("testsave!!");
    //    int num1 = 0;
    //    foreach (Transform crossroad in this.transform)
    //    {
    //        if (crossroad != null)
    //        {
    //            if(crossroad.name.Contains("e1"))
    //            {
    //                name1 = crossroad.transform.name;
    //                PlayerPrefs.SetString(Prename1[num1], name1);
    //                posx = crossroad.transform.position.x;
    //                PlayerPrefs.SetFloat(Prename1[num1] + "x", posx);
    //                posy = crossroad.transform.position.y;
    //                PlayerPrefs.SetFloat(Prename1[num1] + "y", posy);
    //                posz = crossroad.transform.position.z;
    //                PlayerPrefs.SetFloat(Prename1[num1] + "z", posz);
    //            }
    //            else if (crossroad.name.Contains("e2"))
    //            {
    //                name1 = crossroad.transform.name;
    //                PlayerPrefs.SetString(Prename2[num1], name1);
    //                posx = crossroad.transform.position.x;
    //                PlayerPrefs.SetFloat(Prename2[num1] + "x", posx);
    //                posy = crossroad.transform.position.y;
    //                PlayerPrefs.SetFloat(Prename2[num1] + "y", posy);
    //                posz = crossroad.transform.position.z;
    //                PlayerPrefs.SetFloat(Prename2[num1] + "z", posz);
    //            }
    //            else if (crossroad.name.Contains("r2"))
    //            {
    //                name1 = crossroad.transform.name;
    //                PlayerPrefs.SetString(Prename3[num1], name1);
    //                posx = crossroad.transform.position.x;
    //                PlayerPrefs.SetFloat(Prename3[num1] + "x", posx);
    //                posy = crossroad.transform.position.y;
    //                PlayerPrefs.SetFloat(Prename3[num1] + "y", posy);
    //                posz = crossroad.transform.position.z;
    //                PlayerPrefs.SetFloat(Prename3[num1] + "z", posz);
    //            }
    //            else if (crossroad.name.Contains("r3"))
    //            {
    //                name1 = crossroad.transform.name;
    //                PlayerPrefs.SetString(Prename4[num1], name1);
    //                posx = crossroad.transform.position.x;
    //                PlayerPrefs.SetFloat(Prename4[num1] + "x", posx);
    //                posy = crossroad.transform.position.y;
    //                PlayerPrefs.SetFloat(Prename4[num1] + "y", posy);
    //                posz = crossroad.transform.position.z;
    //                PlayerPrefs.SetFloat(Prename4[num1] + "z", posz);
    //            }
    //        }
    //        num1 += 1;
    //    }
    //}
}
