using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerRig : MonoBehaviour
    {
        [field: SerializeField] public Transform Camera { get; private set; }
        [field: SerializeField] public Transform LeftHand { get; private set; }
        [field: SerializeField] public Transform RightHand { get; private set; }
    }
}