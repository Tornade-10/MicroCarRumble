using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{

    [SerializeField] private List<PlayerProfile> profiles;
    [SerializeField] private GameObject body;

    public Action onReady;
    
    public int playerIndex;
    public string controlScheme;
    public InputDevice[] devices;

    private int _modelIndex = 0;
    public bool ready;
    public PlayerProfile Profile => profiles[_modelIndex];
    public string ControlScheme => controlScheme; 
    public InputDevice[] Devices => devices;
    
    private void Start()
    {
        LoadNewModel();
    }

    //the inputdevice and control scheme depend on the the player
    public void BindInput(PlayerInput input)
    {
        playerIndex = input.playerIndex;
        controlScheme = input.currentControlScheme;
        devices = input.devices.ToArray();
    }
    
    private void LoadNewModel()
    {
        Destroy(body);
        body = Instantiate(profiles[_modelIndex].ModelLobby, transform);
    }
    
    void OnChangeCar(InputValue value)
    {
        //get the input value as a float
        float v = value.Get<float>();
        
        //change the car depending on the input value
        if (Mathf.Abs(v) == 1f)
        {
            _modelIndex += Mathf.FloorToInt(v);
            //put the car at the end of the line
            if (_modelIndex < 0)
            {
                _modelIndex = profiles.Count - 1;
            }//put the car at the start of the line
            else if(_modelIndex >= profiles.Count)
            {
                _modelIndex = 0;
            }
        }
        
        //load the new model
        LoadNewModel();
    }

    void OnConfirm(InputValue value)
    {
        ready = true;
        onReady?.Invoke();
    }
}
