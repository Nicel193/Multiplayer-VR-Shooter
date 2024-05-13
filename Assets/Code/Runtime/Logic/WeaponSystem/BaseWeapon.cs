using Code.Runtime.Logic.PlayerSystem;
using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic.WeaponSystem
{
    public abstract class BaseWeapon : NetworkBehaviour
    {
        public Magazine CurrentMagazine { get; private set; }
        
        [SerializeField] protected Transform SpawnBulletPoint;
        [SerializeField] protected Bullet BulletPrefab;
        [SerializeField] protected int Damage;
        [SerializeField] protected float ShootForce;
        [SerializeField] private XRSocketInteractor magazineSocket;
        [SerializeField] private float shootInterval;

        protected PlayerData PlayerData;
        private XRGrabInteractable _xrGrabInteractable;
        private IXRSelectInteractor _interactorObject;
        private float _shootTimer;

        private void Awake() =>
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();

        private void OnEnable()
        {
            _xrGrabInteractable.activated.AddListener(Shoot);
            magazineSocket.selectEntered.AddListener(SelectMagazine);
            magazineSocket.selectExited.AddListener(RemoveMagazine);
        }

        private void OnDisable()
        {
            _xrGrabInteractable.activated.RemoveListener(Shoot);
            magazineSocket.selectEntered.RemoveListener(SelectMagazine);
            magazineSocket.selectExited.RemoveListener(RemoveMagazine);
        }

        public override void FixedUpdateNetwork()
        {
            if (_shootTimer < shootInterval)
                _shootTimer += Runner.DeltaTime;
        }

        public void Initialize(PlayerData playerData)
        {
            PlayerData = playerData;
        }

        protected abstract void ShootImplementation(Vector3 direction);

        private void Shoot(ActivateEventArgs arg)
        {
            if(CurrentMagazine == null) return;
            
            if (CurrentMagazine.HasAmmo() && _shootTimer >= shootInterval)
            {
                ShootImplementation(SpawnBulletPoint.forward);
                
                CurrentMagazine.UseAmmo();
                _shootTimer = 0f;
            }
        }

        // private void ThrowMagazine()
        // {
        //     
        // }

        private void SelectMagazine(SelectEnterEventArgs arg)
        {
            if (arg.interactableObject.transform.TryGetComponent(out Magazine magazine))
                CurrentMagazine = magazine;
        }

        private void RemoveMagazine(SelectExitEventArgs arg) =>
            CurrentMagazine = null;
    }
}