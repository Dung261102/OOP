using System.Collections.Generic;
using UnityEngine;



public class BreadthFirstSearch : GridAbstract, IPathfinding
{
    [Header("Breadth First Search")]

    public List<Node> queue = new List<Node>();
    public List<Node> path = new List<Node>();
    public List<NodeStep> cameFromNodes = new List<NodeStep>();
    public List<Node> visited = new List<Node>();


    //Hàm FnidPath - tìm đường đi 
    public virtual void FindPath(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        Debug.Log("FindPath");
        Node startNode = startBlock.blockData.node;
        Node targetNode = targetBlock.blockData.node;


        this.Enqueue(startNode);
        this.cameFromNodes.Add(new NodeStep(startNode, startNode));
        this.visited.Add(startNode);

        while (this.queue.Count > 0)
        {

            Node current = this.Dequeue();

            if (current == targetNode)
            {
                this.path=BuildNodeBath(startNode, targetNode);
                break;
            }

            foreach (Node neighbor in current.Neighbors())
            {
                if (neighbor == null) continue;
                if (!this.visited.Contains(neighbor)) continue;
                if (!this.IsValidPosition(neighbor, targetNode)) continue;

             
                
                    this.Enqueue(neighbor);
                    this.visited.Add(neighbor);

                    this.cameFromNodes.Add(new NodeStep(neighbor, current));

                

            }
        }
        this.ShowVisited();
        this.ShowPath();
        
    }

    //Hàm ShowCameFrom
    protected virtual void ShowVisited()
    {
        foreach (Node node in this.visited)
        {
            Vector3 pos = node.nodeObj.transform.position;
            Transform keyObj = this.ctrl.blockSpawner.Spawn(BlockSpawner.SCAN, pos, Quaternion.identity);
            keyObj.gameObject.SetActive(true);
        }
    }

    //ham BuildNodeBath - tìm đường đi từ điểm đầu tới điểm target
    protected virtual List<Node> BuildNodeBath(Node startNode, Node targetNode)
    {

        List<Node> path = new List<Node>();
        Node toNode = targetNode;
        while (toNode != startNode)
        {
            path.Add(toNode);
            
            toNode = this.GetFromNode(toNode);

        }

        path.Add(startNode);
        path.Reverse();

        return path;
    }


    //Hàm GetFromNode
    protected virtual Node GetFromNode (Node toNode)
    {
        return this.GetNodeStepByToNode( toNode).fromNode;
    }

    protected virtual NodeStep GetNodeStepByToNode(Node toNode)
    {
       return this.cameFromNodes.Find(item => item.toNode == toNode);
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
