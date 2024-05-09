using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem;
using Code.Runtime.Service;
using Code.Runtime.UI.Windows;
using Fusion;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class MathEndState : IState
    {
        private INetworkPlayersHandler _networkPlayersHandler;
        private IWindowService _windowService;
        private PlayerData[] _playersData;

        public MathEndState(INetworkPlayersHandler networkPlayersHandler, IWindowService windowService)
        {
            _windowService = windowService;
            _networkPlayersHandler = networkPlayersHandler;
        }

        public void Enter()
        {
            InitializePlayers();
            Team winTeam = FindWinTeam();

            _windowService.OpenPayloadWindow<EndGameWindow, Team>(winTeam);
        }

        public void Exit()
        {
        }

        private Team FindWinTeam()
        {
            IReadOnlyDictionary<PlayerRef, Team> players = _networkPlayersHandler.GetTeamsPlayers;

            Team winTeam = Team.Blue;
            int maxKills = 0;

            foreach (KeyValuePair<PlayerRef, Team> playerInfo in players)
            {
                PlayerData playerData = GetPlayerByRef(playerInfo.Key);
                int playerDataKills = playerData.Kills;

                if (playerDataKills > maxKills)
                {
                    winTeam = playerInfo.Value;
                    maxKills = playerDataKills;
                }
            }

            return winTeam;
        }

        private void InitializePlayers() =>
            _playersData = Object.FindObjectsByType<PlayerData>(FindObjectsSortMode.None);

        private PlayerData GetPlayerByRef(PlayerRef playerRef) =>
            _playersData.FirstOrDefault(pd => pd.PlayerRef == playerRef);
    }
}