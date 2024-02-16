using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private CheckpointList _checkpointList;
    
    //give a color on the gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

    //to be added, check point systeme
    private void OnTriggerEnter(Collider other)
    {
        _checkpointList.playerTroughCheckpoint(this);
    }

    public void SetCheckpointList(CheckpointList checkpointList)
    {
        this._checkpointList = checkpointList;
    }
}
