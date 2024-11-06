﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class NodeCameForm
{
    public string nodeId;
    public NodeDirections direction = NodeDirections.noDirection;


    public Node fromNode;

    public Node toNode;

    public NodeCameForm(Node fromNode, Node toNode)
    {
        this.nodeId = fromNode.x + "x" + fromNode.y;

        this.toNode = fromNode;
        this.fromNode = toNode;
        this.direction = this.GetDirection( fromNode, toNode);

    }

    protected virtual NodeDirections GetDirection(Node fromNode, Node toNode)
    {
        if (fromNode.x == toNode.x && fromNode.y < toNode.y) return NodeDirections.up;
        if (fromNode.x == toNode.x && fromNode.y > toNode.y) return NodeDirections.down;

        if (fromNode.x < toNode.x && fromNode.y == toNode.y) return NodeDirections.right;
        if (fromNode.x > toNode.x && fromNode.y == toNode.y) return NodeDirections.left;

        return NodeDirections.noDirection;

    }
}
