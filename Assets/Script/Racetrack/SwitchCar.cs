using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCar : MonoBehaviour
{

    [SerializeField] private GameObject body;
    [SerializeField] private PlayerInput playerInput;
    private PlayerProfile _profile;
    
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        //if there is no PlayerInput it send an error message
        if (playerInput == null)
        {
            Debug.LogWarning("No Input");
        }
    }
    
    public void SetProfile(PlayerSetup profile)
    {
        _profile = profile.Profile;
        // ShapeShifting
        Destroy(body);
        body = Instantiate(_profile.Model, transform);
        // Input rebinding
        playerInput.SwitchCurrentControlScheme(profile.ControlScheme, profile.Devices);
    }
}
