using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CheckpointList : MonoBehaviour
{
    public List<CheckPoint> checkpoints;
    
    private void Awake()
    {
        checkpoints = GetComponentsInChildren<CheckPoint>().ToList();
    }
}
