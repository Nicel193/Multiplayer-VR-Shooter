using Code.Runtime.Configs;
using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : NetworkBehaviour
    {
        private float _moveSpeed;

        [Inject]
        private void Construct(PlayerConfig playerConfig)
        {
            _moveSpeed = playerConfig.MoveSpeed;
        }
        
        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData data))
            {
                data.direction.Normalize();

                Vector3 direction = Runner.DeltaTime * _moveSpeed * data.direction;

                this.transform.Translate(direction);
            }
        }
    }
}
