using Fusion;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerView : NetworkBehaviour
    {
        [SerializeField] private XROrigin localXROrigin;

        public override void Spawned()
        {
            if(!Object.HasInputAuthority) return;

            localXROrigin = FindObjectOfType<XROrigin>();
        }

        public override void FixedUpdateNetwork()
        {
            if(localXROrigin == null) return;

            Vector3 xROriginCameraPosition = localXROrigin.Camera.transform.position;

            transform.position = new Vector3(xROriginCameraPosition.x, transform.position.y, xROriginCameraPosition.z);
        }
    }
}