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
//    /// �浵
//    /// </summary>
//    public void SaveGame()
//    {
//        //var savedata = JsonConvert.SerializeObject(saveList);
//        //FileInfo file = new FileInfo(filePath);
//        //StreamWriter sw = file.CreateText();
//        //sw.WriteLine(savedata);
//        //sw.Close();
//        //sw.Dispose();
//        //Debug.LogError("����ɴ浵");

//        //string savedata = JsonMapper.ToJson(saveList);
//        //StreamWriter sw = File.CreateText(filePath);
//        //sw.WriteLine(savedata);
//        //sw.Close();
//        //sw.Dispose();
//        //Debug.LogError("����ɴ浵");

//        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/newjson.txt", false, Encoding.UTF8);
//        for (int i = 0; i < saveList.SaveDataList.Count; i++)
//        {
//            sw.WriteLine(JsonUtility.ToJson(saveList.SaveDataList[i]));
//        }

//        sw.Close();


//    }
//    /// <summary>
//    /// ����
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
//            //�����Ǳ�ʾ������  �ȴ���������ļ�
//            StreamWriter SWfile = m_file.CreateText();

//            //��Ϊ��һ�����г����ǿ϶�����������ļ��ģ����������  ��Ҫȥ��ȡ StreamingAssets ���oldjson.txt
//            //WWW���÷���ʽ    ���о�û��StreamReader���ã�
//            //StreamingAssets��·����ʽ �������������
//            string url = Application.streamingAssetsPath + "/" + "SaveData /testjson.txt"; ;
//            //�������SkipBom������  �ҷŵ����沩��������⣬����������д  ����Ǿ޿ӣ���
//            string SkipBom;

//            string[] jsondata;

//            //WWW Ĭ���ǰ��ļ���Ϣ��ͷ��βȫ����ȡ����������������������������Ҫ�������У����в��� �Լ��� ÿһ�д���һ�������
//            //WWW ���ŵ���һЩ�ֽ���Ϣ �˽��ƻ� ʮ�����ơ����ļ������ʽ
//            WWW wread = new WWW(url);
//            //��һ����˼��,�õ�һ��UTF8������ַ��� ����wread���ֽ���Ϣ��ȥ������������ǰ��λ�ֽڣ��ӵ���λ�ֽ�ȥ���� Ϊʲô����λ�ֽڣ����Ľ��͡�
//            SkipBom = Encoding.UTF8.GetString(wread.bytes, 3, wread.bytes.Length - 3);
//            //������õ����ַ�������Ϊ������ȫ��json��Ϣ��Ҫ��һ��Split����,�Զ�ת�������顣  ������һ�����и�ʽ�� ��C#��Ԥ����ģ� {"\r\n"}
//            jsondata = SkipBom.Split(new string[] { "\r\n" }, StringSplitOptions.None);

//            //������Ĳ����� �ͺ�StreamReader��˼·һ���ˡ� ʹ�ñ����õ�ÿ�е�json��Ϣ��
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

//        //if (!File.Exists(filePath))                            //�ж��ļ��Ƿ����
//        //{
//        //    //�Ǿʹ�Application.streamingAsset·�����Ƶ�Application.persistentDataPath·���¡�
//        //    //���ļ���Application.streamingAsset·�����Ƶ�Application.persistentDataPath�ķ������£�
//        //    //�����ļ�
//        //    CopyFormStreamassetToPersistent();

//        //    string data = File.ReadAllText(filePath);
//        //    StreamWriter sw;
//        //    FileInfo t = new FileInfo(filePath);
//        //    sw = t.CreateText();
//        //    //������Ҫ���ļ�����д���ȥ
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
//        //    //    //��˳���ӡ��ÿ�е��ı���Ϣ �������{ }��json���������ַ�
//        //    //    //����Ҫע��һ���������⣬����ı���Ϣ��ȫӢ�ĵľ�û�£�����а������ĵ�Ҫʹ��UTF-8�ı��룬����ͳ�������.
//        //    //    Debug.Log(data);

//        //    //    //���json��Ϣ��������ǹ��캯���Ĳ�������д�����
//        //    //    saveList.SaveDataList.Add(JsonUtility.FromJson<SaveData>(data));
//        //    //}


//        //    string data = File.ReadAllText(filePath);

//        //    //saveList = JsonConvert.DeserializeObject<SaveList>(data);
//        //    saveList = JsonMapper.ToObject<SaveList>(data);
//        //}
//    }

//   //ʧ�ܲ���n
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
//    /// �¿���Ϸ
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

//    /// ����
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
//    ///////////////////// �źŵ����///////////////////
//    //�źŵ�����
//    public int LightCycle;
//    //�źŵ����� x,y
//    public Vector2 LightPos;
//    //��ת
//    public int[] LightLeft_gt;
//    //ֱ��
//    public int[] LightStraight_gt;

//    ///////////////////// ����///////////////////
//    public int[] i;
//    public int[] dt;
//    public int[] sc;
//    public int[] li;
//    public int[] ldt;
//    public int[] lsc;

//    ///////////////////// ����///////////////////
//    public int[] life;
//    public int[] speed;
//    public int[] minT;
//    public int[] maxT;
//    public int[] cTt;
//}

//public class RoadInfo
//{
//    //·����
//    public RoadType RoadType;
//    //λ��
//    public Vector3 RoadPos;
//    //ѡ��Ƕ�
//    public Vector3 RoadRotation;
//    //����
//    public Vector3 RoadScale;

//    public string RoadName;

//    public int layer;
//}



//public enum RoadType
//{
//    e1, e2, r2, r3
//}