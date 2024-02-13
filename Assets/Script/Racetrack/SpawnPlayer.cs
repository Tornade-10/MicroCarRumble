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
        
        List<PlayerSetup> setups = FindObjectsByType<PlayerSetup>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        
        //put a car on a spawnpoint depending on the player
        for (int i = 0; i < setups.Count; i++)
        {
            Transform spawnPoint = _spawnPoints[i % _spawnPoints.Count].transform;
            GameObject newCar = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

            if (newCar.TryGetComponent<SwitchCar>(out var shape))
            {
                shape.SetProfile(setups[i]);
            }
        }
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Debug.Log("Player " + input.playerIndex + " joined!");
    }
}
