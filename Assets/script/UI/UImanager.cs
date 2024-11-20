
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject lighttime;
    public GameObject keyparameter;
    public GameObject car;
    //private bool b;
    public Button btn1, btn2, btn3,btn4;
    private void Start()
    {
        btn1.onClick.AddListener(() => { lighttimebutton(); });
        btn2.onClick.AddListener(() => { keyparameterbutton(); });
        btn3.onClick.AddListener(() => { carbutton(); });
        btn4.onClick.AddListener(() => { exitmenubutton(); });
    }

    public void lighttimebutton()
    {
        if (lighttime.activeInHierarchy)
        {
            lighttime.SetActive(false);
        }
        else
        {
            lighttime.SetActive(true);
        }
    }

    //turn left button
    public void keyparameterbutton()
    {
        if (keyparameter.activeInHierarchy)
        {
            keyparameter.SetActive(false);
        }
        else
        {
            keyparameter.SetActive(true);
        }
    }

    public void carbutton()
    {
        if (car.activeInHierarchy)
        {
            car.SetActive(false);
        }
        else
        {
            car.SetActive(true);
        }
    }

    //public void showallbutton()
    //{
    //    if (b != true)
    //    {
    //        b = true;
    //    }
    //    else
    //    {
    //        b = false;
    //    }
    //    lighttime.SetActive(b);
    //    keyparameter.SetActive(b);
    //    car.SetActive(b);
    //}
    //ÍË³ö
    public void exitmenubutton()
    {
        CanvasGroup canvas1 = GetComponent<CanvasGroup>();
        canvas1.alpha = 0;
        canvas1.interactable = false;
        canvas1.blocksRaycasts = false;

    }
}
