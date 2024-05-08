using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic
{
    public class NetworkBodySocketInventory : NetworkBehaviour
    {
        [SerializeField] private NetworkPrefabRef[] itemsPrefabs;

        public override void Spawned()
        {
            if(!Object.HasStateAuthority) return;
            
            BodySocketInventory bodySocketInventory = FindObjectOfType<BodySocketInventory>();

            if (bodySocketInventory.bodySockets.Length < itemsPrefabs.Length)
            {
                Debug.LogError("In network inventory items more than possible slots");

                return;
            }

            for (int i = 0; i < itemsPrefabs.Length; i++)
            {
                XRSocketInteractor xrSocketInteractor = bodySocketInventory.bodySockets[i].SocketInteractor;
                Vector3 socketPosition = xrSocketInteractor.gameObject.transform.position;

                NetworkObject spawnedObject = Runner.Spawn(itemsPrefabs[i], socketPosition, Quaternion.identity);
                spawnedObject.transform.SetParent(xrSocketInteractor.transform);
                
                if(spawnedObject.TryGetComponent(out InventoryItem item))
                    item.Initialize(xrSocketInteractor);
            }
        }
    }
}