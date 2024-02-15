using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CarDatas/PlayerProfile", fileName = "PlayerProfile")]
public class PlayerProfile : ScriptableObject
{
    //a scriptable object in wich there is the model in the loby, the model name and the car on the racetrack
    //the model in the loby
    public GameObject ModelLobby;
    //the name of the model
    public string ModelName;
    //PrefRace is the prefab that'll be use for the race
    public GameObject PrefRace;
}
