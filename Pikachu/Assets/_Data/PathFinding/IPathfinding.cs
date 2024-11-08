using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfinding
{
    public abstract bool FindPath (BlockCtrl startBlock, BlockCtrl targetBlock);

    public abstract void DataReset();

}
