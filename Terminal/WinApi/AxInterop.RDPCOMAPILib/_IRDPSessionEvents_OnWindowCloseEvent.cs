namespace AxRDPCOMAPILib
{
    // Вызывается при закрытии окна
    internal class _IRDPSessionEvents_OnWindowCloseEvent
    {
        public object pWindow;

        public _IRDPSessionEvents_OnWindowCloseEvent(object pWindow)
        {
            this.pWindow = pWindow;
        }
    }
}