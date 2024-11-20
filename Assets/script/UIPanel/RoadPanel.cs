using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class RoadPanel : MonoBehaviour
{
    public InputField InputField1, InputField2,InputField3;
    public Button Button1, Button2, Button3, Button4;
    private Road road;
    private GameObject RoadObj;

    private void Awake()
    {
        Button1.onClick.AddListener(() => { 
            road.RoadPos = new Vector3(int.Parse(InputField1.text), RoadObj.transform.position.y,int.Parse(InputField2.text)); 
            RoadObj.transform.position = road.RoadPos; int a = int.Parse(InputField3.text); if (a == 0) return; 
            RoadObj.transform.localScale= road.RoadScale = new Vector3(1, 1, a);
            RoadObj.transform.GetChild(0).GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(1, 3 * a));            //Í¼Æ¬À­Éì
            RoadObj.transform.localEulerAngles = road.RoadRotation;
        });
        Button2.onClick.AddListener(() => { SaveMgr.Instance.RemoveRoad(RoadObj.name); Destroy(RoadObj); GameMgr.Instance.ShowRoadPanel(false); });
        Button3.onClick.AddListener(() => { GameMgr.Instance.ShowRoadPanel(false); });
        Button4.onClick.AddListener(() => { road.RoadRotation += new Vector3(0, 90, 0); });
    }

    public void OpenUI(GameObject obj)
    {
        Debug.Log(obj.name);
        road = obj.GetComponent<RoadBase>().Road;
        RoadObj = obj;
        InputField1.text = road.RoadPos.x.ToString();
        InputField2.text = road.RoadPos.z.ToString();
        InputField3.text = road.RoadScale.x.ToString();
    }
}
