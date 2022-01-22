using System;
using Rdp.Terminal.Core.Client.Data;

namespace Rdp.Terminal.Core.Client.Models
{
    // RDP менеджер
    public partial class RdpManager : IRemoteTerminal
    {
        private RemoteTeminalManager _manager;

        // Масштабирование транслируемого экрана
        public bool SmartSizing { get; set; }

        // Начало соединения с сервером
        public void Connect(string connectionString, string groupName, string password)
        {
            CheckValid();
            _manager.SmartSizing = SmartSizing;
            _manager.Connect(connectionString, groupName, password);
        }

        // Инициирует прослушиватель для приема обратных подключений
        public string StartReverseConnectListener(string connectionString, string groupName, string passowrd)
        {
            CheckValid();
            _manager.SmartSizing = SmartSizing;
            return _manager.StartReverseConnectListener(connectionString, groupName, passowrd);
        }

        // Прекращение соединения с сервером
        public void Disconnect()
        {
            CheckValid();
            _manager.Disconnect();
        }

        // Присоединение пользователей к серверу
        internal void Attach(RemoteTeminalManager manager)
        {
            Detach();
            _manager = manager;
            Subsribe();
        }

        // Отсоединение пользователей от сервера 
        internal void Detach()
        {
            _manager = null;
        }

        // Проверка экземпляра текущего RDP менеджера
        private void CheckValid()
        {
            if (_manager == null)
            {
                throw new NotSupportedException("RdpManager still not attached");
            }
        }
    }
}