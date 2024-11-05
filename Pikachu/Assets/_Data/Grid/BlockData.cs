﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BlockData : BlockAbstract
{
    [Header("BlockData")]
    public TextMeshPro text;

    // Thuộc tính lưu thông tin của node mà block này đang thuộc về
    public Node node;

    // Thuộc tính lưu trữ hình ảnh (sprite) đại diện cho Block trong game
    public Sprite sprite;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadTextMeshPro();
    }

    protected virtual void LoadTextMeshPro()
    {
        if (this.text != null) return;
        this.text = transform.GetComponentInChildren<TextMeshPro>();
        Debug.LogWarning(transform.name + " LoadTextMeshPro", gameObject);
    }



    // Hàm thiết lập node cho block
    public virtual void SetNode(Node node)
    {
        this.node = node;
        //mã số 
        this.text.text = this.node.x.ToString() + "\n" +  this.node.y.ToString(); 
    }

    //Hàm lấy hình ảnh
    public virtual void SetSprite(Sprite sprite)
    {
        //this.sprite = sprite;
        this.ctrl.sprite.sprite = sprite;
    }
}
