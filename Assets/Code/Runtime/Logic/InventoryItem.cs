using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(XRGrabInteractable), typeof(Rigidbody))]
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private bool isReturnInventoryItem;
        
        private XRGrabInteractable _xrGrabInteractable;
        private XRSocketInteractor _xrSocketInteractor;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _xrGrabInteractable.selectEntered.AddListener(SelectItem);
            _xrGrabInteractable.selectExited.AddListener(RemoveItem);
        }

        private void OnDisable()
        {
            _xrGrabInteractable.selectEntered.RemoveListener(SelectItem);
            _xrGrabInteractable.selectExited.AddListener(RemoveItem);
        }

        public void Initialize(XRSocketInteractor xrSocketInteractor)
        {
            _xrSocketInteractor = xrSocketInteractor;
            _rigidbody.isKinematic = true;
        }

        private void SelectItem(SelectEnterEventArgs arg)
        {
            _rigidbody.isKinematic = true;
        }

        private void RemoveItem(SelectExitEventArgs arg)
        {
            if (isReturnInventoryItem)
            {
                Transform socketTransform = _xrSocketInteractor.transform;
                
                transform.position = socketTransform.position;
                transform.SetParent(socketTransform);
            }
            else
            {
                transform.SetParent(null);
                _rigidbody.isKinematic = false;
                _xrGrabInteractable = null;
            }
        }

        public bool IsSlotItem() => _xrGrabInteractable != null;
    }
}