using UnityEngine;
using System.Collections;
public class GridManager : MonoBehaviour
{
    private static GridManager myInstance = null;  
    
    public static GridManager instance
    {
        get 
        {
            if (myInstance == null)
            {
                myInstance = (GridManager)FindObjectOfType(typeof(GridManager)) ;
                if (myInstance == null)
                    Debug.Log("Can't find GridMan");
            }
            return myInstance;
        }
    }
    
    public int rows;
    public int cols;
    
    public float gridSize;
    
    public bool showGrid=true;
    
    private GameObject[] obstacleList;
    
    private Node[,] nodes;
    
    private Vector3 origin = new Vector3();
    public Vector3 Origin
    {
        get{return origin;}
    }
    void Awake()
    {
       origin = this.transform.position;
        
       obstacleList= GameObject.FindGameObjectsWithTag("Obstacle");
       
       CalculateObstacles();
    }
    private void CalculateObstacles()
    {
        nodes = new Node[cols, rows];
        int index = 0;
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                
                Node node = new Node(GetGridCellCenter(index));
                nodes[i, j] = node;
                index++;
            }
        }

        
        if (obstacleList != null && obstacleList.Length > 0)
        {
            
            foreach(GameObject obstacle in obstacleList)
            {
                int indexCell = GetGridIndex(obstacle.transform.position);
                int col = GetColumn(indexCell);
                int row = GetRow(indexCell);
                
                nodes[row, col].markObstacle();
            }
        }

    }
    public int GetGridIndex(Vector3 pos)
    {
        
        int col = (int)(pos.x / gridSize);
        
        int row = (int)(pos.z / gridSize);
        
        return (row * cols + col);
    }
    public Vector3 GetGridCellCenter(int index)
    {
        
        Vector3 cellPosition = GetGridCellPosition(index);
        cellPosition.x += (gridSize / 2.0f);
        cellPosition.z += (gridSize / 2.0f);
        
        return cellPosition;
    }
    public Vector3 GetGridCellPosition(int index)
    {
        int row = GetRow(index);
        int col = GetColumn(index);
        
        float xPosInGrid = col * gridSize;
        float zPosInGrid = row * gridSize;

        return Origin + new Vector3(xPosInGrid, 0, zPosInGrid);
    }
    public int GetRow(int index)
    {
        int row = index / cols;
        return row;
    }
    public int GetColumn(int index)
    {
        int column = index % cols;
        return column;
    }
    
    void OnDrawGizmos()
    {
        if (showGrid)
        {
            
            DrawGrid(transform.position, rows, cols, gridSize);
        }
        
    }
    private void DrawGrid(Vector3 origin, int rows, int cols, float cellSize)
    {
        float width = cols * cellSize;
        float height = rows * cellSize;
        
        for (int i = 0; i <=rows; i++)
        {
            
            Vector3 startPos = i * cellSize * new Vector3(0.0f, 0.0f, 1.0f);
            
            Vector3 endPos = startPos + width* new Vector3(1.0f, 0.0f, 0.0f);
            Debug.DrawLine(startPos, endPos, Color.gray);
        }
        
        for (int i = 0; i <=cols; i++)
        {
            
            Vector3 startPos = i * cellSize * new Vector3(1.0f, 0.0f, 0.0f);
            
            Vector3 endPos = startPos + height * new Vector3(0.0f, 0.0f, 1.0f);
            Debug.DrawLine(startPos, endPos, Color.black);
        }
    }
    public void GetNeighNode(Node node, ArrayList neighbors)
    {
        Vector3 neighborPos = node.nodePos;
        
        int neighborIndex = GetGridIndex(neighborPos);
        
        int row = GetRow(neighborIndex);
        int column = GetColumn(neighborIndex);
        
        int leftNodeRow = row - 1;
        int leftNodeColumn = column;
        AssignNeighNode(leftNodeRow, leftNodeColumn, neighbors);
        
        leftNodeRow = row +1 ;
        leftNodeColumn = column;
        AssignNeighNode(leftNodeRow, leftNodeColumn, neighbors);
        
        leftNodeRow = row ;
        leftNodeColumn = column+1;
        AssignNeighNode(leftNodeRow, leftNodeColumn, neighbors);
        
        leftNodeRow = row ;
        leftNodeColumn = column-1;
        AssignNeighNode(leftNodeRow, leftNodeColumn, neighbors);
        
    }
    private void AssignNeighNode(int row, int col, ArrayList neighbors)
    {
        if (row != -1 && col != -1 && row < rows && col < cols)
        {
            Node nodeToAdd = nodes[row, col];
            
            if (!nodeToAdd.isObstacle)
            {
                neighbors.Add(nodeToAdd);
            }
        }
    }


}
