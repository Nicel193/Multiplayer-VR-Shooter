using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(XRSocketInteractor))]
    public class InfinitySocketInventory : NetworkBehaviour
    {
        [SerializeField] private NetworkPrefabRef itemPrefab;
        [SerializeField] private float respawnInterval = 1f;

        [Networked] private TickTimer IntervalTimer { get; set; }
        private XRSocketInteractor _xrSocketInteractor;
        private bool _isItemRemoved;
        private bool _isSpawned;

        private void Awake()
        {
            _xrSocketInteractor = GetComponent<XRSocketInteractor>();

            _xrSocketInteractor.selectExited.AddListener(RemoveItem);
            _xrSocketInteractor.selectEntered.AddListener(ReturnItem);
        }

        private void OnDestroy()
        {
            _xrSocketInteractor.selectExited.RemoveListener(RemoveItem);
            _xrSocketInteractor.selectEntered.RemoveListener(ReturnItem);
        }

        public override void Spawned()
        {
            _isSpawned = true;
        }

        public void Update()
        {
            if(!_isSpawned) return;

            if (_isItemRemoved && IntervalTimer.Expired(Runner))
            {
                SpawnItem();
                
                _isItemRemoved = false;
            }
        }

        private void RemoveItem(SelectExitEventArgs arg)
        {
            StartIntervalTimer();
            
            _isItemRemoved = true;
        }

        private void ReturnItem(SelectEnterEventArgs arg)
        {
            _isItemRemoved = false;
        }

        private void StartIntervalTimer() =>
            IntervalTimer = TickTimer.CreateFromSeconds(Runner, respawnInterval);

        private void SpawnItem()
        {
            NetworkObject spawnedItem = Runner.Spawn(itemPrefab, _xrSocketInteractor.transform.position, Quaternion.identity);

            spawnedItem.transform.SetParent(_xrSocketInteractor.transform);

            if (spawnedItem.TryGetComponent(out InventoryItem item))
                item.Initialize(_xrSocketInteractor);
        }
    }
}