using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Reflection;

public class SaveMgr : MonoBehaviour
{
    public static SaveMgr Instance;

    public SaveData SaveData;

    public SaveList saveList;

    string filePath;

    private void Awake()
    {
        Instance = this;
        filePath = Application.persistentDataPath + "/SaveData.json";
        //NewGame();
        LoadGame();
    }

    public Road GetRoad(string roadName)
    {
        return SaveData.RoalList.Find(o => o.RoadName == roadName);
    }

    public void AddRoad(Road road)
    {
        SaveData.RoalList.Add(road);
    }

    public void RemoveRoad(string roadName)
    {
        var road = GetRoad(roadName);
        if (road != null)
            SaveData.RoalList.Remove(road);
    }

    public RYBpole GetRYBpole(string roadName)
    {
        return SaveData.RYBpoleList.Find(o => o.RoadName == roadName);
    }

    public void AddRYBpole(RYBpole road)
    {
        SaveData.RYBpoleList.Add(road);
    }

    public void RemoveRYBpole(string roadName)
    {
        var road = GetRYBpole(roadName);
        if (road != null)
            SaveData.RYBpoleList.Remove(road);
    }


    /// <summary>
    /// 存档
    /// </summary>
    public void SaveGame()
    {
        var savedata = JsonConvert.SerializeObject(saveList);

        FileInfo file = new(filePath);
        StreamWriter sw = file.CreateText();
        sw.WriteLine(savedata);
        sw.Close();
        sw.Dispose();
        Debug.LogError("已完成存档");
    }
    /// <summary>
    /// 读档
    /// </summary>
    public void LoadGame()
    {
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            var data = sr.ReadToEnd();
            sr.Close();
            saveList = JsonConvert.DeserializeObject<SaveList>(data);
        }
        else
        {
            NewGame();
        }
    }
    public void SetSaveData(int index)
    {
        SaveData = saveList.SaveDataList[index];
    }
    public void ResetSaveData(int index)
    {
        saveList.SaveDataList[index] = CreatSaveData(index);
        SaveGame();
    }

    /// <summary>
    /// 新开游戏
    /// </summary>
    public void NewGame()
    {
        var SaveData = CreatSaveData(0);
        var SaveData1 = CreatSaveData(1);
        var SaveData2 = CreatSaveData(2);

        saveList = new SaveList();
        saveList.SaveDataList = new List<SaveData>();
        saveList.SaveDataList.Add(SaveData);
        saveList.SaveDataList.Add(SaveData1);
        saveList.SaveDataList.Add(SaveData2);
        SaveGame();
    }

    private SaveData CreatSaveData(int ID)
    {
        var SaveData = new SaveData();
        SaveData.RoalList = new List<Road>();
        SaveData.RYBpoleList = new List<RYBpole>();
        SaveData.ID = ID;
        SaveData.Weather = 0;
        var a = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
        var b = Convert.ToInt64(a.TotalSeconds);
        SaveData.SaveTime = b;
        return SaveData;
    }

    public RYBpole CreatRYBpole()
    {
        RYBpole rYBpole = new RYBpole();
        rYBpole.LightCycle = 140;
        rYBpole.LightPos = new Vector2(0, 0);
        rYBpole.LightLeft_gt = new int[4] { 22, 22, 22, 22 };
        rYBpole.LightStraight_gt = new int[4] { 42, 42, 42, 42 };

        rYBpole.i = new int[4] { 0, 0, 0, 0 };
        rYBpole.dt = new int[4] { 0, 0, 70, 70 };
        rYBpole.sc = new int[4] { 0, 0, 2, 2 };
        rYBpole.li = new int[4] { 0, 0, 0, 0 };
        rYBpole.ldt = new int[4] { 45, 45, 115, 115 };
        rYBpole.lsc = new int[4] { 2, 2, 2, 2 };

        rYBpole.life = new int[4] { 500, 500, 500, 500 };
        rYBpole.speed = new int[4] { 10, 10, 10, 10 };
        rYBpole.minT = new int[4] { 5, 5, 5, 5 };
        rYBpole.maxT = new int[4] { 15, 15, 15, 15 };
        rYBpole.cTt = new int[4] { 60, 60, 60, 60 };

        return rYBpole;
    }
}

public class SaveList
{
    public List<SaveData> SaveDataList;
}

public class SaveData
{
    public int ID;

    /// 天气
    public int Weather;

    public List<Road> RoalList = new List<Road>();

    public List<RYBpole> RYBpoleList = new List<RYBpole>();

    public long SaveTime;

}

public class Road : RoadInfo
{

}

public class RYBpole : RoadInfo
{
    ///////////////////// 信号灯相关///////////////////
    //信号灯周期
    public int LightCycle;
    //信号灯坐标 x,y
    public Vector2 LightPos;
    //右转
    public int[] LightLeft_gt;
    //直行
    public int[] LightStraight_gt;

    ///////////////////// 参数///////////////////
    public int[] i;
    public int[] dt;
    public int[] sc;
    public int[] li;
    public int[] ldt;
    public int[] lsc;

    ///////////////////// 车辆///////////////////
    public int[] life;
    public int[] speed;
    public int[] minT;
    public int[] maxT;
    public int[] cTt;
}

public class RoadInfo
{
    //路类型
    public RoadType RoadType;
    //位置
    public Vector3 RoadPos;
    //选择角度
    public Vector3 RoadRotation;
    //比例
    public Vector3 RoadScale;

    public string RoadName;

    public int layer;
}



public enum RoadType
{
    e1, e2, r2, r3
}