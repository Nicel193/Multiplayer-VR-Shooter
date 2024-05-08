using System.Threading.Tasks;
using Fusion;

namespace Code.Runtime
{
    public static class Extensions
    {
        public static async Task WaitObjectSpawned(this NetworkObject networkObject)
        {
            if (networkObject.IsValid)
                await Task.Delay(100);
        }
    }
}