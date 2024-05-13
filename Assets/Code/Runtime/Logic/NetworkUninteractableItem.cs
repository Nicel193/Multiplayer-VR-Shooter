using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class NetworkUninteractableItem : NetworkBehaviour
    {
        private XRGrabInteractable _xrGrabInteractable;

        private void Awake()
        {
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
        }

        public override void Spawned() =>
            _xrGrabInteractable.enabled = Object.HasStateAuthority;
    }
}