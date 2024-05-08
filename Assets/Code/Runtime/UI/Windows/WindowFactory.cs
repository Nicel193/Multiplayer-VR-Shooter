using System;
using System.Collections.Generic;
using Code.Runtime.Configs;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Runtime.UI.Windows
{
    public class WindowFactory : IWindowFactory
    {
        private DiContainer _diContainer;
        private Transform _playerWindowContainer;
        private Dictionary<Type, GameObject> _windowsPrefabs;

        public WindowFactory(DiContainer diContainer, WindowsConfig windowsConfig)
        {
            _diContainer = diContainer;
            _windowsPrefabs = windowsConfig.GetWindows();
        }

        public void Initialize(Transform playerWindowContainer)
        {
            _playerWindowContainer = playerWindowContainer;
        }
        
        public ICloseWindow CreateWindow<T>() where T : ICloseWindow
        {
            Type windowType = typeof(T);
            
            if(_windowsPrefabs.TryGetValue(windowType, out GameObject windowPrefab))
            {
                GameObject window = Object.Instantiate(windowPrefab, _playerWindowContainer);

                _diContainer.InjectGameObject(window);
                
                return window.GetComponent<ICloseWindow>();
            }

            throw new Exception($"There is no such window {windowType}");
        }
    }
}