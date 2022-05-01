namespace Rdp.Terminal.Core.Client.Data
{
    /// <summary>
    ///     Remote terminal interface
    /// </summary>
    public interface IRemoteTerminal
    {
        /// <summary>
        ///     Smart sizing
        /// </summary>
        bool SmartSizing { get; set; }

        /// <summary>
        ///     Connect to server
        /// </summary>
        ///
        /// <param name="connectionString">Connection string</param>
        /// <param name="groupName">Group name</param>
        /// <param name="password">Password</param>
        void Connect(string connectionString, string groupName, string password);
        
        /// <summary>
        ///     Start reverse connection listener
        /// </summary>
        ///
        /// <param name="connectionString">Connection string</param>
        /// <param name="groupName">Group name</param>
        /// <param name="password">Password</param>
        ///
        /// <returns>string</returns>
        string StartReverseConnectListener(string connectionString, string groupName, string password);

        /// <summary>
        ///     Disconect from server
        /// </summary>
        void Disconnect();
    }
}