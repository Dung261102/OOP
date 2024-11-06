using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class NodeCameForm
{
    public string nodeId;

    public Node node;
    public Node cameFromNode;

    public NodeCameForm(Node node, Node cameFromNode)
    {
        this.nodeId = node.x + "x" + node.y;

        this.node = node;
        this.cameFromNode = cameFromNode;
    }
}
