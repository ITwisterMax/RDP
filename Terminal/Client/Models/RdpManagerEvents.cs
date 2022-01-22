using System;
using RDPCOMAPILib;

namespace Rdp.Terminal.Core.Client.Models
{
    // Реализация интерфеса при возникновении событий
    public partial class RdpManager
    {
        // Вызывается при закрытии приложения
        public event _IRDPSessionEvents_OnApplicationCloseEventHandler OnApplicationClose;

        // Вызывается при открытии приложения
        public event _IRDPSessionEvents_OnApplicationOpenEventHandler OnApplicationOpen;

        // Вызывается при обновлении приложения
        public event _IRDPSessionEvents_OnApplicationUpdateEventHandler OnApplicationUpdate;

        // Вызывается при подключении нового пользователя
        public event _IRDPSessionEvents_OnAttendeeConnectedEventHandler OnAttendeeConnected;

        // Вызывается при отключении пользователя
        public event _IRDPSessionEvents_OnAttendeeDisconnectedEventHandler OnAttendeeDisconnected;

        // Вызывается при обновлении сессии
        public event _IRDPSessionEvents_OnAttendeeUpdateEventHandler OnAttendeeUpdate;

        // Вызывается при приеме данных
        public event _IRDPSessionEvents_OnChannelDataReceivedEventHandler OnChannelDataReceived;

        // Вызывается при отправке данных
        public event _IRDPSessionEvents_OnChannelDataSentEventHandler OnChannelDataSent;

        // Вызывается при аутентификации пользователя
        public event EventHandler OnConnectionAuthenticated;

        // Вызывается при установке соединения с сервером
        public event EventHandler OnConnectionEstablished;

        // Вызывается при ошибке установления соединения с сервером
        public event EventHandler OnConnectionFailed;

        // Вызывается при закрытии соединениея с сервером
        public event _IRDPSessionEvents_OnConnectionTerminatedEventHandler OnConnectionTerminated;

        // Вызывается при изменении уровня доступа пользователя
        public event _IRDPSessionEvents_OnControlLevelChangeRequestEventHandler OnControlLevelChangeRequest;

        // Вызывается при обработке ошибок сессии
        public event _IRDPSessionEvents_OnErrorEventHandler OnError;

        // Вызывается при потере фокуса
        public event _IRDPSessionEvents_OnFocusReleasedEventHandler OnFocusReleased;

        // Вызывается при остановке графического потока
        public event EventHandler OnGraphicsStreamPaused;

        // Вызывается при возобновлении графического потока
        public event EventHandler OnGraphicsStreamResumed;

        // Вызывается при изменении настроек рабочего стола
        public event _IRDPSessionEvents_OnSharedDesktopSettingsChangedEventHandler OnSharedDesktopSettingsChanged;

        // Вызывается при изменении размеров окна
        public event _IRDPSessionEvents_OnSharedRectChangedEventHandler OnSharedRectChanged;

        // Вызывается при закрытии окна
        public event _IRDPSessionEvents_OnWindowCloseEventHandler OnWindowClose;

        // Вызывается при открытии окна
        public event _IRDPSessionEvents_OnWindowOpenEventHandler OnWindowOpen;

        // Вызывается при обновлении окна
        public event _IRDPSessionEvents_OnWindowUpdateEventHandler OnWindowUpdate;

        // Все обрабатываемые сервером события
        private void Subsribe()
        {
            _manager.RdpViewer.OnApplicationClose += delegate(object application) { OnApplicationClose?.Invoke(application); };
            _manager.RdpViewer.OnApplicationOpen += delegate(object application) { OnApplicationOpen?.Invoke(application); };
            _manager.RdpViewer.OnApplicationUpdate += delegate(object application) { OnApplicationUpdate?.Invoke(application); };
            _manager.RdpViewer.OnAttendeeConnected += delegate(object attendee) { OnAttendeeConnected?.Invoke(attendee); };
            _manager.RdpViewer.OnAttendeeDisconnected += delegate(object info) { OnAttendeeDisconnected?.Invoke(info); };
            _manager.RdpViewer.OnAttendeeUpdate += delegate(object attendee) { OnAttendeeUpdate?.Invoke(attendee); };
            _manager.RdpViewer.OnChannelDataReceived += delegate(object channel, int id, string data) { OnChannelDataReceived?.Invoke(channel, id, data); };
            _manager.RdpViewer.OnChannelDataSent += delegate(object channel, int id, int sent) { OnChannelDataSent?.Invoke(channel, id, sent); };
            _manager.RdpViewer.OnConnectionAuthenticated += delegate(object sender, EventArgs args) { OnConnectionAuthenticated?.Invoke(sender, args); };
            _manager.RdpViewer.OnConnectionEstablished += delegate(object sender, EventArgs args) { OnConnectionEstablished?.Invoke(sender, args); };
            _manager.RdpViewer.OnConnectionFailed += delegate(object sender, EventArgs args) { OnConnectionFailed?.Invoke(sender, args); };
            _manager.RdpViewer.OnConnectionTerminated += delegate(int reason, int info) { OnConnectionTerminated?.Invoke(reason, info); };
            _manager.RdpViewer.OnControlLevelChangeRequest += delegate(object attendee, CTRL_LEVEL level) { OnControlLevelChangeRequest?.Invoke(attendee, level); };
            _manager.RdpViewer.OnError += delegate(object info) { OnError?.Invoke(info); };
            _manager.RdpViewer.OnFocusReleased += delegate(int direction) { OnFocusReleased?.Invoke(direction); };
            _manager.RdpViewer.OnGraphicsStreamPaused += delegate(object sender, EventArgs args) { OnGraphicsStreamPaused?.Invoke(sender, args); };
            _manager.RdpViewer.OnGraphicsStreamResumed += delegate(object sender, EventArgs args) { OnGraphicsStreamResumed?.Invoke(sender, args); };
            _manager.RdpViewer.OnSharedDesktopSettingsChanged += delegate(int width, int height, int colordepth) { OnSharedDesktopSettingsChanged?.Invoke(width, height, colordepth); };
            _manager.RdpViewer.OnSharedRectChanged += delegate(int left, int top, int right, int bottom) { OnSharedRectChanged?.Invoke(left, top, right, bottom); };
            _manager.RdpViewer.OnWindowClose += delegate(object window) { OnWindowClose?.Invoke(window); };
            _manager.RdpViewer.OnWindowOpen += delegate(object window) { OnWindowOpen?.Invoke(window); };
            _manager.RdpViewer.OnWindowUpdate += delegate(object window) { OnWindowUpdate?.Invoke(window); };
        }
    }
}