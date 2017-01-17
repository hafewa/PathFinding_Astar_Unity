using UnityEngine;
using System.Collections;
using System;
public class Node : IComparable
{
    
    public float nodeTotalCost;
    
    public float estimatedCost;
    
    public bool isObstacle;
    
    public Node parentNode;
    
    public Vector3 nodePos;

    
    public Node()
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.isObstacle = false;
        this.parentNode = null;
    }
    public Node(Vector3 nodePos)
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.isObstacle = false;
        this.parentNode = null;
        this.nodePos = nodePos;
    }
    public void markObstacle()
    {
        this.isObstacle = true;
    }
    public int CompareTo(object obj)
    {
        Node node = (Node)obj;
        
        if (this.estimatedCost < node.estimatedCost)
        {
            return -1;
        }

        if (this.estimatedCost > node.estimatedCost)
        {
            return 1;
        }
        return 0;
    }
}
