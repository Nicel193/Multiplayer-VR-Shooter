using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class EndGameState : IPayloadedState<Team>
    {
        public EndGameState()
        {

        }
        
        public void Enter(Team winTeam)
        {
            Debug.Log("Win team: " + winTeam);
        }

        public void Exit()
        {
            
        }
    }
}