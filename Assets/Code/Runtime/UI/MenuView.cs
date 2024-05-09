using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Core;
using Code.Runtime.Repositories;
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
        private UserRepository _userRepository;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, UserRepository userRepository)
        {
            _userRepository = userRepository;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            connectButton.onClick.AddListener(Connect);
            generateNickButton.onClick.AddListener(GenerateNickname);
            nickInputField.onValueChanged.AddListener(NicknameChanged);
        }

        private void Start() => GenerateNickname();

        private void OnDestroy()
        {
            connectButton.onClick.RemoveListener(Connect);
            generateNickButton.onClick.RemoveListener(GenerateNickname);
        }

        private void Connect()
        {
            _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Gameplay.ToString());
        }

        private void GenerateNickname()
        {
            _userRepository.GenerateNickname();
            nickInputField.text = _userRepository.Nickname;
        }

        private void NicknameChanged(string nickname)
        {
            _userRepository.SetNickname(nickname);
            nickInputField.text = _userRepository.Nickname;
        }
    }
}