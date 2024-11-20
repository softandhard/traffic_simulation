using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y2 : MonoBehaviour
{
    void Start()
    {
        Material material = new Material(Shader.Find("Transparent/Diffuse"));
        material.color = Color.black;
        GetComponent<Renderer>().material = material;//开始时将灯设置为黑色
        InvokeRepeating("YellowTurn", 120f, 123f);//
        InvokeRepeating("TurnBlack", 123f, 123f);//
    }

    void Update()
    {

    }
    void YellowTurn()
    {
        Material material1 = new Material(Shader.Find("Transparent/Diffuse"));
        material1.color = Color.yellow;
        GetComponent<Renderer>().material = material1;
    }
    void TurnBlack()
    {
        Material material2 = new Material(Shader.Find("Transparent/Diffuse"));
        material2.color = Color.black;
        GetComponent<Renderer>().material = material2;
    }
}
