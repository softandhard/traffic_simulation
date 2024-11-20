using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
        Debug.Log(SaveMgr.Instance.SaveData.RoalList.Count);
        foreach (var Item in SaveMgr.Instance.SaveData.RoalList)
        {
            CreatePlaceObj(Item);
        }

        foreach (var Item in SaveMgr.Instance.SaveData.RYBpoleList)
        {
            CreateRYBpoleObj(Item);
        }
    }

    void CreatePlaceObj(Road road)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>($"Prefab/{road.RoadType.ToString()}"));
        obj.transform.position = road.RoadPos;
        obj.transform.localEulerAngles = road.RoadRotation;
        obj.transform.localScale = road.RoadScale;
        obj.name = road.RoadName;
        //�ı���������LayerΪDrag���Ա�����϶����
        obj.layer = road.layer;
        obj.GetComponent<RoadBase>().Road = road;
    }

    void CreateRYBpoleObj(RYBpole road)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>($"Prefab/{road.RoadType.ToString()}"));
        obj.transform.position = road.RoadPos;
        obj.transform.localEulerAngles = road.RoadRotation;
        obj.transform.localScale = road.RoadScale;
        obj.name = road.RoadName;
        //�ı���������LayerΪDrag���Ա�����϶����
        obj.layer = road.layer;
        obj.GetComponent<RYBpoleBase>().RYBpole = road;
    }
}
