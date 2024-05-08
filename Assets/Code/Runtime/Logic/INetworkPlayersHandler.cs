using Fusion;

namespace Code.Runtime.Logic
{
    public interface INetworkPlayersHandler
    {
        void AddPlayer(PlayerRef playerRef);
        
        [Rpc]
        void RPC_RemovePlayer(PlayerRef playerRef);

        void MovePlayerInStartPosition(PlayerRef playerRef);
    }
}