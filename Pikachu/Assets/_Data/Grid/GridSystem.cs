using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GridSystem : GridAbstract
{
    [Header("Grid System")]
    //Kích thước của lưới
    public int width = 18;
    public int height = 11;
    public float offsetX = 0.2f; //Khoảng cách giữa các node
    public BlocksProfile blocksProfile;
    public List<Node> nodes;
    public List<BlockCtrl> blocks;
    public List<int> nodeIds;


    protected override void Start()
    {
        this.SpawnBlocks();
    }

    protected override void LoadComponents() //reset hiện được node lên
    {
        base.LoadComponents();
        this.InitGridSystem();
        this.LoadBlockProflie();
    }

    protected virtual void LoadBlockProflie()
    {
        if (this.blocksProfile != null) return;
        this.blocksProfile = Resources.Load<BlocksProfile>("Pokemons");
        Debug.LogWarning(transform.name + " LoadBlockProflie", gameObject);
    }

    //Hàm hiện node lên
    protected virtual void InitGridSystem()
    {

        if (this.nodes.Count > 0) return;

        int nodeId = 0;
        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                Node node = new()
                {
                    x = x,
                    y = y,
                    posX = x - (this.offsetX * x),
                    nodeId = nodeId,

                };

                this.nodes.Add(node);
                this.nodeIds.Add(nodeId);
                nodeId++;
            }
        }
    }

    //done
    protected virtual void SpawnNodeObj()
    {
        Vector3 pos = Vector3.zero;
        foreach (Node node in this.nodes)
        {
            if (node.x == 0) continue;
            if (node.y == 0) continue;
            if (node.x == this.width - 1) continue;
            if (node.y == this.height - 1) continue;

            pos.x = node.posX;
            pos.y = node.y;

            Transform obj = this.ctrl.blockSpawner.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);
            BlockCtrl blockCtrl = obj.GetComponent<BlockCtrl>();

            
            obj.gameObject.SetActive(true);
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

                Transform block = this.ctrl.blockSpawner.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);

                BlockCtrl blockCtrl = block.GetComponent<BlockCtrl>();
                
                //Lấy hình ảnh
                blockCtrl.blockData.SetSprite(sprite);

                this.LinkNodeBlock(node, blockCtrl); //kết nối 2 node lại với nhau
        
            
                block.gameObject.SetActive(true);
            }
        }
    }
    
    //done
    
    protected virtual Node GetRandomNode()
    {
       

        Node node;
        int randId;
        int nodeCount = this.nodes.Count;
        //Đi tìm đúng số lần mà node có trong danh sách
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
    protected virtual void LinkNodeBlock(Node node, BlockCtrl blockCtrl)
    {
        blockCtrl.blockData.SetNode(node);
        node.blockCtrl = blockCtrl;
    }

   
}
