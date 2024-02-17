using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    //creat a list of SpawnPoint
    private List<SpawnPoint> _spawnPoints;
    //creat a list of "LobySetup" that will be renamed "PlayerSetup"
    private List<PlayerSetup> _joinedSetup = new List<PlayerSetup>();
    
    // Start is called before the first frame update
    private void Start()
    {
        //add that has the the SpawnPoint script to the SpawnPoint List
        _spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        //send a message in the consol when a player join
        Debug.Log("Player joined : " + input.playerIndex + " : " + input.gameObject + " : " + input.devices);
        
        //creat a new player in the lobby
        if (input.gameObject.TryGetComponent<PlayerSetup>(out var setup))
        {
            //spawn a player at a spawn point
            var spawnPoint = setup.transform;
            spawnPoint.position = _spawnPoints[input.playerIndex].transform.position;
            //give the player the rotation of the spawn point
            spawnPoint.rotation = _spawnPoints[input.playerIndex].transform.rotation;
            
            setup.BindInput(input);

            FindObjectOfType<AudioManager>().Play("Join");
            
            setup.onReady += CheckEveryoneIsReady;
            _joinedSetup.Add(setup);
            
            DontDestroyOnLoad(setup);
        }
    }

    private void CheckEveryoneIsReady()
    {
        //if there is no player the game doesn't start
        if (_joinedSetup.Count <= 0)
        {
            return;
        }
        
        bool everyoneIsReady = true;
        //check if everyone is ready
        foreach(var setup in _joinedSetup)
        {
            //if someone isn't ready set "everyoneIsReady" to false
            if (!setup.ready)
            {
                everyoneIsReady = false;
            }
        }

        //if everyone is ready lauch the new scene
        if (everyoneIsReady)
        {
            foreach (PlayerSetup setup in _joinedSetup)
            {
                setup.gameObject.SetActive(false);
            }
            Debug.Log("Loading the game scene");
            //load the race track
            SceneManager.LoadScene("Office");
        }
    }
}
