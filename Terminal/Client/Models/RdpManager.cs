using System;
using Rdp.Terminal.Core.Client.Data;

namespace Rdp.Terminal.Core.Client.Models
{
    /// <summary>
    ///     RDP manager
    /// </summary>
    public partial class RdpManager : IRemoteTerminal
    {
        private RemoteTeminalManager _manager;

        public bool SmartSizing { get; set; }

        /// <summary>
        ///     Start connection
        /// </summary>
        ///
        /// <param name="connectionString">Connection string</param>
        /// <param name="groupName">Group name</param>
        /// <param name="password">Password</param>
        public void Connect(string connectionString, string groupName, string password)
        {
            CheckValid();

            _manager.SmartSizing = SmartSizing;
            _manager.Connect(connectionString, groupName, password);
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
            CheckValid();
            _manager.SmartSizing = SmartSizing;

            return _manager.StartReverseConnectListener(connectionString, groupName, password);
        }

        /// <summary>
        ///     Stop connection
        /// </summary>
        public void Disconnect()
        {
            CheckValid();
            _manager.Disconnect();
        }

        /// <summary>
        ///     Attach clients
        /// </summary>
        ///
        /// <param name="manager">Manager</param>
        internal void Attach(RemoteTeminalManager manager)
        {
            Detach();

            _manager = manager;

            Subsribe();
        }

        /// <summary>
        ///     Detach client
        /// </summary>
        internal void Detach()
        {
            _manager = null;
        }

        /// <summary>
        ///     Check current manager
        /// </summary>
        private void CheckValid()
        {
            if (_manager == null)
            {
                throw new NotSupportedException("RdpManager still not attached.");
            }
        }
    }
}