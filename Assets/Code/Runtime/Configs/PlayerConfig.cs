using UnityEngine;
using NetworkPlayer = Code.Runtime.Logic.PlayerSystem.NetworkPlayer;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public NetworkPlayer NetworkPlayerPrefab { get; private set; }
        [field: SerializeField] public int MaxPlayerHealth { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}