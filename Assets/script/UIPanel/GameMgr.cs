using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance;
    public RoadPanel RoadPanel;
    public SavePanel SavePanel;
    public RYBpolePanel RYBpolePanel;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        RoadPanel = transform.Find("Canvas/RoadPanel").GetComponent<RoadPanel>();
        SavePanel = transform.Find("Canvas/SavePanel").GetComponent<SavePanel>();
        RYBpolePanel = transform.Find("Canvas/RYBpolePanel").GetComponent<RYBpolePanel>();
        CloseAllPanel();
    }

    public void ShowRoadPanel(bool show,GameObject obj=null)
    {
        RYBpolePanel.gameObject.SetActive(false);
        RoadPanel.gameObject.SetActive(show);
        Time.timeScale = show? 0f : 1f;
        if (show)
            RoadPanel.OpenUI(obj);
        else
        {
            GameManager.instance.mainCanvas.alpha = 1;
            GameManager.instance.mainCanvas.blocksRaycasts = true;
            GameManager.instance.mainCanvas.interactable = true;
        }
    }

    public void ShowRYBpolePanel(bool show, GameObject obj = null)
    {
        RoadPanel.gameObject.SetActive(false);
        RYBpolePanel.gameObject.SetActive(show);
        Time.timeScale = show ? 0f : 1f;
        if (show)
            RYBpolePanel.OpenUI(obj);
        else
        {
            GameManager.instance.mainCanvas.alpha = 1;
            GameManager.instance.mainCanvas.blocksRaycasts = true;
            GameManager.instance.mainCanvas.interactable = true;
        }
    }

    public void ShowSavePanel(bool show)
    {
        SavePanel.gameObject.SetActive(show);
        SavePanel.OpenUI(show,true);
    }

    public void CloseAllPanel()
    {
        RYBpolePanel.gameObject.SetActive(false);
        SavePanel.gameObject.SetActive(false);
        RoadPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
