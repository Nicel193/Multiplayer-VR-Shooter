using UnityEngine;

namespace Code.Runtime.Logic.WeaponSystem
{
    public class SemiAutomaticWeapon : BaseWeapon
    {
        protected override void ShootImplementation(Vector3 direction)
        {
            Bullet bullet = Runner.Spawn(BulletPrefab, SpawnBulletPoint.position, Quaternion.identity);
            
            bullet.Initialize(Damage);
            bullet.Launch(direction, ShootForce);
        }
    }
}