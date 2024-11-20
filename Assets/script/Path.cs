using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    
    public List<LineRenderer> lineRenderers = new List<LineRenderer>();
    //private void Awake()
    //{
    //    GetComponentsInChildren(lineRenderers);
    //}
    private void Update()
    {
        GetComponentsInChildren(lineRenderers);
    }
    public List<Vector3> GetPathPoint() {

        List<Vector3> totalpath = new List<Vector3>();
        for (int i = 0; i < lineRenderers.Count ; i++)
        {
            Vector3[] path = new Vector3[lineRenderers[i].positionCount];

            lineRenderers[i].GetPositions(path);
            totalpath.AddRange(path);
        }
       
        return totalpath;
    }

    internal static string GetDirectoryName(string filePath)
    {
        throw new NotImplementedException();
    }
}
