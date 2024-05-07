using System;
using Code.Runtime.Logic.WeaponSystem;
using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerHealth : NetworkBehaviour, IDamageable
    {
        public event Action OnPlayerDead;
        
        [HideInInspector, Networked] public int MAXHealth { get; private set; }
        [HideInInspector, Networked] public int Health { get; private set; }

        private bool _isPlayerDead;

        public void Initialize(int maxHealth)
        {
            MAXHealth = maxHealth;

            Health = maxHealth;
        }
        
        [Rpc]
        public void RPC_Damage(int damage)
        {
            if(damage <= 0 || _isPlayerDead) return;
            
            Health -= damage;

            Health = Mathf.Clamp(Health, 0, MAXHealth);

            if (Health == 0)
            {
                OnPlayerDead?.Invoke();
                
                _isPlayerDead = true;
            }
        }

        [Rpc]
        public void RPC_ResumeHealth()
        {
            Health = MAXHealth;

            _isPlayerDead = false;
        }
    }
}