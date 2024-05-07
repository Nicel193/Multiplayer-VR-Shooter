using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerRig : MonoBehaviour
    {
        [field: SerializeField] public Transform Camera { get; private set; }
        [field: SerializeField] public XRDirectInteractor LeftHand { get; private set; }
        [field: SerializeField] public XRDirectInteractor RightHand { get; private set; }
    }
}