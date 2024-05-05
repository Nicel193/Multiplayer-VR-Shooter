using System;
using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class PlayerData : NetworkBehaviour
    {
        public event Action OnPlayerDead;
        
        public int MAXHealth { get; private set; }
        public int MAXAmmo { get; private set; }
        
        public int Health { get; private set; }
        public int Ammo { get; private set; }

        public void Initialize(int maxHealth, int maxAmmo)
        {
            MAXHealth = maxHealth;
            MAXAmmo = maxAmmo;
            
            Health = maxHealth;
            Ammo = maxAmmo;
        }
        
        
        public void UseAmmo()
        {
            if(Ammo <= 0) return;
            
            Ammo--;
        }

        public void RestoreAmmo(int ammoCount)
        {
            if(ammoCount <= 0) return;
            
            Ammo += ammoCount;
            
            Ammo = Mathf.Clamp(Ammo, 0, MAXAmmo);
        }

        public void Damage(int damage)
        {
            if(damage <= 0) return;
            
            Health -= damage;

            Health = Mathf.Clamp(Health, 0, MAXHealth);
            
            if(Health == 0) OnPlayerDead?.Invoke();
        }
    }
}