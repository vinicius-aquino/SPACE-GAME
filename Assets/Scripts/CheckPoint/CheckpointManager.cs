using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointkey = 0;
    public List<CheckPointBase> checkPoints;

    public bool HasCheckpoint()
    {
        return lastCheckpointkey > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if(i > lastCheckpointkey)
        {
            lastCheckpointkey = i;
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkPoints.Find(i => i.key == lastCheckpointkey);
        return checkpoint.transform.position;
    }
}
