using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariable : MonoBehaviour
{
    //public static GlobalVariable instnace;
    public int[] num1;
    public InputField[] input1;
    string[] str1=new string[60];

    //ֱ�к��
    public int x0r;
    public int x1r;
    public int z0r;
    public int z1r;

    //��ת���
    public int x0lr=3;
    public int x1lr;
    public int z0lr;
    public int z1lr;

    //private void Awake()
    //{
    //    instnace = this;
    //}
    private void Update()
    {
        for (int i = 0; i < input1.Length; i++)
        {
            //if (input1[i].text is null)
            //{
            //    num1[i] = 1;
            //    print("null");
            //}
            str1[i] = input1[i].text;
            if (string.IsNullOrEmpty(str1[i]))
            {
                num1[i] = 1;
                num1[0] = 3;
            }
            else
            {
                num1[i] = int.Parse(str1[i]);
                //��������Ϊ3
                if (num1[0] <= 3)
                {
                    num1[0] = 3;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                print(num1[0]);
            }          
        }
        CalculateNum();
    }
    public void CalculateNum()
    {
        //ֱ��
        x0r = Mathf.Abs(num1[0] - num1[1] - 3);
        //print("����"+num1[0]);
        //print("�̵�ʱ��"+num1[1]);
        //print("���"+x0r);
        x1r = Mathf.Abs(num1[0] - num1[2]- 3);
        z0r = Mathf.Abs(num1[0] - num1[3]- 3);
        z1r = Mathf.Abs(num1[0] - num1[4]- 3);
        //��ת
        x0lr = Mathf.Abs(num1[0] - num1[5] - 3);
        x1lr = Mathf.Abs(num1[0] - num1[6] - 3);
        z0lr = Mathf.Abs(num1[0] - num1[7] - 3);
        z1lr = Mathf.Abs(num1[0] - num1[8] - 3);
    }
}
