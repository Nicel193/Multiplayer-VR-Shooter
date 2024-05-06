using Fusion;

namespace Code.Runtime.Logic.PlayerSystem
{
    public interface IPlayerFactory
    {
        void CreatePlayer(PlayerRef playerRef);
    }
}