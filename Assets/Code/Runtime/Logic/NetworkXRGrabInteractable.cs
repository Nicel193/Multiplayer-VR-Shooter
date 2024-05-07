using System;
using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class NetworkXRGrabInteractable : NetworkBehaviour
    {
        private XRGrabInteractable _xrGrabInteractable;

        private void Awake() =>
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();

        private void OnEnable() =>
            _xrGrabInteractable.selectEntered.AddListener(Grab);

        private void OnDisable() =>
            _xrGrabInteractable.selectEntered.RemoveListener(Grab);

        private void Grab(SelectEnterEventArgs arg)
        {
            if(Object == null) return;

            Object.RequestStateAuthority();
        }
    }
}