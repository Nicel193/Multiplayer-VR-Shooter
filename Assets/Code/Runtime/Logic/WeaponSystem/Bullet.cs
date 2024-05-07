using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic.WeaponSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : NetworkBehaviour
    {
        private const float TimeToDespawn = 3f;

        [Networked] private TickTimer Timer { set; get; }
        private Rigidbody _bulletRigidbody;
        private int _damage;

        private void Awake()
        {
            _bulletRigidbody = GetComponent<Rigidbody>();
        }

        public override void Spawned()
        {
            Timer = TickTimer.CreateFromSeconds(Runner, TimeToDespawn);
        }

        public override void FixedUpdateNetwork()
        {
            if(Timer.Expired(Runner))
            {
                Runner.Despawn(Object);
            }
        }

        public void Initialize(int damage)
        {
            _damage = damage;
        }

        public void Launch(Vector3 direction, float force)
        {
            direction.Normalize();
   
            _bulletRigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.RPC_Damage(_damage);
                
                Runner.Despawn(Object);
            }
        }
    }
}