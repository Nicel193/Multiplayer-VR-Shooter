using UnityEngine;

namespace Code.Runtime.Logic.PlayerSystem
{
    public interface INetworkPlayer
    {
        Transform WindowPosition { get; }
        PlayerData PlayerData { get; }
    }
}