namespace AxRDPCOMAPILib
{
    // Вызывается при обновлении окна
    internal class _IRDPSessionEvents_OnWindowUpdateEvent
    {
        public object pWindow;

        public _IRDPSessionEvents_OnWindowUpdateEvent(object pWindow)
        {
            this.pWindow = pWindow;
        }
    }
}