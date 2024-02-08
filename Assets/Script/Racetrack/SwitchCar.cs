using UnityEditor.SceneManagement;
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
    
    public void SetProfile(PlayerSetup setup)
    {
        _profile = setup.Profile;
        // ShapeShifting
        Destroy(body);
        body = Instantiate(_profile.PrefRace, transform);
        // Input rebinding
        playerInput.SwitchCurrentControlScheme(setup.ControlScheme, setup.Devices);

        //add a new culling mask depending on wich car it is
        playerInput.camera.cullingMask |= 1 << LayerMask.NameToLayer("CamPlayer" + (setup.playerIndex + 1));
        
        //get each player and set they player index  
        CarController carController = body.GetComponent<CarController>();
        carController.cameraNumber = setup.playerIndex;
    }
}
