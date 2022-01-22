using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RDPCOMAPILib;

namespace AxRDPCOMAPILib
{
    [Clsid("{32be5ed2-5c86-480f-a914-0ff8885a1b3f}")]
    [DesignTimeVisible(true)]
    internal class AxRDPViewer : AxHost
    {
        private IRDPSRAPIViewer ocx;
        private AxRDPViewerEventMulticaster eventMulticaster;
        private AxHost.ConnectionPointCookie cookie;

        // Конструктор
        public AxRDPViewer() : base("32be5ed2-5c86-480f-a914-0ff8885a1b3f")
        {
        }

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

        // Фильтрация отображаемых окон
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DispId(215)]
        public virtual RDPSRAPIApplicationFilter ApplicationFilter
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException(
                        "ApplicationFilter",
                        AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.ApplicationFilter;
            }
        }

        // Присоединенные пользователи
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DispId(203)]
        public virtual RDPSRAPIAttendeeManager Attendees
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("Attendees", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.Attendees;
            }
        }

        // Действия при отключении от сервера
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DispId(237)]
        public virtual string DisconnectedText
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException(
                        "DisconnectedText",
                        AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.DisconnectedText;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException(
                        "DisconnectedText",
                        AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.DisconnectedText = value;
            }
        }

        // Набор параметров для установки соединения
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DispId(204)]
        public virtual RDPSRAPIInvitationManager Invitations
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("Invitations", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.Invitations;
            }
        }

        // Свойства
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DispId(202)]
        public virtual RDPSRAPISessionProperties Properties
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("Properties", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.Properties;
            }
        }

        // Автоматическое масштабирование
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DispId(238)]
        public virtual bool SmartSizing
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("SmartSizing", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.SmartSizing;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("SmartSizing", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.SmartSizing = value;
            }
        }

        // Менеджер
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DispId(206)]
        public virtual IRDPSRAPIVirtualChannelManager VirtualChannelManager
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException(
                        "VirtualChannelManager",
                        AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.VirtualChannelManager;
            }
        }

        // Соединение пользователся с сервером
        public virtual void Connect(string bstrConnectionString, string bstrName, string bstrPassword)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("Connect", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            try
            {
                this.ocx.Connect(bstrConnectionString, bstrName, bstrPassword);
            }
            catch
            {

            }
        }

        // Отключение от сервера
        public virtual void Disconnect()
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("Disconnect", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.Disconnect();
        }

        // Изменение глубины цвета
        public virtual void RequestColorDepthChange(int bpp)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException(
                    "RequestColorDepthChange",
                    AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.RequestColorDepthChange(bpp);
        }

        // Уровень доступа пользователя
        public virtual void RequestControl(CTRL_LEVEL ctrlLevel)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("RequestControl", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.RequestControl(ctrlLevel);
        }

        // Начало обратного прослушивания
        public virtual string StartReverseConnectListener(
            string bstrConnectionString,
            string bstrUserName,
            string bstrPassword)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException(
                    "StartReverseConnectListener",
                    AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            return this.ocx.StartReverseConnectListener(bstrConnectionString, bstrUserName, bstrPassword);
        }

        // Изменения при закрытии приложения
        internal void RaiseOnOnApplicationClose(object sender, _IRDPSessionEvents_OnApplicationCloseEvent e)
        {
            if (this.OnApplicationClose != null)
            {
                this.OnApplicationClose(e);
            }
        }

        // Изменения при открытии приложения
        internal void RaiseOnOnApplicationOpen(object sender, _IRDPSessionEvents_OnApplicationOpenEvent e)
        {
            if (this.OnApplicationOpen != null)
            {
                this.OnApplicationOpen(e);
            }
        }

        // Изменения при обновлении приложения
        internal void RaiseOnOnApplicationUpdate(object sender, _IRDPSessionEvents_OnApplicationUpdateEvent e)
        {
            if (this.OnApplicationUpdate != null)
            {
                this.OnApplicationUpdate(e);
            }
        }

        // Изменения при подключении нового пользователя
        internal void RaiseOnOnAttendeeConnected(object sender, _IRDPSessionEvents_OnAttendeeConnectedEvent e)
        {
            if (this.OnAttendeeConnected != null)
            {
                this.OnAttendeeConnected(e);
            }
        }

        // Изменения при отключении пользователя
        internal void RaiseOnOnAttendeeDisconnected(object sender, _IRDPSessionEvents_OnAttendeeDisconnectedEvent e)
        {
            if (this.OnAttendeeDisconnected != null)
            {
                this.OnAttendeeDisconnected(e);
            }
        }

        // Изменения при обновлении сессии
        internal void RaiseOnOnAttendeeUpdate(object sender, _IRDPSessionEvents_OnAttendeeUpdateEvent e)
        {
            if (this.OnAttendeeUpdate != null)
            {
                this.OnAttendeeUpdate(e);
            }
        }

        // Изменения при начале приема данных
        internal void RaiseOnOnChannelDataReceived(object sender, _IRDPSessionEvents_OnChannelDataReceivedEvent e)
        {
            if (this.OnChannelDataReceived != null)
            {
                this.OnChannelDataReceived(e.pChannel, e.lAttendeeId, e.bstrData);
            }
        }

        // Изменения при начале передачи данных
        internal void RaiseOnOnChannelDataSent(object sender, _IRDPSessionEvents_OnChannelDataSentEvent e)
        {
            if (this.OnChannelDataSent != null)
            {
                this.OnChannelDataSent(e.pChannel, e.lAttendeeId, e.bytesSent);
            }
        }

        // Изменения при аутентификации пользователя
        internal void RaiseOnOnConnectionAuthenticated(object sender, EventArgs e)
        {
            if (this.OnConnectionAuthenticated != null)
            {
                this.OnConnectionAuthenticated(sender, e);
            }
        }

        // Изменения при установке соединения с сервером
        internal void RaiseOnOnConnectionEstablished(object sender, EventArgs e)
        {
            if (this.OnConnectionEstablished != null)
            {
                this.OnConnectionEstablished(sender, e);
            }
        }

        // Изменения при ошибке установления соединения с сервером
        internal void RaiseOnOnConnectionFailed(object sender, EventArgs e)
        {
            if (this.OnConnectionFailed != null)
            {
                this.OnConnectionFailed(sender, e);
            }
        }

        // Изменения при закрытии соединениея с сервером
        internal void RaiseOnOnConnectionTerminated(object sender, _IRDPSessionEvents_OnConnectionTerminatedEvent e)
        {
            if (this.OnConnectionTerminated != null)
            {
                this.OnConnectionTerminated(e.discReason, e.extendedInfo);
            }
        }

        // Изменения при изменении уровня доступа пользователя
        internal void RaiseOnOnControlLevelChangeRequest(
            object sender,
            _IRDPSessionEvents_OnControlLevelChangeRequestEvent e)
        {
            if (this.OnControlLevelChangeRequest != null)
            {
                this.OnControlLevelChangeRequest(e.pAttendee, e.requestedLevel);
            }
        }

        // Изменения при обработке ошибок сессии
        internal void RaiseOnOnError(object sender, _IRDPSessionEvents_OnErrorEvent e)
        {
            if (this.OnError != null)
            {
                this.OnError(e);
            }
        }

        // Изменения при потере фокуса
        internal void RaiseOnOnFocusReleased(object sender, _IRDPSessionEvents_OnFocusReleasedEvent e)
        {
            if (this.OnFocusReleased != null)
            {
                this.OnFocusReleased(e.iDirection);
            }
        }

        // Изменения при остановке графического потока
        internal void RaiseOnOnGraphicsStreamPaused(object sender, EventArgs e)
        {
            if (this.OnGraphicsStreamPaused != null)
            {
                this.OnGraphicsStreamPaused(sender, e);
            }
        }

        // Изменения при возобновлении графического потока
        internal void RaiseOnOnGraphicsStreamResumed(object sender, EventArgs e)
        {
            if (this.OnGraphicsStreamResumed != null)
            {
                this.OnGraphicsStreamResumed(sender, e);
            }
        }

        // Изменения при обновлении настроек рабочего стола
        internal void RaiseOnOnSharedDesktopSettingsChanged(
            object sender,
            _IRDPSessionEvents_OnSharedDesktopSettingsChangedEvent e)
        {
            if (this.OnSharedDesktopSettingsChanged != null)
            {
                this.OnSharedDesktopSettingsChanged(e.width, e.height, e.colordepth);
            }
        }

        // Изменения при обновлении размеров окна
        internal void RaiseOnOnSharedRectChanged(object sender, _IRDPSessionEvents_OnSharedRectChangedEvent e)
        {
            if (this.OnSharedRectChanged != null)
            {
                this.OnSharedRectChanged(e.left, e.top, e.right, e.bottom);
            }
        }

        // Изменения при закрытии окна
        internal void RaiseOnOnWindowClose(object sender, _IRDPSessionEvents_OnWindowCloseEvent e)
        {
            if (this.OnWindowClose != null)
            {
                this.OnWindowClose(e.pWindow);
            }
        }

        // Изменения при открытии окна
        internal void RaiseOnOnWindowOpen(object sender, _IRDPSessionEvents_OnWindowOpenEvent e)
        {
            if (this.OnWindowOpen != null)
            {
                this.OnWindowOpen(e);
            }
        }

        // Изменения при обновлении окна
        internal void RaiseOnOnWindowUpdate(object sender, _IRDPSessionEvents_OnWindowUpdateEvent e)
        {
            if (this.OnWindowUpdate != null)
            {
                this.OnWindowUpdate(e);
            }
        }

        // Связь с ActiveX (способ трансляции)
        protected override void AttachInterfaces()
        {
            try
            {
                this.ocx = (IRDPSRAPIViewer)base.GetOcx();
            }
            catch
            {

            }
        }

        // Подготовка к прослушиванию событий
        protected override void CreateSink()
        {
            try
            {
                this.eventMulticaster = new AxRDPViewerEventMulticaster(this);
                this.cookie = new AxHost.ConnectionPointCookie(
                    this.ocx,
                    this.eventMulticaster,
                    typeof(_IRDPSessionEvents));
            }
            catch
            {

            }
        }

        // Прекращение прослушивания событий
        protected override void DetachSink()
        {
            try
            {
                this.cookie.Disconnect();
            }
            catch
            {

            }
        }
    }
}