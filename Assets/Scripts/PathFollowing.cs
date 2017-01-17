using UnityEngine;
using System.Collections;

public class PathFollowing : MonoBehaviour {
    
    public Path path = new Path();
    
    public float speed;
    private float curSpeed;
    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPoint;
    Vector3 velocity;
    
    private Transform startPos, endPos;
    public Node StartNode { get; set; }
    public Node goalNode { get; set; }
    GameObject Attacker, Target;
    public ArrayList pathArray;
    private float elapsed = 0.0f;
	void Start () {
        Attacker = GameObject.FindGameObjectWithTag("Attacker");
        Target = GameObject.FindGameObjectWithTag("Target");
        pathArray = new ArrayList();
        
        FindPath();
        
        path.SetPoints(pathArray);
        pathLength = path.Length;
        curPathIndex = 0;
        velocity = transform.forward;
	}
    private void FindPath()
    {
        startPos = Attacker.transform;
        endPos = Target.transform;
        
        StartNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos.position)));
        goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos.position)));
        pathArray = AStar.FindPath(StartNode, goalNode);
    }
	void Update () {
        
        elapsed += Time.deltaTime;
        if (elapsed >= 1.0)
        {
            elapsed = 0.0f;
            FindPath();
            
            curPathIndex = 0;
            pathLength = pathArray.Count;
            path.SetPoints(pathArray);
        }
        curSpeed = speed * Time.deltaTime;
        targetPoint = path.GetPoint(curPathIndex);
        if (Vector3.Distance(Attacker.transform.position, targetPoint) < path.Radius)
        {
            if (curPathIndex < pathLength - 1)
                curPathIndex++;
            else
                return;
        }
        if (curPathIndex >= pathLength - 1)
            velocity += Steer(targetPoint, true);
        else
            velocity += Steer(targetPoint);
        Attacker.transform.position += velocity;
        
	}

    public Vector3 Steer(Vector3 target, bool finalPoint = false)
    {
        Vector3 currentVelocity = (target - Attacker.transform.position);
        float distance = currentVelocity.magnitude;
        currentVelocity.Normalize();
        if (finalPoint && distance < 20.0f)
            currentVelocity *= (curSpeed * (distance / 20.0f));
        else
            currentVelocity *= curSpeed;
        Vector3 steeringForce = currentVelocity - velocity;
        return steeringForce;
    }
}
