using System;
using System.Collections;
using System.Collections.Generic;
using PlayerControls;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

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

   private Rigidbody carRB;
   private InputManager _controller;

   private void Start()
   {
      //_controller = GetComponent<InputManager>();
      carRB = GetComponent<Rigidbody>();
      carRB.centerOfMass = _centerOfMass;
   }

   private void Update()
   {
      GetInputs();
      AnimationsWheels();
   }

   private void FixedUpdate()
   {
      Move();
      Steer();
   }

   void GetInputs()
   {
      moveInput = Input.GetAxis("Jump");
      steerInput = Input.GetAxis("Horizontal");
   }

   void Move()
   {
      foreach (var wheel in wheels)
      {
         wheel.wheelCollider.motorTorque = moveInput * maxAceleration;
      }
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

   void Brake()
   {
      if(false)
      {
         foreach (var wheel in wheels)
         {
            wheel.wheelCollider.brakeTorque = 0;
         }  
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
