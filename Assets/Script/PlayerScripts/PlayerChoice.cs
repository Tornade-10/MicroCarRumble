using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoice : MonoBehaviour
{
    public enum Car
    {
        Sedan,
        Truck,
        Formula,
        PickUp
    }

    public GameObject[] cars;
    public int selectedCar;

    void CharacterChoice()
    {
       //selectedCar = ;
    }
    
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCar", selectedCar);
    }
}
