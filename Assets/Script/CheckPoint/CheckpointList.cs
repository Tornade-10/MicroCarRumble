using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointList : MonoBehaviour
{

    private List<CheckPoint> _checkPoints;
    private int _nextCheckpointIndex = 0;
    
    private void Awake()
    {
        _checkPoints = GetComponentsInChildren<CheckPoint>().ToList();

        foreach (CheckPoint checkPointTransform in _checkPoints)
        {
            CheckPoint checkPoint = checkPointTransform.GetComponent<CheckPoint>();
            checkPoint.SetCheckpointList(this);
        }
    }

    private void Update()
    {
        if (_nextCheckpointIndex == _checkPoints.Count - 1)
        {
            Debug.Log("+ 1 Turn");
        }
    }

    public void playerTroughCheckpoint(CheckPoint checkPoint)
    {
        if ((_checkPoints.IndexOf(checkPoint)) == _nextCheckpointIndex)
        {
            _nextCheckpointIndex = (_nextCheckpointIndex + 1) % _checkPoints.Count;
            Debug.Log("Correct Checkpoint");
        }
        else
        {
            Debug.Log("Wrong Checkpoint");
        }
    }
}
