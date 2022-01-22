using System;
using System.Windows;
using Rdp.Demonstration.PropertiesAndCommands;
using Rdp.Demonstration.Views;
using Rdp.Terminal.Core.Client.Models;
using Rdp.Terminal.Core.Server.Models;
using Rdp.Terminal.Core.WinApi;
using RDPCOMAPILib;

namespace Rdp.Demonstration.ViewModels
{
    // Логика работы главной формы
    public class MainWindowViewModel : NotificationObject
    {
        // Набор параметров для подключения к серверу
        private string _serverConnectionText;
        // Строка с событиями (для терминала / сервера)
        private string _terminalEventText;
        // Выбранные действия
        private bool _actionChoosen = false;

        // Конструктор
        public MainWindowViewModel()
        {
            // Создание экземпляра RDP менеджера с автоматической масштабируемостью экрана
            RdpManager = new RdpManager() { SmartSizing = true };

            // Обработка прерывания сеансов трансляции
            RdpManager.OnConnectionTerminated += (reason, info) => SessionTerminated();
            RdpManager.OnGraphicsStreamPaused += (sender, args) => SessionTerminated();
            RdpManager.OnAttendeeDisconnected += info => SessionTerminated();

            // Определение логики работы основных команд
            SingleStartCommand = new DelegateCommand(SingleStart, o => !_actionChoosen);
            ConnectCommand = new DelegateCommand(Connect);
            ServerStartCommand = new DelegateCommand(ServerStart, o => !_actionChoosen);
            CopyCommand = new DelegateCommand(Copy);
        }

        // Строка для набора параметров подключения (клиент)
        public string ConnectionText { get; set; }

        // Строка с набором параметров подключения (сервер)
        public string ServerConnectionText
        {
            get
            {
                return _serverConnectionText;
            }
            set
            {
                _serverConnectionText = value;
                RaisePropertyChanged(() => ServerConnectionText);
            }
        }

        // RDP менеджер
        public RdpManager RdpManager { get; set; }

        // Команда старта сервера (трансляция всего экрана)
        public DelegateCommand ServerStartCommand { get; private set; }

        // Команда копирования набора параметров для подключения
        public DelegateCommand CopyCommand { get; private set; }

        // Команда старта сервера (трансляция одного окна)
        public DelegateCommand SingleStartCommand { get; private set; }

        // Команда подключения к серверу
        public DelegateCommand ConnectCommand { get; private set; }

        // Строка с событиями (для терминала / сервера)
        public string TerminalEventText
        {
            get
            {
                return _terminalEventText;
            }
            set
            {
                _terminalEventText = value;
                RaisePropertyChanged(() => TerminalEventText);
            }
        }

        // Имя группы
        private string GroupName
        {
            get
            {
                return Environment.UserName;
            }
        }

        // Пароль
        private string Password
        {
            get
            {
                return "58vabaha";
            }
        }

        // Команда старта сервера (трансляция одного окна)
        private void SingleStart(object obj)
        {
            // Обработка на случай неподходящей ОС
            if (!SupportUtils.CheckOperationSytem())
            {
                UnsupportingVersion();
                return;
            }

            // Инициализация RDP сервера
            var server = new RdpSessionServer();
            server.Open();

            // Получение полного имени приложения
            var executableName = GetApplicationName(AppDomain.CurrentDomain.FriendlyName);
            
            // Фильтр разрешенных окон
            server.ApplicationFilterEnabled = true;
            // Находим нужное окно из списка
            foreach (RDPSRAPIApplication application in server.ApplicationList)
            {
                application.Shared = GetApplicationName(application.Name) == executableName;
            }

            // Генерация строки набора параметров для подключения
            ServerConnectionText = server.CreateInvitation(GroupName, Password);

            // Старт сервера
            ServerStarted();
        }

        // Получение полного имени приложения
        private string GetApplicationName(string fileName)
        {
            const string Executable = ".exe";
            return fileName.EndsWith(Executable) ? fileName.Substring(0, fileName.Length - Executable.Length) : fileName;
        }

        // Ошибка: неподходящая ОС
        private void UnsupportingVersion()
        {
            MessageBox.Show("Support from Windows Vista only");
        }

        // Ошибка: прекращение сессии
        private void SessionTerminated()
        {
            MessageBox.Show("Session terminated");
        }

        // Команда копирования набора параметров для подключения
        private void Copy(object obj)
        {
            try
            {
                Clipboard.SetText(ServerConnectionText);
            }
            catch
            {

            }
        }

        // Команда старта сервера (трансляция всего экрана)
        private void ServerStart(object obj)
        {
            // Обработка на случай неподходящей ОС
            if (!SupportUtils.CheckOperationSytem())
            {
                UnsupportingVersion();
                return;
            }

            // Инициализация RDP сервера
            var server = new RdpSessionServer();
            server.Open();

            // Генерация строки набора параметров для подключения
            ServerConnectionText = server.CreateInvitation(GroupName, Password);

            // Старт сервера
            ServerStarted();
        }

        // Старт сервера
        private void ServerStarted()
        {
            _actionChoosen = true;
            ServerStartCommand.RaiseCanExecuteChanged();
            SingleStartCommand.RaiseCanExecuteChanged();
        }

        // Команда подключения к серверу
        private void Connect(object obj)
        {
            RdpManager.Connect(ConnectionText, GroupName, Password);
        }
    }
}