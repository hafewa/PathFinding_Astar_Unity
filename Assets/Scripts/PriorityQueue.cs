using UnityEngine;
using System.Collections;
public class PriorityQueue 
{
    
    private ArrayList nodes=new ArrayList();
    public int Length
    {
        get { return this.nodes.Count; }
    }
    public void Push(Node node)
    {
        this.nodes.Add(node);
        this.nodes.Sort();
    }
    public void Remove(Node node)
    {
        this.nodes.Remove(node);
        this.nodes.Sort();
    }
    public Node First()
    {
        if(this.nodes.Count>0)
        {
            return (Node)this.nodes[0];
        }
        return null;
    }
    public bool Contains(Node node)
    {
        return this.nodes.Contains(node);
    }
}
