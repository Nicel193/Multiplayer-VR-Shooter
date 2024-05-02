using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class SimpleAI : NetworkBehaviour
    {
        public float moveSpeed = 3f;
        public float stoppingDistance = 2f;

        private Transform _player;

        public void Initialize(Transform player) =>
            _player = player;

        public override void FixedUpdateNetwork()
        {
            if (_player != null)
            {
                Vector3 direction = (_player.position - transform.position).normalized;
            
                float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

                if (distanceToPlayer > stoppingDistance)
                {
                    Vector3 movement = direction * moveSpeed * Time.deltaTime;

                    transform.Translate(movement);
                }
            }
        }
    }
}