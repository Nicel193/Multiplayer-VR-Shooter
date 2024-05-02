using Fusion;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class NetworkSpawner : NetworkBehaviour, IPlayerJoined
    {
        public void PlayerJoined(PlayerRef player)
        {
            Debug.Log("Player Joined");
        }
    }
}
