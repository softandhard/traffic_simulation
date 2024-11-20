using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown1 : MonoBehaviour
{
    private Dropdown drop1;
    public GameObject obj;
    //·½Ïò
    public GameObject obj1;
    public GameObject obj2;

    // Start is called before the first frame update
    void Start()
    {
        drop1 = this.GetComponent<Dropdown>();
        drop1.onValueChanged.AddListener(change);
        ChangeWeather();
    }

    public void ChangeWeather()
    {
        change(SaveMgr.Instance.SaveData.Weather);
        drop1.value = SaveMgr.Instance.SaveData.Weather;
    }


    private void change(int index)
    {
        print(index);
        switch (index)
        {
            case 0:
                do0();
                break;
            case 1:
                do1();
                break;
            case 2:
                do2();
                break;
            default:
                break;
        }
        SaveMgr.Instance.SaveData.Weather = index;
    }
    private void do0()
    {
        obj1.SetActive(false);
        obj2.SetActive(false);
    }
    private void do1()
    {
        obj1.SetActive(true);
        obj2.SetActive(false);
    }
    void do2()
    {
        obj2.SetActive(true);
        obj1.SetActive(false);
    }
}
