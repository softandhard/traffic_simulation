using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    public Button BackBtn, LoadBtn,ReBtn;//SaveBtn;
    public Toggle toggle1, toggle2, toggle3;
    public Text Info;

    private void Awake()
    {
        BackBtn.onClick.AddListener(() => { gameObject.SetActive(false); });
        LoadBtn.onClick.AddListener(() => { LoadGame(); });
        //SaveBtn.onClick.AddListener(() => {  });
        ReBtn.onClick.AddListener(() => { ResetFile(); });
    }

    public void OpenUI(bool show,bool load)
    {
        if (!show) return;
        Info.text = "";
        toggle1.isOn= toggle2.isOn = toggle3.isOn  = false;

        ShowSaveInfo(toggle1,1);
        ShowSaveInfo(toggle2, 2);
        ShowSaveInfo(toggle3, 3);
    }
    /// <summary>
    /// 读取存档
    /// </summary>
    private void LoadGame()
    {
        if (toggle1.isOn)
        {
            LoadScene(0);
        }
        else if (toggle2.isOn)
        {
            LoadScene(1);
        }
        else if (toggle3.isOn)
        {
            LoadScene(2);
        }
        else
        {
            Info.text = "请选择一个档案！";
        }
    }
    /// <summary>
    /// 重置档案
    /// </summary>
    private void ResetFile()
    {

        if (toggle1.isOn)
        {
            SaveMgr.Instance.ResetSaveData(0);
        }
        else if (toggle2.isOn)
        {
            SaveMgr.Instance.ResetSaveData(1);
        }
        else if (toggle3.isOn)
        {
            SaveMgr.Instance.ResetSaveData(2);
        }
        else
        {
            Info.text = "请选择一个档案！";
            return;
        }
        Info.text = "重置成功！";
    }


    private void LoadScene(int index)
    {
        SaveMgr.Instance.SetSaveData(index);
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }


    private void ShowSaveInfo(Toggle toggle,int index)
    {
       Text text= toggle.transform.Find("Label").GetComponent<Text>();
        SaveData saveData = SaveMgr.Instance.saveList.SaveDataList[index - 1];

        DateTime startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);

        //System.DateTime startTime = System.TimeZoneInfo.ToLocalTime(new System.DateTime(1970, 1, 1));//获取时间戳
        DateTime dt = startTime.AddSeconds(saveData.SaveTime);
        string t = dt.ToString("yyyy/MM/dd HH:mm:ss");//转化为日期时间

        text.text = $"存档{index}    {t}";
    }
}
