using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CarController : MonoBehaviour
{
   //creat two axel
   public enum Axel
   {
      Front,
      Rear
   }
   
   //creat a list of wheel
   public struct Wheel
   {
      public GameObject wheelModel;
      public WheelCollider wheelCollider;
      public Axel axel;
   }
   
   //the default value for the wheel rpm
   public float maxAceleration = 30.0f;
   public float brakeAcceleration = 50.0f;

   //default value for how high angle can the wheel turn
   public float turnSensitivity = 1.0f;
   public float maxSteerAngle = 30.0f;
   
   public Vector3 centerOfMass;
   
   public List<Wheel> wheels;

   public float moveInput;
   public float steerInput;
   public bool lookInput = false;

   private Rigidbody _carRb;

   //get the two cinemachine virtual camera
   public CinemachineVirtualCamera backCamera;
   public CinemachineVirtualCamera frontCamera;

   //get the player camera
   public int cameraNumber;
   
   //some numbers to set the camera at the front or the back
   private int _activePriority = 15;
   private int _inactivePriority = 10;
   
   private void Start()
   {
      //set the center of mass to the midle and botom
      _carRb = GetComponent<Rigidbody>();
      _carRb.centerOfMass = centerOfMass;
   }

   private void Update()
   {
      AnimationsWheels();
      CameraLook();
      SplitCamera();
   }

   private void FixedUpdate()
   {
      Move();
      Steer();
   }
   
   
   // Forward
   void OnForward(InputValue value)
   {
      moveInput = value.Get<float>();
   }
   
   void Move()
   {
      foreach (var wheel in wheels)
      {
         wheel.wheelCollider.motorTorque = moveInput * maxAceleration;
      }
   }

   // Steer
   void OnSteer(InputValue value)
   {
      steerInput = value.Get<float>();
   }

   void Steer()
   {
      foreach (var wheel in wheels)
      {
         if (wheel.axel == Axel.Front)
         {
            var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
            wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
         }
      }
   }
   
   // Brake
   void OnBrake(InputValue value)
   {
      moveInput = value.Get<float>() * -0.5f;
      foreach (var wheel in wheels)
      {
         wheel.wheelCollider.brakeTorque = 0;
      }  
   }

   // Look
   void OnLookBehind(InputValue value)
   {
      lookInput = value.isPressed;
   }

   void CameraLook()
   {
      if (lookInput)
      {
         backCamera.Priority = _inactivePriority;
         frontCamera.Priority = _activePriority;
      }
      else
      {
         backCamera.Priority = _activePriority;
         frontCamera.Priority = _inactivePriority;
      }
   }
   
   //set the angle of the wheel and the rotation
   void AnimationsWheels()
   {
      foreach (var wheel in wheels)
      {
         Quaternion rotation;
         Vector3 position;
         wheel.wheelCollider.GetWorldPose(out position, out rotation);
         wheel.wheelModel.transform.position = position;
         wheel.wheelModel.transform.rotation = rotation;
      }
   }

   //set the layer of both the virtual camera depending on the player
   void SplitCamera()
   {
       backCamera.gameObject.layer = LayerMask.NameToLayer("CamPlayer" + (cameraNumber + 1));
       frontCamera.gameObject.layer = LayerMask.NameToLayer("CamPlayer" + (cameraNumber + 1));
   }
   
}
