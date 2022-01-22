namespace AxRDPCOMAPILib
{
    // Вызывается при отключении пользователя
    internal class _IRDPSessionEvents_OnAttendeeDisconnectedEvent
    {
        public object pDisconnectInfo;

        public _IRDPSessionEvents_OnAttendeeDisconnectedEvent(object pDisconnectInfo)
        {
            this.pDisconnectInfo = pDisconnectInfo;
        }
    }
}