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

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.RawButton.Y) || OVRInput.GetDown(OVRInput.RawButton.B))
                _windowService.OpenWindow<GameStatsWindow>();
            else
                _windowService.Close();
        }
    }
}
