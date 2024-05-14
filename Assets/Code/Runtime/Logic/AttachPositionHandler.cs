using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class AttachPositionHandler : MonoBehaviour
    {
        [SerializeField] private Transform leftAttach;
        [SerializeField] private Transform rightAttach;

        private XRGrabInteractable _xrGrabInteractable;

        private void Awake()
        {
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
        }
        
        public void SetAttach(HandInteractionType handInteractionType)
        {
            _xrGrabInteractable.attachTransform =
                handInteractionType.HandType == HandType.Left ? leftAttach : rightAttach;
        }
    }
}