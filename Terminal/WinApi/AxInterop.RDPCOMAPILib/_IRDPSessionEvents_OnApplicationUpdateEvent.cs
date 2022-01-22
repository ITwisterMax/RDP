namespace AxRDPCOMAPILib
{
    // Вызывается при обновлении приложения
    internal class _IRDPSessionEvents_OnApplicationUpdateEvent
    {
        public object pApplication;

        public _IRDPSessionEvents_OnApplicationUpdateEvent(object pApplication)
        {
            this.pApplication = pApplication;
        }
    }
}