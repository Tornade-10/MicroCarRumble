using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CarDatas/PlayerProfile", fileName = "PlayerProfile")]
public class PlayerProfile : ScriptableObject
{
    //a scriptable object in wich there is the model in the loby, the model name and the car on the racetrack
    public GameObject ModelLobby;
    public string ModelName;
    public GameObject PrefRace;
}
