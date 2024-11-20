using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class RYBpolePanel : MonoBehaviour
{
    public InputField InputField1, InputField2,InputField3;
    public Button DeleteBtn, LightBtn, KeyBtn, CarBtn, ShowBtn, BackBtn,BackdeleteBtn, SuredelteBtn;
    private RYBpole RYBpole;
    private GameObject RoadObj;

    public GameObject light1, parameter, car, canvas1;

    public List<InputField> Light1List, Light2List, Light3List;
    public List<InputField> Parameter1List, Parameter2List, Parameter3List, Parameter4List, Parameter5List, Parameter6List;
    public List<InputField> Car1List, Car2List, Car3List, Car4List, Car5List;
    //public List<InputField> AllInputFieldList;
    public Button button1, button2, button3, button4;

    private void Awake()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        DeleteBtn.onClick.AddListener(() => {Delete(); });
        BackBtn.onClick.AddListener(() => { GameMgr.Instance.ShowRYBpolePanel(false); });
        LightBtn.onClick.AddListener(() => { OpenLight(); });
        KeyBtn.onClick.AddListener(() => { OpenKeyBtn(); });
        CarBtn.onClick.AddListener(() => { OpenCarBtn(); });
        ShowBtn.onClick.AddListener(() => { OpenShowBtn(); });
        BackdeleteBtn.onClick.AddListener(() => {BackDelete(); }) ;
        SuredelteBtn.onClick.AddListener(() => {SureDelete(); }); 
        button1.onClick.AddListener(() => { SaveLight(); });
        button2.onClick.AddListener(() => { Saveparameter(); });
        button3.onClick.AddListener(() => { SaveCar(); });
    }

    public void OpenUI(GameObject obj)
    {
        Debug.Log(obj.name);
        RYBpole = obj.GetComponent<RYBpoleBase>().RYBpole;
        RoadObj = obj;
        CloseObj();
    }
    /// <summary>
    /// 打开灯设置页面
    /// </summary>
    public void OpenLight()
    {
        CloseObj();
        light1.SetActive(true);
        Light1List[0].text = RYBpole.LightCycle.ToString();
        Light1List[1].text = RYBpole.LightPos.x.ToString();
        Light1List[2].text = RYBpole.LightPos.y.ToString();

        for (int i = 0; i < 4; i++)
        {
            Light2List[i].text = RYBpole.LightStraight_gt[i].ToString();
            Light3List[i].text = RYBpole.LightLeft_gt[i].ToString();
        }
    }

    /// <summary>
    /// 打开参数设置页面
    /// </summary>
    public void OpenKeyBtn()
    {
        CloseObj();
        parameter.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            Parameter1List[i].text = RYBpole.i[i].ToString();
            Parameter2List[i].text = RYBpole.dt[i].ToString();
            Parameter3List[i].text = RYBpole.sc[i].ToString();
            Parameter4List[i].text = RYBpole.li[i].ToString();
            Parameter5List[i].text = RYBpole.ldt[i].ToString();
            Parameter6List[i].text = RYBpole.lsc[i].ToString();
        }

    }

    /// <summary>
    /// 打开车设置页面
    /// </summary>
    public void OpenCarBtn()
    {
        CloseObj();
        car.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            Car1List[i].text = RYBpole.life[i].ToString();
            Car2List[i].text = RYBpole.speed[i].ToString();
            Car3List[i].text = RYBpole.minT[i].ToString();
            Car4List[i].text = RYBpole.maxT[i].ToString();
            Car5List[i].text = RYBpole.cTt[i].ToString();
        }
    }
    /// <summary>
    /// 删除
    /// </summary>
    public void Delete()
    {
        CloseObj();
        canvas1.SetActive(true);
    }

    ///<summary>
    ///确认删除
    ///</summary>
    public void SureDelete()
    {
        SaveMgr.Instance.RemoveRYBpole(RoadObj.name); Destroy(RoadObj); GameMgr.Instance.ShowRYBpolePanel(false);
    }

    ///<summary>
    ///不删除
    ///</summary>
    public void BackDelete()
    {
        canvas1.SetActive(false);
    }

    /// 打开全部参数设置页面
    /// </summary>
    public void OpenShowBtn()
    {
        OpenLight();
        OpenKeyBtn();
        OpenCarBtn();
        car.SetActive(true);
        parameter.SetActive(true);
        light1.SetActive(true);
    }

    /// <summary>
    /// 保存灯参数
    /// </summary>
    private void SaveLight()
    {
        RYBpole.LightCycle = int.Parse(Light1List[0].text);
        RYBpole.LightPos.x= int.Parse(Light1List[1].text);
        RYBpole.LightPos.y = int.Parse(Light1List[2].text);

        RYBpole.RoadPos = new Vector3(RYBpole.LightPos.x, 0, RYBpole.LightPos.y);
        RoadObj.transform.position = RYBpole.RoadPos;



        for (int i = 0; i < 4; i++)
        {
            RYBpole.LightStraight_gt[i] = int.Parse(Light2List[i].text);
            RYBpole.LightLeft_gt[i] = int.Parse(Light3List[i].text);
        }
        CloseObj();
    }

    /// <summary>
    /// 保存参数
    /// </summary>
    private void Saveparameter()
    {
        for (int i = 0; i < 4; i++)
        {
            RYBpole.i[i] = int.Parse(Parameter1List[i].text);
            RYBpole.dt[i] = int.Parse(Parameter2List[i].text);
            RYBpole.sc[i] = int.Parse(Parameter3List[i].text);
            RYBpole.li[i] = int.Parse(Parameter4List[i].text);
            RYBpole.ldt[i] = int.Parse(Parameter5List[i].text);
            RYBpole.lsc[i] = int.Parse(Parameter6List[i].text);
        }
        CloseObj();
    }

    /// <summary>
    /// 保存车参数
    /// </summary>
    private void SaveCar()
    {
        for (int i = 0; i < 4; i++)
        {
            RYBpole.life[i] = int.Parse(Car1List[i].text);
            RYBpole.speed[i] = int.Parse(Car2List[i].text);
            RYBpole.minT[i] = int.Parse(Car3List[i].text);
            RYBpole.maxT[i] = int.Parse(Car4List[i].text);
            RYBpole.cTt[i] = int.Parse(Car5List[i].text);
        }
        CloseObj();
    }
    private void CloseObj()
    {
        light1.SetActive(false);
        parameter.SetActive(false);
        car.SetActive(false);
        canvas1.SetActive(false);
    }
}
