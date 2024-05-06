using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic.WeaponSystem
{
    public abstract class BaseWeapon : NetworkBehaviour
    {
        [SerializeField] protected Transform SpawnBulletPoint;
        [SerializeField] protected Bullet BulletPrefab;
        [SerializeField] protected int Damage;
        [SerializeField] protected float ShootForce;
        [SerializeField] private XRSocketInteractor magazineSocket;

        private Magazine _currentMagazine;
        private XRGrabInteractable _xrGrabInteractable;
        private IXRSelectInteractor _interactorObject;
        private float _shootInterval;
        private float _shootTimer;

        private void Awake() =>
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();

        private void OnEnable()
        {
            _xrGrabInteractable.activated.AddListener(Shoot);
            magazineSocket.selectEntered.AddListener(SelectMagazine);
        }

        private void OnDisable()
        {
            _xrGrabInteractable.activated.RemoveListener(Shoot);
            magazineSocket.selectEntered.RemoveListener(SelectMagazine);
        }

        public override void FixedUpdateNetwork()
        {
            if (_shootTimer < _shootInterval)
                _shootTimer += Runner.DeltaTime;
        }

        private void Shoot(ActivateEventArgs arg)
        {
            if(_currentMagazine == null) return;
            
            if (_currentMagazine.HasAmmo() && _shootTimer >= _shootInterval)
            {
                ShootImplementation(SpawnBulletPoint.forward);
                
                _currentMagazine.UseAmmo();
                _shootTimer = 0f;
            }
        }

        protected abstract void ShootImplementation(Vector3 direction);
        
        private void SelectMagazine(SelectEnterEventArgs arg)
        {
            if (arg.interactableObject.transform.TryGetComponent(out Magazine magazine))
                _currentMagazine = magazine;
        }
    }
}