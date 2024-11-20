//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Newtonsoft.Json;
//using LitJson;
//using System.IO;
//using System;
//using System.Reflection;
//using Unity.VisualScripting;
//using System.Text;

//public class SaveMgr : MonoBehaviour
//{
//    public static SaveMgr Instance;

//    public SaveData SaveData;

//    public SaveList saveList;

//    string filePath;

//    private void Awake()
//    {
//        filePath = Application.persistentDataPath + "/new.txt";
//        Instance = this;
//        //NewGame();
//        LoadGame();
//    }

//    public Road GetRoad(string roadName)
//    {
//        return SaveData.RoalList.Find(o => o.RoadName == roadName);
//    }

//    public void AddRoad(Road road)
//    {
//        SaveData.RoalList.Add(road);
//    }

//    public void RemoveRoad(string roadName)
//    {
//        var road = GetRoad(roadName);
//        if (road != null)
//            SaveData.RoalList.Remove(road);
//    }

//    public RYBpole GetRYBpole(string roadName)
//    {
//        return SaveData.RYBpoleList.Find(o => o.RoadName == roadName);
//    }

//    public void AddRYBpole(RYBpole road)
//    {
//        SaveData.RYBpoleList.Add(road);
//    }

//    public void RemoveRYBpole(string roadName)
//    {
//        var road = GetRYBpole(roadName);
//        if (road != null)
//            SaveData.RYBpoleList.Remove(road);
//    }


//    /// <summary>
//    /// 存档
//    /// </summary>
//    public void SaveGame()
//    {
//        //var savedata = JsonConvert.SerializeObject(saveList);
//        //FileInfo file = new FileInfo(filePath);
//        //StreamWriter sw = file.CreateText();
//        //sw.WriteLine(savedata);
//        //sw.Close();
//        //sw.Dispose();
//        //Debug.LogError("已完成存档");

//        //string savedata = JsonMapper.ToJson(saveList);
//        //StreamWriter sw = File.CreateText(filePath);
//        //sw.WriteLine(savedata);
//        //sw.Close();
//        //sw.Dispose();
//        //Debug.LogError("已完成存档");

//        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/newjson.txt", false, Encoding.UTF8);
//        for (int i = 0; i < saveList.SaveDataList.Count; i++)
//        {
//            sw.WriteLine(JsonUtility.ToJson(saveList.SaveDataList[i]));
//        }

//        sw.Close();


//    }
//    /// <summary>
//    /// 读档
//    /// </summary>
//    public void LoadGame()
//    {
//        //1
//        //StreamReader sr = new StreamReader(filePath);
//        //var data = sr.ReadToEnd();
//        //sr.Close();
//        //saveList = JsonConvert.DeserializeObject<SaveList>(data);
//        //2
//        //string data = File.ReadAllText(filePath);
//        //saveList = JsonMapper.ToObject<SaveList>(data);

//        FileInfo m_file = new FileInfo(filePath);
//        if (!m_file.Exists)
//        {
//            //这里是表示不存在  先创建出这个文件
//            StreamWriter SWfile = m_file.CreateText();

//            //因为第一次运行程序是肯定不存在这个文件的，会进到这里  需要去读取 StreamingAssets 里的oldjson.txt
//            //WWW的用法格式    （感觉没有StreamReader好用）
//            //StreamingAssets的路径格式 建议用下面这个
//            string url = Application.streamingAssetsPath + "/" + "SaveData /testjson.txt"; ;
//            //关于这个SkipBom的意义  我放到后面博文里做详解，代码里先照写  这个是巨坑！！
//            string SkipBom;

//            string[] jsondata;

//            //WWW 默认是把文件信息从头到尾全部读取出来，放在下面这个变量里。我们需要对它进行，换行操作 以及把 每一行存在一个数组里。
//            //WWW 里存放的是一些字节信息 八进制或 十六进制。看文件编码格式
//            WWW wread = new WWW(url);
//            //这一句意思是,得到一个UTF8编码的字符串 ，从wread的字节信息里去读，但是跳过前三位字节，从第四位字节去读。 为什么是三位字节，后文解释。
//            SkipBom = Encoding.UTF8.GetString(wread.bytes, 3, wread.bytes.Length - 3);
//            //将上面得到的字符串，因为包含了全部json信息，要做一个Split换行,自动转换成数组。  这里有一个换行格式符 是C#里预定义的， {"\r\n"}
//            jsondata = SkipBom.Split(new string[] { "\r\n" }, StringSplitOptions.None);

//            //到这里的操作后 就和StreamReader的思路一样了。 使用遍历得到每行的json信息。
//            foreach (string item in jsondata)
//            {
//                saveList.SaveDataList.Add(JsonUtility.FromJson<SaveData>(item));
//            }



//        }
   


//        else
//        {
//            StreamReader sr = new StreamReader(Application.persistentDataPath + "/new.txt");
//            string nextLine;
//            while ((nextLine = sr.ReadLine()) != null)
//            {
//                saveList.SaveDataList.Add(JsonUtility.FromJson<SaveData>(nextLine));
//            }

//            sr.Close();

//            //var data = File.ReadAllText(filePath);
//            //saveList = JsonMapper.ToObject<SaveList>(data);

//        }

//        //if (!File.Exists(filePath))                            //判断文件是否存在
//        //{
//        //    //那就从Application.streamingAsset路径复制到Application.persistentDataPath路径下。
//        //    //将文件从Application.streamingAsset路径复制到Application.persistentDataPath的方法如下：
//        //    //创建文件
//        //    CopyFormStreamassetToPersistent();

//        //    string data = File.ReadAllText(filePath);
//        //    StreamWriter sw;
//        //    FileInfo t = new FileInfo(filePath);
//        //    sw = t.CreateText();
//        //    //把你需要的文件数据写入进去
//        //    sw.Write(data.ToString());
//        //    sw.Close();
//        //    sw.Dispose();


//        //}

//        //else if (File.Exists(filePath))
//        //{
//        //    //StreamReader sr = new StreamReader(filePath);
//        //    //var data = sr.ReadToEnd();
//        //    //sr.Close();

//        //    //data = sr.ReadLine();
//        //    //while ((data = sr.ReadLine()) != null)
//        //    //{
//        //    //    //按顺序打印出每行的文本信息 会包含“{ }”json的中括号字符
//        //    //    //这里要注意一个编码问题，如果文本信息是全英文的就没事，如果有包含中文的要使用UTF-8的编码，否则就出现乱码.
//        //    //    Debug.Log(data);

//        //    //    //如果json信息里包含的是构造函数的参数，就写在这里。
//        //    //    saveList.SaveDataList.Add(JsonUtility.FromJson<SaveData>(data));
//        //    //}


//        //    string data = File.ReadAllText(filePath);

//        //    //saveList = JsonConvert.DeserializeObject<SaveList>(data);
//        //    saveList = JsonMapper.ToObject<SaveList>(data);
//        //}
//    }

//   //失败测试n
//   // public void CopyFormStreamassetToPersistent()
//   // {
//   //     string formPath;
//   //     string toPath;
//   //     formPath = Application.streamingAssetsPath + "/" + "SaveData /testjson.txt";
//   //     toPath =filePath;
//   //     WWW w = new WWW(formPath);
//   //while (!w.isDone) { }
//   //     if (w.error == null)
//   //     {
//   //         File.WriteAllBytes(toPath, w.bytes);
//   //     }
//   // }

//    public void SetSaveData(int index)
//    {
//        SaveData = saveList.SaveDataList[index];
//    }
//    public void ResetSaveData(int index)
//    {
//        saveList.SaveDataList[index] = CreatSaveData(index);
//        SaveGame();
//    }

//    /// <summary>
//    /// 新开游戏
//    /// </summary>
//    public void NewGame()
//    {
//        var SaveData = CreatSaveData(0);
//        var SaveData1 = CreatSaveData(1);
//        var SaveData2 = CreatSaveData(2);

//        saveList = new SaveList();
//        saveList.SaveDataList = new List<SaveData>();
//        saveList.SaveDataList.Add(SaveData);
//        saveList.SaveDataList.Add(SaveData1);
//        saveList.SaveDataList.Add(SaveData2);
//        SaveGame();
//    }

//    private SaveData CreatSaveData(int ID)
//    {
//        var SaveData = new SaveData();
//        SaveData.RoalList = new List<Road>();
//        SaveData.RYBpoleList = new List<RYBpole>();
//        SaveData.ID = ID;
//        SaveData.Weather = 0;
//        var a = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
//        var b = Convert.ToInt64(a.TotalSeconds);
//        SaveData.SaveTime = b;
//        return SaveData;
//    }

//    public RYBpole CreatRYBpole()
//    {
//        RYBpole rYBpole = new RYBpole();
//        rYBpole.LightCycle = 140;
//        rYBpole.LightPos = new Vector2(0, 0);
//        rYBpole.LightLeft_gt = new int[4] { 22, 22, 22, 22 };
//        rYBpole.LightStraight_gt = new int[4] { 42, 42, 42, 42 };

//        rYBpole.i = new int[4] { 0, 0, 0, 0 };
//        rYBpole.dt = new int[4] { 0, 0, 70, 70 };
//        rYBpole.sc = new int[4] { 0, 0, 2, 2 };
//        rYBpole.li = new int[4] { 0, 0, 0, 0 };
//        rYBpole.ldt = new int[4] { 45, 45, 115, 115 };
//        rYBpole.lsc = new int[4] { 2, 2, 2, 2 };

//        rYBpole.life = new int[4] { 500, 500, 500, 500 };
//        rYBpole.speed = new int[4] { 10, 10, 10, 10 };
//        rYBpole.minT = new int[4] { 5, 5, 5, 5 };
//        rYBpole.maxT = new int[4] { 15, 15, 15, 15 };
//        rYBpole.cTt = new int[4] { 60, 60, 60, 60 };

//        return rYBpole;
//    }
//}

//public class SaveList
//{
//    public List<SaveData> SaveDataList = new();
//}

//public class SaveData
//{
//    public int ID;

//    /// 天气
//    public int Weather;

//    public List<Road> RoalList = new List<Road>();

//    public List<RYBpole> RYBpoleList = new List<RYBpole>();

//    public long SaveTime;

//}

//public class Road : RoadInfo
//{

//}

//public class RYBpole : RoadInfo
//{
//    ///////////////////// 信号灯相关///////////////////
//    //信号灯周期
//    public int LightCycle;
//    //信号灯坐标 x,y
//    public Vector2 LightPos;
//    //右转
//    public int[] LightLeft_gt;
//    //直行
//    public int[] LightStraight_gt;

//    ///////////////////// 参数///////////////////
//    public int[] i;
//    public int[] dt;
//    public int[] sc;
//    public int[] li;
//    public int[] ldt;
//    public int[] lsc;

//    ///////////////////// 车辆///////////////////
//    public int[] life;
//    public int[] speed;
//    public int[] minT;
//    public int[] maxT;
//    public int[] cTt;
//}

//public class RoadInfo
//{
//    //路类型
//    public RoadType RoadType;
//    //位置
//    public Vector3 RoadPos;
//    //选择角度
//    public Vector3 RoadRotation;
//    //比例
//    public Vector3 RoadScale;

//    public string RoadName;

//    public int layer;
//}



//public enum RoadType
//{
//    e1, e2, r2, r3
//}