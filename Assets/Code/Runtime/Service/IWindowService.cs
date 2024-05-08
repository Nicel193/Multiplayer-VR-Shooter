using Code.Runtime.UI.Windows;

namespace Code.Runtime.Service
{
    public interface IWindowService
    {
        void OpenWindow<T>() where T : IOpenWindow;
        void OpenPayloadWindow<T, TPayload>(TPayload payload) where T : IPayloadOpenWindow<TPayload>;
        void Close();
    }
}