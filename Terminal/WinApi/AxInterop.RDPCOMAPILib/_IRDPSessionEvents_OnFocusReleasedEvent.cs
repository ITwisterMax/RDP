namespace AxRDPCOMAPILib
{
    // Вызывается при потере фокуса
    internal class _IRDPSessionEvents_OnFocusReleasedEvent
    {
        public int iDirection;

        public _IRDPSessionEvents_OnFocusReleasedEvent(int iDirection)
        {
            this.iDirection = iDirection;
        }
    }
}