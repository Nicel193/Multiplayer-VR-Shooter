using UnityEngine;

namespace Code.Runtime.UI.Windows
{
    public interface IWindowFactory
    {
        ICloseWindow CreateWindow<T>() where T : ICloseWindow;
        void Initialize(Transform playerWindowContainer);
    }
}