using System.Net.Sockets;

namespace Rdp.Terminal.Core.Server.Models.Models
{
    /// <summary>
    ///     Servaer state class
    /// </summary>
    class ServerState
    {
        public const int BufferSize = 4096;

        public Socket WorkSocket { get; set; }

        public byte[] Buffer { get; set; }

        public long NeedToReceivedBytes { get; set; }
        
        public long ByteReceived { get; set; }
        
        public bool IsFirstBlockReceived { get; set; }

        /// <summary>
        ///     Server socket and buffer info
        /// </summary>
        public ServerState()
        {
            WorkSocket = null;
            Buffer = new byte[BufferSize];

            NeedToReceivedBytes = 0;
            ByteReceived = 0;
            IsFirstBlockReceived = true;
        }
    }
}
