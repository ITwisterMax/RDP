using System;
using RDPCOMAPILib;

namespace Rdp.Terminal.Core.Server.Models
{
    // Rdp сервер
    public class RdpSessionServer : IDisposable
    {
        // Rdp сессия
        private readonly RDPSession _rdpSession;
        
        // Конструктор
        public RdpSessionServer()
        {
            // Создание экземпляра RDP сессии с глубиной цвета 8
            _rdpSession = new RDPSession { colordepth = 8 };
            // Уровень доступа пользователя
            _rdpSession.add_OnAttendeeConnected(OnAttendeeConnected);
        }
        
        // Фильтр разрешенных окон
        public bool ApplicationFilterEnabled
        {
            get
            {
                return _rdpSession.ApplicationFilter.Enabled;
            }
            set
            {
                _rdpSession.ApplicationFilter.Enabled = value;
            }
        }

        // Список разрешенных окон
        public RDPSRAPIApplicationList ApplicationList
        {
            get
            {
                return _rdpSession.ApplicationFilter.Applications;
            }
        }

        // Открытие сервера
        public void Open()
        {
            _rdpSession.Open();
        }

        // Закрытие сервера
        public void Close()
        {
            _rdpSession.Close();
        }

        // Остановка сервера
        public void Pause()
        {
            _rdpSession.Pause();
        }

        // Возобновление сервера
        public void Resume()
        {
            _rdpSession.Resume();
        }

        // Соединение с клиентом
        public void ConnectToClient(string connectionString)
        {
            _rdpSession.ConnectToClient(connectionString);
        }

        // Генерация набора параметров для подключения
        public string CreateInvitation(string groupName, string password)
        {
            var invitation = _rdpSession.Invitations.CreateInvitation(null, groupName, password, 1);
            return invitation.ConnectionString;
        }

        // Закрытие сервера
        public void Dispose()
        {
            try
            {
                if (_rdpSession != null)
                {
                    foreach (IRDPSRAPIAttendee attendees in _rdpSession.Attendees)
                    {
                        attendees.TerminateConnection();
                    }

                    _rdpSession.Close();
                }
            }
            catch
            {
            }
        }
        
        // Уровень доступа пользователя
        private void OnAttendeeConnected(object pAttendee)
        {
            var attendee = (IRDPSRAPIAttendee)pAttendee;
            attendee.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE;
        }
    }
}