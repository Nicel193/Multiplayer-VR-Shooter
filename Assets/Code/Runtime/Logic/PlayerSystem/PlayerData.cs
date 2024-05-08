using System;
using Code.Runtime.Logic.WeaponSystem;
using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class PlayerData : NetworkBehaviour, IDamageable
    {
        public event Action OnPlayerDead;
        
        [HideInInspector, Networked] public int MAXHealth { get; private set; }
        [HideInInspector, Networked] public int Health { get; private set; }
        [HideInInspector, Networked] public int Kills { get; private set; }

        [Networked] private bool IsPlayerDead { get; set; }

        public void Initialize(int maxHealth)
        {
            MAXHealth = maxHealth;

            Health = maxHealth;
        }

        [Rpc]
        public void RPC_AddKill()
        {
            Kills++;
        }
        
        [Rpc]
        public void RPC_Damage(int damage)
        {
            if(damage <= 0 || IsPlayerDead) return;
            
            Health -= damage;

            Health = Mathf.Clamp(Health, 0, MAXHealth);

            if (Health == 0)
            {
                OnPlayerDead?.Invoke();
                
                IsPlayerDead = true;
            }
        }

        [Rpc]
        public void RPC_ResumeHealth()
        {
            Health = MAXHealth;

            IsPlayerDead = false;
        }

        public bool IsDead() =>
            IsPlayerDead;
    }
}