using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CarDatas/PlayerProfile", fileName = "PlayerProfile")]
public class PlayerProfile : ScriptableObject
{
    public GameObject ModelLobby;
    public string ModelName;
    public GameObject PrefRace;
}
