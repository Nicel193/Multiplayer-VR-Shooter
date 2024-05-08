using System;
using System.Collections.Generic;
using Code.Runtime.UI.Windows;

namespace Code.Runtime.Service
{
    public class WindowService : IWindowService
    {
        private readonly IWindowFactory _windowFactory;

        private Dictionary<Type, ICloseWindow> _createdWindows = new Dictionary<Type, ICloseWindow>();
        private ICloseWindow _currentWindow;

        public WindowService(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public void OpenWindow<T>() where T : IOpenWindow
        {
            Type windowType = typeof(T);

            if (!IsCreatedWindow(windowType, out _currentWindow))
            {
                _currentWindow = _windowFactory.CreateWindow<T>();
                _createdWindows.Add(windowType, _currentWindow);
            }
            
            ((IOpenWindow)_currentWindow).Open();
        }
        
        public void OpenPayloadWindow<T, TPayload>(TPayload payload) where T : IPayloadOpenWindow<TPayload>
        {
            Type windowType = typeof(T);

            if (!IsCreatedWindow(windowType, out _currentWindow))
            {
                _currentWindow = _windowFactory.CreateWindow<T>();
                _createdWindows.Add(windowType, _currentWindow);
            }
            
            ((IPayloadOpenWindow<TPayload>)_currentWindow).Open(payload);
        }


        public void Close()
        {
            if (_currentWindow == null) return;

            _currentWindow.Close();
            _currentWindow = null;
        }

        private bool IsCreatedWindow(Type windowType, out ICloseWindow window) =>
            _createdWindows.TryGetValue(windowType, out window);
    }
}