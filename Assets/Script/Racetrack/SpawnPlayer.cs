using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject carPrefab;
    private List<SpawnPoint> _spawnPoints;

    public void Start()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        
        List<PlayerSetup> setUps = FindObjectsByType<PlayerSetup>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        
        for (int i = 0; i < setUps.Count; i++)
        {
            Transform t = _spawnPoints[i % _spawnPoints.Count].transform;
            GameObject newCar = Instantiate(carPrefab, t.position, t.rotation);

            if (newCar.TryGetComponent<SwitchCar>(out var shape))
            {
                shape.SetProfile(setUps[i]);
            }
        }
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Debug.Log("Player " + input.playerIndex + " joined!");
    }
}
