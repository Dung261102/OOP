using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridAbstract : SaiMonoBehaviour
{
    [Header("Grid Abstract")]
    public GridManagerCtrl ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        //chatgpt
        if (this.ctrl != null) return;

        // Kiểm tra parent có tồn tại không
        if (transform.parent == null)
        {
            Debug.LogWarning(transform.name + " has no parent. Cannot load GridManagerCtrl.", gameObject);
            return;
        }

        // Kiểm tra parent có GridManagerCtrl hay không
        this.ctrl = transform.parent.GetComponent<GridManagerCtrl>();
        if (this.ctrl == null)
        {
            Debug.LogWarning(transform.name + " parent does not have GridManagerCtrl component.", gameObject);
        }
        else
        {
            Debug.Log(transform.name + " LoadCtrl successful.", gameObject);
        }
        //end
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<GridManagerCtrl>();
        Debug.LogWarning(transform.name + " LoadCtrl", gameObject);
    }

}
