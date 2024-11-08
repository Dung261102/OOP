﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : GridAbstract
{
    [Header("Grid System")]
    //Kích thước của lưới
    public int width = 18;
    public int height = 11;
    public float offsetX = 0.2f; //Khoảng cách giữa các toNode
    public BlocksProfileSO blocksProfile;
    public List<Node> nodes;

    public List<BlockCtrl> blocks;
    public List<int> nodeIds;


   
    
    protected override void LoadComponents() //reset hiện được toNode lên
    {
        base.LoadComponents();
        this.InitGridSystem();
        this.LoadBlockProflie();
    }

    protected virtual void LoadBlockProflie()
    {
        if (this.blocksProfile != null) return;
        this.blocksProfile = Resources.Load<BlocksProfileSO>("Pokemons");
        Debug.LogWarning(transform.name + " LoadBlockProflie", gameObject);
    }

    protected override void Start()
    {
        this.SpawnNodeObj();
        this.SpawnBlocks();
        this.FindNodesNeighbors();
        this.FindBlocksNeighbors();
    }

    protected virtual void FindNodesNeighbors()
    {
        int x, y;
        foreach (Node node in this.nodes)
        {
            x = node.x;
            y = node.y;
            node.up = this.GetNodeByXY(x, y + 1);
            node.right = this.GetNodeByXY(x + 1, y);
            node.down = this.GetNodeByXY(x, y - 1);
            node.left = this.GetNodeByXY(x - 1, y);
        }
    }

    public virtual Node GetNodeByXY(int x, int y)
    {
        foreach (Node node in this.nodes)
        {
            if (node.x == x && node.y == y) return node;
        }

        return null;
    }


    protected virtual void FindBlocksNeighbors()
    {
        foreach (Node node in this.nodes)
        {
            if (node.blockCtrl == null) continue;
            node.blockCtrl.neighbors.Add(node.up.blockCtrl);
            node.blockCtrl.neighbors.Add(node.right.blockCtrl);
            node.blockCtrl.neighbors.Add(node.down.blockCtrl);
            node.blockCtrl.neighbors.Add(node.left.blockCtrl);
        }
    }


    //Hàm hiện toNode lên
    protected virtual void InitGridSystem()
    {
        if (this.nodes == null) this.nodes = new List<Node>();
        if (this.nodeIds == null) this.nodeIds = new List<int>();

        if (this.nodes.Count > 0) return; // Đảm bảo không khởi tạo lại nếu danh sách toNode đã có

        int nodeId = 0;
        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                Node node = new Node
                {
                    x = x,
                    y = y,
                    posX = x - (this.offsetX * x),
                    nodeId = nodeId,
                };

                this.nodes.Add(node);
                this.nodeIds.Add(nodeId); // Thêm nodeId vào danh sách nodeIds
                nodeId++;
            }
        }

        Debug.Log("Total nodes created: " + this.nodes.Count); // Thêm thông báo debug
        Debug.Log("Total toNode IDs available: " + this.nodeIds.Count); // Thêm thông báo debug
    }


    //done
    protected virtual void SpawnNodeObj()
    {
        Vector3 pos = Vector3.zero;
        foreach (Node node in this.nodes)
        {
            pos.x = node.posX;
            pos.y = node.y;

            Transform obj = this.ctrl.blockSpawner.Spawn(BlockSpawner.NODE_OBJ, pos, Quaternion.identity);
            obj.name = "Holder_" + node.x.ToString() + "_" + node.y.ToString();
            obj.gameObject.SetActive(true);

            NodeObj nodeObj = obj.GetComponent<NodeObj>();
            nodeObj.SetText(node.y + "\n" + node.x);
            Color color = node.y % 2 == 0 ? Color.red : Color.blue;
            nodeObj.SetColor(color);
            nodeObj.gameObject.SetActive(true);

            node.nodeObj = nodeObj;


        }
    }

    //Hàm SpawnBlocks()
    protected virtual void SpawnBlocks()
    {
        Vector3 pos = Vector3.zero;
        int blockCount = 4; //mỗi con xuất hiện 4 lần
        foreach (Sprite sprite in this.blocksProfile.sprites)
        {
            for (int i = 0; i < blockCount; i++)
            {
                Node node = this.GetRandomNode();
                pos.x = node.posX;
                pos.y = node.y;

                //lấy toạ độ của block
                Transform blockObj = this.ctrl.blockSpawner.Spawn(BlockSpawner.NODE_OBJ, pos, Quaternion.identity);
                NodeObj blockHolder = blockObj.GetComponent<NodeObj>();
                node.nodeObj = blockHolder;
                blockObj.name = "Holder_" + node.x.ToString() + "_" + node.y.ToString();


                blockHolder.gameObject.SetActive(true);


                Transform block = this.ctrl.blockSpawner.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);

                BlockCtrl blockCtrl = block.GetComponent<BlockCtrl>();
                
                //Lấy hình ảnh
                blockCtrl.blockData.SetSprite(sprite);

                this.LinkNodeBlock(blockCtrl, node); //kết nối 2 toNode lại với nhau
                block.name = "Block_" + node.x.ToString() + "_" + node.y.ToString();


                block.gameObject.SetActive(true);

                this.NodeOccupied(node);

            }
        }
    }

    public virtual void NodeOccupied(Node node)
    {
        node.occupied = true;
        node.blockPlaced = true;
    }

    public virtual void NodeFree(Node node)
    {
        //this.freeNodes.Add(node);
        node.occupied = false;
        node.blockCtrl.sprite.sprite = null;
        //this.ctrl.blockHandler.UnchooseBlock(node.blockCtrl);
    }
    protected virtual Node GetRandomNode()
    {
   
        Node node;
        int randId;
        int nodeCount = this.nodes.Count;
        //Đi tìm đúng số lần mà toNode có trong danh sách
        for (int i = 0; i < nodeCount; i++)
        {
            if (this.nodeIds.Count == 0) break;  // Kiểm tra nếu nodeIds trống

            randId = Random.Range(0, this.nodeIds.Count);
            node = this.nodes[this.nodeIds[randId]];


            this.nodeIds.RemoveAt(randId);

            if (node.x == 0) continue;
            if (node.y == 0) continue;
            if (node.x == this.width - 1) continue;
            if (node.y == this.height - 1) continue;

            if (node.blockCtrl == null) return node;
        }

        Debug.LogError("Node can't found, this should not happen");
        return null;
    }


    //done
    protected virtual void LinkNodeBlock( BlockCtrl blockCtrl, Node node)
    {
        blockCtrl.blockData.SetNode(node); //gắn vào hàm spamblock cũng dc
        node.blockCtrl = blockCtrl;
    }

   
}
