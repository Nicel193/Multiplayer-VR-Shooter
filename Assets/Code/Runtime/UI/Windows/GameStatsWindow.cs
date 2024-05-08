using Code.Runtime.Logic;
using Code.Runtime.Logic.PlayerSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class GameStatsWindow : MonoBehaviour, IOpenWindow
    {
        [SerializeField] private TextMeshProUGUI playerKillsCount;
        [SerializeField] private TextMeshProUGUI timeToEndGame;
        
        private INetworkPlayersHandler _networkPlayersHandler;
        private IGameTime _gameTime;

        [Inject]
        private void Construct(INetworkPlayersHandler networkPlayersHandler, IGameTime gameTime)
        {
            _gameTime = gameTime;
            _networkPlayersHandler = networkPlayersHandler;
        }

        private void Update()
        {
            DrawKillsCount();
            DrawTimeToEndGame();
        }

        private void DrawTimeToEndGame()
        {
            float? timeToEnd = _gameTime.GetTimeToEnd();

            if (timeToEnd == null) return;

            timeToEndGame.text = GetStringTime(timeToEnd.Value);
        }

        private void DrawKillsCount()
        {
            INetworkPlayer localNetworkPlayer = _networkPlayersHandler.LocalNetworkPlayer;
            
            if(localNetworkPlayer == null) return;
            
            int killsCount = localNetworkPlayer.PlayerData.Kills;
            playerKillsCount.text = $"Your kills: {killsCount}";
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        private string GetStringTime(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60);
            
            return $"{minutes:00}:{seconds:00}";
        }
    }
}