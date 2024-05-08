using Code.Runtime.Logic.PlayerSystem;
using Fusion;

namespace Code.Runtime.Logic
{
    public interface INetworkPlayersHandler
    {
        INetworkPlayer LocalNetworkPlayer { get; }
        
        void AddPlayer(PlayerRef playerRef, INetworkPlayer networkPlayer);

        void RemovePlayer(PlayerRef playerRef);

        void MovePlayerInStartPosition(PlayerRef playerRef);
    }
}