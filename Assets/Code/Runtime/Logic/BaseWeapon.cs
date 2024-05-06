using System.Threading.Tasks;
using Fusion;
using Fusion.XR.Shared;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic
{
    public class BaseWeapon : NetworkBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private XRSocketInteractor xrSocketInteractor;
        [SerializeField] private float shootForce;

        private XRGrabInteractable _xrGrabInteractable;
        private Magazine _currentMagazine;
        private IXRSelectInteractor _interactorObject;

        private void Awake()
        {
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
        }

        private void OnEnable()
        {
            _xrGrabInteractable.activated.AddListener(Shoot);
            _xrGrabInteractable.selectEntered.AddListener(Grab);
            xrSocketInteractor.selectEntered.AddListener(SelectObject);
        }

        private void OnDisable()
        {
            _xrGrabInteractable.activated.RemoveListener(Shoot);
            _xrGrabInteractable.selectEntered.RemoveListener(Grab);
            xrSocketInteractor.selectEntered.RemoveListener(SelectObject);
        }

        private async void Grab(SelectEnterEventArgs arg)
        {
            GetAuthority();
            
        }

        public async void GetAuthority()
        {
            Object.RequestStateAuthority();

            while (Object.HasStateAuthority == false)
                await Task.Delay(100);
        }

        private void Shoot(ActivateEventArgs arg)
        {
            if (_currentMagazine == null || !_currentMagazine.HasAmmo()) return;

            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

            rigidbody.AddForce(spawnPoint.forward * shootForce, ForceMode.Impulse);
            Destroy(bullet, 5f);

            _currentMagazine.UseAmmo();
        }

        private void SelectObject(SelectEnterEventArgs arg)
        {
            if (arg.interactableObject.transform.TryGetComponent(out Magazine magazine))
                _currentMagazine = magazine;
        }
    }
}