using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public NetworkPlayerRig NetworkPlayerRigPrefab { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}