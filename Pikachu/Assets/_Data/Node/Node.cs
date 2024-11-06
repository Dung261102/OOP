using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] //hiện node lên
public class Node
{
    public int x = 0;
    public int y = 0;
    public float posX = 0;
    public int weight = 1;
    public bool occupied = false;
    public int nodeId;
    public Node up;
    public Node right;
    public Node down;
    public Node left;
    public NodeObj nodeObj;
    public BlockCtrl blockCtrl;

    public virtual List<Node> Neighbors()
    {
        List<Node> nodes = new List<Node>
        {
            this.up,
            this.right,
            this.down,
            this.left
        };
        return nodes;
    }


}

