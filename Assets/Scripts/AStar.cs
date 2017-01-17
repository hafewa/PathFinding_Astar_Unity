using UnityEngine;
using System.Collections;

public class AStar  {
    public static PriorityQueue closedList, openlist;
    private static float NodeCost(Node a,Node b)
    {
        Vector3 vecCost = a.nodePos - b.nodePos;
        return vecCost.magnitude;
    }
    public static ArrayList FindPath(Node start, Node goal)
    {
        openlist = new PriorityQueue();
        openlist.Push(start);
        start.nodeTotalCost = 0.0f;
        start.estimatedCost = NodeCost(start, goal);
        closedList = new PriorityQueue();
        Node node = new Node();
        while (openlist.Length != 0)
        {
            node = openlist.First();
            
            if (node.nodePos == goal.nodePos)
            {
                return CalculatePath(node);
            }
            
            ArrayList neighbors = new ArrayList();
            GridManager.instance.GetNeighNode(node, neighbors);
            
            for (int i = 0; i < neighbors.Count; i++)
            {
                Node neighborNode=(Node)neighbors[i];
                
                if(!closedList.Contains(neighborNode))
                {
                    
                    float cost = NodeCost(node, neighborNode);
                    
                    float totalCost = node.nodeTotalCost + cost;
                    
                    float neighborNodeEstCost = NodeCost(neighborNode, goal);
                    neighborNode.nodeTotalCost = totalCost;
                    neighborNode.estimatedCost = neighborNodeEstCost;
                    neighborNode.estimatedCost += totalCost;
                    
                    neighborNode.parentNode = node;
                    
                    if (!openlist.Contains(neighborNode))
                    {
                        openlist.Push(neighborNode);
                    }
                }
            }
            closedList.Push(node);
            openlist.Remove(node);
        }
        
        if (node.nodePos != goal.nodePos)
        {
            Debug.Log("目标节点未找到");
            return null;
        }
        return CalculatePath(node);
    }
    private static ArrayList CalculatePath(Node node)
    {
        ArrayList list = new ArrayList();

        while (node != null)
        {
            list.Add(node);
            node = node.parentNode;
        }
        list.Reverse();
        return list;
    }
}
