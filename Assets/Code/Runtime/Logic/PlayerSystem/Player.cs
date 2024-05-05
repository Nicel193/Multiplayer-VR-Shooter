using Fusion;

namespace Code.Runtime.Logic.PlayerSystem
{
    public class Player : NetworkBehaviour
    {
        private void Start()
        {
            PlayerData playerData = GetComponent<PlayerData>();

            playerData.Initialize(100, 30);
        }
    }
}