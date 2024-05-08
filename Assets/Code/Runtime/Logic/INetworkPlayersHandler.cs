
using Fusion;

namespace Code.Runtime.Logic
{
    public interface INetworkPlayersHandler
    {
        void AddPlayer(PlayerRef playerRef);
        void MovePlayerInStartPosition();
        void RemovePlayer(PlayerRef playerRef);
    }
}