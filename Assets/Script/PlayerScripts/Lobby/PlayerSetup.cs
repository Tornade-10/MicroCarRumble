using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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

    public void BindInput(PlayerInput input)
    {
        playerIndex = input.playerIndex;
        controlScheme = input.currentControlScheme;
        devices = input.devices.ToArray();
    }
    
    private void LoadNewModel()
    {
        Destroy(body);
        body = Instantiate(profiles[_modelIndex].Model, transform);
    }
    
    void OnChangeCar(InputValue value)
    {
        float v = value.Get<float>();
        
        if (Mathf.Abs(v) == 1f)
        {
            _modelIndex += Mathf.FloorToInt(v);
            if (_modelIndex < 0)
            {
                _modelIndex = profiles.Count - 1;
            }
            else if(_modelIndex >= profiles.Count)
            {
                _modelIndex = 0;
            }
        }
        
        LoadNewModel();
        
    }

    void OnConfirm(InputValue value)
    {
        ready = true;
        onReady?.Invoke();
    }
}
