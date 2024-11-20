using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartCoroutine : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Timer(1));
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Timer(float onesecond)
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(onesecond);
            count++;
            gameObject.GetComponent<Text>().text = count.ToString();
        }

    }
}
