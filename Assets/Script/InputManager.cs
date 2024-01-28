using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControls
{
    public class InputManager : MonoBehaviour
    {
        [Header("Movement Input")] 
        public bool accelerate;
        public bool brake;
        public Vector2 steer;

#if ENABLE_INPUT_SYSTEM

        public void OnForward(InputValue value)
        {
            AccelerateInput(value.isPressed);
        }

        public void OnBrake(InputValue value)
        {
            BrakeInput(value.isPressed);
        }

        public void OnSteer(InputValue value)
        {
            SteerInput(value.Get<Vector2>());
        }
        
#endif
        //
        public void AccelerateInput(bool newAccelerateState)
        {
            accelerate = newAccelerateState;
            Debug.Log("Accelerating");
        }

        public void BrakeInput(bool newBrakeState)
        {
            brake = newBrakeState;
            Debug.Log("Braking");
        }

        public void SteerInput(Vector2 newSteerState)
        {
            steer = newSteerState;
            Debug.Log("steering in " + steer + " direction");
        }
        
    }
}


