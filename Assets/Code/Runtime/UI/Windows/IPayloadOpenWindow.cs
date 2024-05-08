namespace Code.Runtime.UI.Windows
{
    public interface IPayloadOpenWindow<T> : ICloseWindow
    {
        void Open(T payload);
    }
}