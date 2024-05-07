using Fusion;

namespace Code.Runtime.Logic.PlayerSystem
{
    public interface IPlayerFactory
    {
        NetworkPlayer CreatePlayer(PlayerRef playerRef);
    }
}