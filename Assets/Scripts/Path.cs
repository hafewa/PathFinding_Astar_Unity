using UnityEngine;
using System.Collections;

public class Path {
    
    public Vector3[] pointA = new Vector3[0];
    
    public float Radius = 1.0f;
    public float Length
    {
        get
        {
            return pointA.Length;
        }
    }
    public Vector3 GetPoint(int index)
    {
        return pointA[index];
    }
    public void SetPoints(ArrayList path)
    {
        int index = 0;
        pointA = new Vector3[path.Count];
        foreach (Node node in path)
        {
            
            pointA[index] = node.nodePos;
            index++;
        }
    }
}
