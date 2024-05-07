using Fusion;

namespace Code.Runtime.Logic.WeaponSystem
{
    public interface IDamageable
    {
        [Rpc]
        void RPC_Damage(int damage);
    }
}