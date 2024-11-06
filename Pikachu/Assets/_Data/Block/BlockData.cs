using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BlockData : BlockAbstract
{
    [Header("BlockData")]

    // Thuộc tính lưu thông tin của node mà block này đang thuộc về
    public Node node;


 


    // Hàm thiết lập node cho block
    public virtual void SetNode(Node node)
    {
        this.node = node;
    }

    //Hàm lấy hình ảnh
    public virtual void SetSprite(Sprite sprite)
    {
        //this.sprite = sprite;
        this.ctrl.sprite.sprite = sprite;
    }
}
