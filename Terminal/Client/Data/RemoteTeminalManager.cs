using System;
using AxRDPCOMAPILib;
using Rdp.Terminal.Core.Client.Controls;

namespace Rdp.Terminal.Core.Client.Data
{
    /// <summary>
    ///     Remote terminl manager class
    /// </summary>
    internal class RemoteTeminalManager : IRemoteTerminal
    {
        private readonly AxRDPViewer _axRdpViewer;

        /// <summary>
        ///     Default constructor
        /// </summary>
        ///
        /// <param name="axRdpViewer">RDP viewer</param>
        public RemoteTeminalManager(AxRDPViewer axRdpViewer)
        {
            if (axRdpViewer == null)
            {
                throw new ArgumentNullException(nameof(axRdpViewer));
            }

            _axRdpViewer = axRdpViewer;
        }

        /// <summary>
        ///     Smart sizing
        /// </summary>
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

        /// <summary>
        ///     RDP viewer
        /// </summary>
        internal AxRDPViewer RdpViewer
        {
            get
            {
                return _axRdpViewer;
            }
        }

        /// <summary>
        ///     Connect to server
        /// </summary>
        ///
        /// <param name="connectionString">Connection string</param>
        /// <param name="groupName">Group name</param>
        /// <param name="password">Password</param>
        public void Connect(string connectionString, string groupName, string password)
        {
            _axRdpViewer.Connect(connectionString, groupName, password);
        }

        /// <summary>
        ///     Start reverse connection listener
        /// </summary>
        ///
        /// <param name="connectionString">Connection string</param>
        /// <param name="groupName">Group name</param>
        /// <param name="password">Password</param>
        ///
        /// <returns>string</returns>
        public string StartReverseConnectListener(string connectionString, string groupName, string password)
        {
            return _axRdpViewer.StartReverseConnectListener(connectionString, groupName, password);
        }

        /// <summary>
        ///     Disconect from server
        /// </summary>
        public void Disconnect()
        {
            _axRdpViewer.Disconnect();
        }
    }
}