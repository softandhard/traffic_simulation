using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //主菜单
    public CanvasGroup mainCanvas;
    public GameObject mune1;
    public GameObject model1;

    //timespeed
    public Text timespeedtext1;
    //private float lastClickTime;
    //private int clickCount;

    private Road road;
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
        }
        controltime();
        //点击模型
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log(1);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject);
                GameObject obj = hit.collider.gameObject;
            
                if (obj.CompareTag("road"))
                {
                    GameMgr.Instance.ShowRoadPanel(true, obj);
                    mainCanvas.alpha = 0;
                    mainCanvas.blocksRaycasts = false;
                    mainCanvas.interactable = false;
                }
                else if (obj.CompareTag("RYBpole"))
                {
                    GameMgr.Instance.ShowRYBpolePanel(true, obj);
                    mainCanvas.alpha = 0;
                    mainCanvas.blocksRaycasts = false;
                    mainCanvas.interactable = false;
                }

                return;             
            }    
        }
    }
    public void selectgyr()
    {
        mune1.SetActive(false);
        model1.SetActive(true);
    }
    public void exitselectgyr()
    {
        mune1.SetActive(true);
        model1.SetActive(false);
    }
    public void savescene()
    {
        SaveMgr.Instance.SaveGame();
    }
    public void reloadScene()
    {
        int id = SaveMgr.Instance.SaveData.ID;
        SaveMgr.Instance.LoadGame();
        SaveMgr.Instance.SetSaveData(id);
        SceneManager.LoadScene(1);
    }
    public void exitScene1toScene0()
    {
        SceneManager.LoadScene(0);
        GameMgr.Instance.CloseAllPanel();
    }


    //timespeed
    public void timescale(float value)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Time.timeScale = Mathf.Round(value * 2); 
        timespeedtext1.text = "倍速：" + Mathf.Round(value * 2);
    }


    void controltime()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 2;

        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Time.timeScale = 10;
        }
    }
}
