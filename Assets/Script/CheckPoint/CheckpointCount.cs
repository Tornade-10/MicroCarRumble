using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointCount : MonoBehaviour
{
    private CheckpointList _checkpointList;
    private List<CheckPoint> _checkPoints;
    private int _nextCheckpointIndex;
    private int _numberOfTurns;

    public GameObject player;

    private void Start()
    {
        _checkpointList = FindObjectOfType<CheckpointList>();
        if (_checkpointList != null)
        {
            _checkPoints = _checkpointList.checkpoints;
            
            foreach (var checkpoint in _checkPoints)
            {
                CheckPoint checkPoint = checkpoint.GetComponent<CheckPoint>();
                
                checkPoint.SetCheckpoint(this);
            }
        }
        else
        {
            Debug.LogError("FirstScript not found in the scene.");
        }
    }

    public void Update()
    {
        if (_nextCheckpointIndex == _checkPoints.Count - 1)
        {
            _numberOfTurns++;
        }

        if (_numberOfTurns == 3)
        {
            SceneManager.LoadScene("VictoryScreen");
        }
    }

    public void PlayerGoThroughCheckpoint(CheckPoint checkPoint)
    {
        if (_checkPoints.IndexOf(checkPoint) == _nextCheckpointIndex)
        {
            Debug.Log("CorrectCheckpoint " + checkPoint);
            _nextCheckpointIndex = (_nextCheckpointIndex + 1) % _checkPoints.Count;
        }
        else
        {
            Debug.Log("WrongCheckpoint");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            player.transform.position = _nextCheckpointIndex - 1;
        }
    }
}
