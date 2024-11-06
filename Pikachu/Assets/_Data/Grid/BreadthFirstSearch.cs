using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class BreadthFirstSearch : GridAbstract, IPathfinding
{
    [Header("Breadth First Search")]

    public List<Node> queue = new List<Node>();
    public List<Node> path = new List<Node>();
    public Dictionary<Node, Node> cameForm = new Dictionary<Node, Node>();


    //Hàm FnidPath - tìm đường đi 
    public virtual void FindPath(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        Debug.Log("FindPath");
        Node startNode = startBlock.blockData.node;
        Node targetNode = targetBlock.blockData.node;


        this.Enqueue(startNode);
        this.cameForm[startNode] = startNode;

        while (this.queue.Count > 0)
        {

            Node current = this.Dequeue();

            if (current == targetNode)
            {
                ConstructPath(startNode, targetNode);
                break;
            }

            foreach (Node neighbor in current.Neighbors())
            {
                if (neighbor == null) continue;


                if (this.IsValidPosition(neighbor, targetNode) && !cameForm.ContainsKey(neighbor))
                {
                    this.Enqueue(neighbor);
                    this.cameForm[neighbor] = current;
                }
                
            }
        }
        this.ShowCameFrom();
        this.ShowPath();
        
    }

    //Hàm ShowCameFrom
    protected virtual void ShowCameFrom()
    {
        //Node key = Pair().Key;
        Node key = this.cameForm.Keys.First(); //chatgpt

        Vector3 pos = key.nodeObj.transform.position;
        Transform keyObj = this.ctrl.blockSpawner.Spawn(BlockSpawner.SCAN, pos, Quaternion.identity);
        keyObj.gameObject.SetActive(true);

    }

    //ham ConstructPath - tìm đường đi ngắn nghất
    protected virtual void ConstructPath(Node startNode, Node targetNode)
    {
        Node currenCell = targetNode;
        while (currenCell != startNode)
        {
            path.Add(currenCell);
            currenCell = this.cameForm[currenCell];
        }

        path.Add(startNode);
        path.Reverse();
    }

    protected virtual void ShowPath()
    {
        Vector3 pos;
        foreach (Node node in this.path)
        {
            pos = node.nodeObj.transform.position;
            UnityEngine.Transform linker = this.ctrl.blockSpawner.Spawn(BlockSpawner.LINKER, pos, Quaternion.identity);
            linker.gameObject.SetActive(true);
        }
    }


    //Hàm enqueue - đưa 1 block vào trong queue
    protected virtual void Enqueue(Node blockCtrl)
    {
        this.queue.Add(blockCtrl);
    }

    //Hàm dequeue - lấy phần tử đầu tiên và cắt nó khỏi list
    protected virtual Node Dequeue()
    {
        Node node = this.queue[0];
        this.queue.RemoveAt(0);
        return node;
    }


    private bool IsValidPosition(Node node, Node startNode)
    {
        if (node == startNode) return true;

        return !node.occupied;
    }

}
