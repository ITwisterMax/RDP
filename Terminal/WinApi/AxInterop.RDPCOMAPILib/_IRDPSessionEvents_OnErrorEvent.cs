namespace AxRDPCOMAPILib
{
    // Вызывается при обработке ошибок сессии
    internal class _IRDPSessionEvents_OnErrorEvent
    {
        public object errorInfo;

        public _IRDPSessionEvents_OnErrorEvent(object errorInfo)
        {
            this.errorInfo = errorInfo;
        }
    }
}