using Code.Runtime.Configs;
using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class GameTime : NetworkBehaviour
    {
        [Networked] private TickTimer GameTimer { get; set; }
        [Networked] private GameTimeState CurrentGameTimeState { get; set; }

        private float _warmUpTime;
        private float _matchTime;

        [Inject]
        private void Construct(GameConfig gameConfig)
        {
            _warmUpTime = gameConfig.WarmUpTime;
            _matchTime = gameConfig.MatchTime;
        }

        public override void Spawned()
        {
            if(!Object.HasStateAuthority) return;
            
            GameTimer = TickTimer.CreateFromSeconds(Runner, _warmUpTime);
            CurrentGameTimeState = GameTimeState.WarmUp;
        }
        
        public override void FixedUpdateNetwork()
        {
            if (GameTimer.Expired(Runner))
            {
                switch (CurrentGameTimeState)
                {
                    case GameTimeState.WarmUp:
                        GameTimer = TickTimer.CreateFromSeconds(Runner, _matchTime);
                        CurrentGameTimeState = GameTimeState.Match;
                        
                        Debug.Log("Warm-up end");
                        break;
                    case GameTimeState.Match:
                        CurrentGameTimeState = GameTimeState.End;
                        
                        Debug.Log("Match end");
                        break;
                    case GameTimeState.End:
                        break;
                }
            }
        }
        
        private enum GameTimeState
        {
            WarmUp,
            Match,
            End
        }
    }
}