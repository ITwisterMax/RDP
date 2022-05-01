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
    /// <summary>
    ///     Main WPF class
    /// </summary>
    public class MainWindowViewModel : NotificationObject
    {
        private string _serverConnectionText;

        private string _terminalEventText;

        private bool _actionChoosen = false;

        public RdpManager RdpManager { get; set; }

        public DelegateCommand ServerStartCommand { get; private set; }

        public DelegateCommand CopyCommand { get; private set; }

        public DelegateCommand SingleStartCommand { get; private set; }

        public DelegateCommand ConnectCommand { get; private set; }

        public string ConnectionText { get; set; }

        /// <summary>
        ///     Server connection string
        /// </summary>
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

        /// <summary>
        ///     Events string
        /// </summary>
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

        /// <summary>
        ///     Group name
        /// </summary>
        private string GroupName
        {
            get
            {
                return Environment.UserName;
            }
        }

        /// <summary>
        ///     Password
        /// </summary>
        private string Password
        {
            get
            {
                return "12345678";
            }
        }

        /// <summary>
        ///     Default constructor
        /// </summary>
        public MainWindowViewModel()
        {
            RdpManager = new RdpManager() { SmartSizing = true };

            RdpManager.OnConnectionTerminated += (reason, info) => SessionTerminated();
            RdpManager.OnGraphicsStreamPaused += (sender, args) => SessionTerminated();
            RdpManager.OnAttendeeDisconnected += info => SessionTerminated();

            SingleStartCommand = new DelegateCommand(SingleStart, o => !_actionChoosen);
            ConnectCommand = new DelegateCommand(Connect);
            ServerStartCommand = new DelegateCommand(ServerStart, o => !_actionChoosen);
            CopyCommand = new DelegateCommand(Copy);
        }

        /// <summary>
        ///     Current window translation
        /// </summary>
        ///
        /// <param name="obj">Object</param>
        private void SingleStart(object obj)
        {
            if (!SupportUtils.CheckOperationSytem())
            {
                UnsupportingVersion();
                return;
            }

            var server = new RdpSessionServer();
            server.Open();

            var executableName = GetApplicationName(AppDomain.CurrentDomain.FriendlyName);

            server.ApplicationFilterEnabled = true;
            foreach (RDPSRAPIApplication application in server.ApplicationList)
            {
                application.Shared = GetApplicationName(application.Name) == executableName;
            }

            ServerConnectionText = server.CreateInvitation(GroupName, Password);

            ServerStarted();
        }

        /// <summary>
        ///     Get application name
        /// </summary>
        ///
        /// <param name="fileName">File name</param>
        ///
        /// <returns>string</returns>
        private string GetApplicationName(string fileName)
        {
            const string Executable = ".exe";
            return fileName.EndsWith(Executable) ? fileName.Substring(0, fileName.Length - Executable.Length) : fileName;
        }

        /// <summary>
        ///     OS error
        /// </summary>
        private void UnsupportingVersion()
        {
            MessageBox.Show("Support from Windows Vista only.");
        }

        /// <summary>
        ///     Session was terminated
        /// </summary>
        private void SessionTerminated()
        {
            MessageBox.Show("Session terminated.");
        }

        /// <summary>
        ///     Copy connection parameters
        /// </summary>
        ///
        /// <param name="obj">Object</param>
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

        /// <summary>
        ///     All windows translation
        /// </summary>
        ///
        /// <param name="obj">Object</param>
        private void ServerStart(object obj)
        {
            if (!SupportUtils.CheckOperationSytem())
            {
                UnsupportingVersion();
                return;
            }

            var server = new RdpSessionServer();
            server.Open();

            ServerConnectionText = server.CreateInvitation(GroupName, Password);

            ServerStarted();
        }

        /// <summary>
        ///     Finish start server
        /// </summary>
        private void ServerStarted()
        {
            _actionChoosen = true;
            ServerStartCommand.RaiseCanExecuteChanged();
            SingleStartCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Connect to server
        /// </summary>
        ///
        /// <param name="obj">Object</param>
        private void Connect(object obj)
        {
            RdpManager.Connect(ConnectionText, GroupName, Password);
        }
    }
}