using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager0 : MonoBehaviour
{  
    //标题
    public GameObject title;
    //panel
    public GameObject panel;
    //说明文档
    public GameObject doc;
    // Start is called before the first frame update

    public void Startbuttom()
    {
        //SaveMgr.Instance.NewGame();
        GameMgr.Instance.ShowSavePanel(true);
    }
    //说明文档
    public void DocButton()
    {
        title.SetActive(false);
        panel.SetActive(false);
        doc.SetActive(true);
    }
    public void ExitdocButton()
    {
        title.SetActive(true);
        panel.SetActive(true);
        doc.SetActive(false);
    }

    public void Example1()
    {
        SceneManager.LoadScene(2);
    }
    public void About()
    {
        Application.OpenURL("http://www.baidu.com");
    }



    public void Exitbutton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    } 
}
