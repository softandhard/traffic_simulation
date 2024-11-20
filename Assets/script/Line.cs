using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    private void Start()
    {
        InvokeRepeating(nameof(Drawpath1),0,1);
    }
    //private void Awake()
    //{
    //    List<Vector3> path = BezierUtility.BezierIntepolate4List(pointA.position, pointB.position, pointC.position, pointD.position, 40);

    //    lineRenderer.positionCount = path.Count;
    //    lineRenderer.SetPositions(path.ToArray());
    //}
    // Update is called once per frame

    public void Drawpath1()
    {
        List<Vector3> path = BezierUtility.BezierIntepolate4List(pointA.position, pointB.position, pointC.position, pointD.position, 40);

        lineRenderer.positionCount = path.Count;
        lineRenderer.SetPositions(path.ToArray());
    }
}
