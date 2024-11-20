using SR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public Button btn1;
    public Transform cam1;
    public ScrollCircle scrollT;  // 摇杆对象
    public ScrollCircle scrollR; // 摇杆对象

    public float MoveSpeed = 10f; // 移动速度

    //修正系数
    int moveModify;


    private float horizontal;
    private float vertical;

    private float rotateX;
    private float rotateY;

    public float RotateSensitivity = 50f;

    public float xRotation;
    public float yRotation;
    private void Start()
    {
        btn1.onClick.AddListener(() => Exittoscene0());
    }
    void Update()
    {

        float dis = Mathf.Abs(transform.position.z);
        // 摇杆输入T
        horizontal = scrollT.output.y;
        vertical = scrollT.output.x;

        if (dis > 0 && dis < 100)
        {
            moveModify = 1;
        }
        else
        {
            moveModify = Mathf.CeilToInt(dis / 100);
        }
        cam1.Translate(vertical * MoveSpeed * moveModify, 0, horizontal * MoveSpeed * moveModify);

        //R
        rotateX = scrollR.output.x * RotateSensitivity * 0.02f;
        rotateY = scrollR.output.y * RotateSensitivity * 0.02f;

        xRotation += rotateY;
        yRotation += rotateX;

        //xRotation = Mathf.Clamp(xRotation, -90f, 10f);
        //yRotation = Mathf.Clamp(yRotation, -90, 90);
        //RotationTarget.Rotate(Vector3.up * rotateX);
        //RotationTarget.Rotate(Vector3.up * rotateX);
        //transform.Rotate(rotateY, rotateX, 0);
        transform.localRotation = Quaternion.Euler(-xRotation * moveModify, yRotation * moveModify, 0);

    }
    public void Exittoscene0()
    {
        SceneManager.LoadScene(0);
    }
}
