using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using PlayerControls;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
using Vector2 = System.Numerics.Vector2;

public class CarController : MonoBehaviour
{
   public enum Axel
   {
      Front,
      Rear
   }
   
   [Serializable] 
   public struct Wheel
   {
      public GameObject WheelModel;
      public WheelCollider wheelCollider;
      public Axel axel;
   }

   public float maxAceleration = 30.0f;
   public float brakeAcceleration = 50.0f;

   public float turnSensitivity = 1.0f;
   public float maxSteerAngle = 30.0f;

   public Vector3 _centerOfMass;
   
   public List<Wheel> wheels;

   public float moveInput;
   public float steerInput;
   public bool lookInput = false;

   private Rigidbody carRB;
   private InputManager _controller;
   
   public CinemachineVirtualCamera backCamera;
   public CinemachineVirtualCamera frontCamera;
   
   private int _activePriority = 15;
   private int _inactivePriority = 10;
   
   private void Start()
   {
      //_controller = GetComponent<InputManager>();
      carRB = GetComponent<Rigidbody>();
      carRB.centerOfMass = _centerOfMass;
   }

   private void Update()
   {
      AnimationsWheels();
      CameraLook();
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
   
   void AnimationsWheels()
   {
      foreach (var wheel in wheels)
      {
         Quaternion rotation;
         Vector3 position;
         wheel.wheelCollider.GetWorldPose(out position, out rotation);
         wheel.WheelModel.transform.position = position;
         wheel.WheelModel.transform.rotation = rotation;
      }
   }
   
}
