using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class NetworkVisibility : NetworkBehaviour
    {
        [SerializeField] private bool isLocalVisibility;

        public override void Spawned()
        {
            if (!isLocalVisibility)
            {
                gameObject.SetActive(!Object.HasStateAuthority);
            }
            else
            {
                gameObject.SetActive(Object.HasStateAuthority);
            }
        }
    }
}