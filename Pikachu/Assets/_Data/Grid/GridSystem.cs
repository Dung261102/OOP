using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridSystem : GridAbstract
{
    [Header("Grid System")]
    //Kích thước của lưới
    public int width = 18;
    public int height = 11;
    public float offsetX = 0.2f; //Khoảng cách giữa các node
    public List<Node> nodes;

    protected override void Start()
    {
        this.SpawnBlocks();
    }

    protected override void LoadComponents() //reset hiện được node lên
    {
        base.LoadComponents();
        this.InitGridSystem();
        this.SpawnBlocks(); 
    }

    //Hàm hiện node lên
    protected virtual void InitGridSystem()
    {

        if (this.nodes.Count > 0) return;

       
        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                Node node = new()
                {
                    x = x,
                    y = y,
                    posX = x - (this.offsetX * x),

                };

                this.nodes.Add(node);
                
            }
        }
    }

    //Hàm SpawnBlocks()
    protected virtual void SpawnBlocks()
    {
        
        Vector3 pos = Vector3.zero;
        
        foreach (Node node in this.nodes)
        {
            if (node.x == 0) continue; //Bỏ đi hàng đầu tiên
            if (node.y == 0) continue; 
            if (node.x == this.width-1) continue;
            if (node.y == this.height-1) continue; //Bỏ đi hàng đaauf tiên

            pos.x = node.posX; //khoảng cách giữa 2 node theo chiều ngang
            pos.y = node.y;


            Transform block = this.ctrl.blockSpawner.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);
            block.gameObject.SetActive(true);

        }
        
    }

}
