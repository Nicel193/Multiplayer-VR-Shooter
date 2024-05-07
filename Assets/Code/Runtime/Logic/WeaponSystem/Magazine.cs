using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic.WeaponSystem
{
    public class Magazine : NetworkBehaviour
    {
        [HideInInspector, Networked] public int MaxAmmoCount { get; private set; } = 30;
        [HideInInspector, Networked] public int AmmoCount { get; private set; }

        public override void Spawned() =>
            AmmoCount = MaxAmmoCount;

        public void UseAmmo() =>
            AmmoCount--;

        public bool HasAmmo() =>
            AmmoCount > 0;
    }
}