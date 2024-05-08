using System.Threading.Tasks;
using Fusion;

namespace Code.Runtime
{
    public static class Extensions
    {
        public static async Task WaitObjectSpawned(this NetworkRunner networkRunner)
        {
            if (networkRunner.IsFirstTick)
                await Task.Delay(100);
        }
    }
}