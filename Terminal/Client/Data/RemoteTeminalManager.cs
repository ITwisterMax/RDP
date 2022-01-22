using System;
using AxRDPCOMAPILib;
using Rdp.Terminal.Core.Client.Controls;

namespace Rdp.Terminal.Core.Client.Data
{
    // Логика работы соединения в обе стороны
    internal class RemoteTeminalManager : IRemoteTerminal
    {
        // Взаимодействие клиент-сервер
        private readonly AxRDPViewer _axRdpViewer;

        // Конструктор
        public RemoteTeminalManager(AxRDPViewer axRdpViewer)
        {
            if (axRdpViewer == null)
            {
                throw new ArgumentNullException(nameof(axRdpViewer));
            }

            _axRdpViewer = axRdpViewer;
        }

        // Масштабируемость
        public bool SmartSizing
        {
            get
            {
                return _axRdpViewer.SmartSizing;
            }
            set
            {
                _axRdpViewer.SmartSizing = value;
            }
        }

        // Ссылка на экземпляр
        internal AxRDPViewer RdpViewer
        {
            get
            {
                return _axRdpViewer;
            }
        }

        // Начало соединения с сервером
        public void Connect(string connectionString, string groupName, string passowrd)
        {
            _axRdpViewer.Connect(connectionString, groupName, passowrd);
        }

        // Инициирует прослушиватель для приема обратных подключений
        public string StartReverseConnectListener(string connectionString, string groupName, string passowrd)
        {
            return _axRdpViewer.StartReverseConnectListener(connectionString, groupName, passowrd);
        }

        // Прекращение соединения с сервером
        public void Disconnect()
        {
            _axRdpViewer.Disconnect();
        }
    }
}