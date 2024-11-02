using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : BlockAbstract
{
    [Header("BlockData")]
    // Thu?c tính l?u tr? hình ?nh (sprite) ??i di?n cho Block trong game.
    public Sprite sprite;
  
    public Node node;

    //Hàm setnot
    public virtual void SetNode(Node node)
    {
        this.node = node;
    }

    //Hàm lấy hình ảnh
    public virtual void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
        this.ctrl.sprite.sprite = sprite;
    }
}
