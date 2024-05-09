using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States.Gameplay;
using Fusion;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class GameTime : NetworkBehaviour, IGameTime
    {
        [Networked] private TickTimer GameTimer { get; set; }
        [Networked] private GameTimeState CurrentGameTimeState { get; set; }

        private GameplayStateMachine _gameplayStateMachine;
        private float _warmUpTime;
        private float _matchTime;

        [Inject]
        private void Construct(GameConfig gameConfig, GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
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
                        _gameplayStateMachine.Enter<MathEndState>();
                        CurrentGameTimeState = GameTimeState.End;

                        Debug.Log("Match end");
                        break;
                    case GameTimeState.End:
                        break;
                }
            }
        }

        [Rpc]
        private void RPC_ChangeState()
        {
            switch (CurrentGameTimeState)
            {
                case GameTimeState.WarmUp:
                    GameTimer = TickTimer.CreateFromSeconds(Runner, _matchTime);
                    CurrentGameTimeState = GameTimeState.Match;
                        
                    Debug.Log("Warm-up end");
                    break;
                case GameTimeState.Match:
                    RPC_EndGame();
                    CurrentGameTimeState = GameTimeState.End;

                    Debug.Log("Match end");
                    break;
                case GameTimeState.End:
                    break;
            }
        }

        [Rpc]
        private void RPC_EndGame()
        {
            _gameplayStateMachine.Enter<MathEndState>();
        }

        public float? GetTimeToEnd()
        {
            return GameTimer.RemainingTime(Runner);
        }
        
        private enum GameTimeState
        {
            WarmUp,
            Match,
            End
        }
    }
}