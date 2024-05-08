using Fusion;

namespace Code.Runtime.Logic
{
    public interface INetworkPlayersHandler
    {
        void AddPlayer(PlayerRef playerRef);
        
        void RemovePlayer(PlayerRef playerRef);

        void MovePlayerInStartPosition(PlayerRef playerRef);
    }
}