using System;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button connectButton;
        [SerializeField] private Button generateNickButton;
        [SerializeField] private TMP_InputField nickInputField;
        
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            connectButton.onClick.AddListener(Connect);
        }

        private void OnDestroy()
        {
            connectButton.onClick.RemoveListener(Connect);
        }

        private void Connect()
        {
            _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Gameplay.ToString());
        }
    }
}