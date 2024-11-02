using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridSystem : SaiMonoBehaviour
{

    //Kích thước của lưới
    public int width = 18;
    public int height = 11;
    public List<Node> nodes;

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
                
                pos.x = node.x;
                pos.y = node.y;
       

                Transform block = BlockSpawner.Instance.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);
                block.gameObject.SetActive(true);

        }
        
    }

}
