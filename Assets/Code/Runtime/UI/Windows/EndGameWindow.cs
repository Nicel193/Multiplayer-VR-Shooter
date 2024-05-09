using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Core;
using Code.Runtime.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class EndGameWindow : MonoBehaviour, IEndGameWindow, IPayloadOpenWindow<Team>
    {
        [SerializeField] private TextMeshProUGUI winTeamText;
        [SerializeField] private Button exitButton;
        
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake() =>
            exitButton.onClick.AddListener(ExitInMenu);

        private void OnDestroy() =>
            exitButton.onClick.RemoveListener(ExitInMenu);

        public void Open(Team winTeam)
        {
            gameObject.SetActive(true);

            winTeamText.text = $"Win team: {winTeam.ToString()}";
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void ExitInMenu()
        {
            _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Menu.ToString());
        }
    }
}