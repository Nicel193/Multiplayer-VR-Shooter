using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float WarmUpTime { get; private set; }
        [field: SerializeField] public float MatchTime { get; private set; }
    }
}