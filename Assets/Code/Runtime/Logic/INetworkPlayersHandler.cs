using Fusion;

namespace Code.Runtime.Logic
{
    public interface INetworkPlayersHandler
    {
        [Rpc]
        void RPC_AddPlayer(PlayerRef playerRef);
        
        [Rpc]
        void RPC_RemovePlayer(PlayerRef playerRef);

        void MovePlayerInStartPosition(PlayerRef playerRef);
    }
}