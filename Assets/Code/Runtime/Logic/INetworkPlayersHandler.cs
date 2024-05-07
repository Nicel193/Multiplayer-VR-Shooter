
using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public interface INetworkPlayersHandler
    {
        void AddPlayerInTeam(PlayerRef playerRef);
        Vector3 GetPlayerSpawnPosition(PlayerRef playerRef);
    }
}