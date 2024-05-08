using System;
using Code.Runtime.Service;
using Code.Runtime.UI.Windows;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Runtime.Logic
{
    public class WindowsInput : MonoBehaviour
    {
        [SerializeField] private InputActionProperty openGameStatsWindow;
        private IWindowService _windowService;

        [Inject]
        private void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

        private void Awake()
        {
            openGameStatsWindow.action.performed += OpenGameStatsWindow;
            openGameStatsWindow.action.canceled += CloseGameStatsWindow;
        }

        private void OnDestroy()
        {
            openGameStatsWindow.action.performed -= OpenGameStatsWindow;
            openGameStatsWindow.action.canceled += CloseGameStatsWindow;
        }

        private void OpenGameStatsWindow(InputAction.CallbackContext obj)
        {
            _windowService.OpenWindow<GameStatsWindow>();
        }

        private void CloseGameStatsWindow(InputAction.CallbackContext obj)
        {
            _windowService.Close();
        }
    }
}