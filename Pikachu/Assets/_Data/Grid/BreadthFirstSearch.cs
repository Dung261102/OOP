using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BreadthFirstSearch : GridAbstract, IPathfinding
{
    [Header("Breadth First Search")]

    public Queue<BlockCtrl> queue = new Queue<BlockCtrl>();
    public Dictionary<BlockCtrl, BlockCtrl> cameForm = new Dictionary<BlockCtrl, BlockCtrl>();

    public virtual void FindPath(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        Debug.Log("FindPath");
        this.queue.Enqueue(startBlock);
        this.cameForm[startBlock] = startBlock;

        while (this.queue.Count > 0)
        {
            BlockCtrl current = this.queue.Dequeue();  
       

            if (current == targetBlock)
            {
                break;
            }

            foreach (BlockCtrl neighbor in current.neighbors)
            {
                 if (this.IsValidPosition(neighbor) && !cameForm.ContainsKey(neighbor))
                {
                    this.queue.Enqueue(neighbor);
                    this.cameForm[neighbor] = current;
                }
                
            }
        }
        this.ShowCameForm();
    }

    protected virtual void ShowCameForm ()
    {
        foreach (var pair in cameForm)
        {
            BlockCtrl key = pair.Key;
            BlockCtrl value = pair.Value;

            Debug.Log("Left: " + key.ToString() + ", Right: " + value.ToString());
        }
    }
    private bool IsValidPosition(BlockCtrl block)
    {
        return true;
    }

}
