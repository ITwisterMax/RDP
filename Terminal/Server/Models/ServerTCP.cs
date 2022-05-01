using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Rdp.Terminal.Core.Server.Models.Controls;

namespace Rdp.Terminal.Core.Server.Models.Models
{
    /// <summary>
    ///     TCP server class
    /// </summary>
    public class ServerTCP
    {
        private const int BufferSize = 4096;
        
        private const int MaxConnections = 1;
        
        private readonly ManualResetEvent allDone;
        
        private readonly ManualResetEvent sendDone;
        
        private Socket server;

        /// <summary>
        ///     Initialize properties
        /// </summary>
        public ServerTCP()
        {
            allDone = new ManualResetEvent(false);
            sendDone = new ManualResetEvent(false);
        }

        /// <summary>
        ///     Start TCP server
        /// </summary>
        ///
        /// <param name="ipString">IP v4 string</param>
        /// <param name="port">Port</param>
        public void Start(string ipString, int port)
        {
            try
            {
                var ipAddress = IPAddress.Parse(ipString);
                var endPoint = new IPEndPoint(ipAddress, port);

                server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                server.Bind(endPoint);
                server.Listen(MaxConnections);

                while (true)
                {
                    allDone.Reset();

                    server.BeginAccept(new AsyncCallback(AcceptConnection), server);
                    allDone.WaitOne();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        ///     Accept connection
        /// </summary>
        ///
        /// <param name="asyncResult">Async result parameter</param>
        private void AcceptConnection(IAsyncResult asyncResult)
        {
            allDone.Set();

            var listener = (Socket)asyncResult.AsyncState;
            var handler = listener.EndAccept(asyncResult);

            var listenerState = new ServerState
            {
                WorkSocket = handler
            };
        }

        /// <summary>
        ///     Send data
        /// </summary>
        ///
        /// <param name="path">Path</param>
        public void Send(string path)
        {
            var transmitted = new Transmitted(path);

            SendDetails(transmitted);

            byte[] contentBuffer = new byte[BufferSize];
            long contentLength = transmitted.GetContentLength();
            long contentByteSent = 0;

            while (contentByteSent != contentLength)
            {
                int numberOfBytesToSend = ((contentLength - contentByteSent) / BufferSize > 0) ?
                    BufferSize : (int)(contentLength - contentByteSent);

                const int bufferOffset = 0;
                transmitted.ReadBytes(contentBuffer, bufferOffset,
                    numberOfBytesToSend, contentByteSent);

                server.BeginSend(contentBuffer, bufferOffset,
                    contentBuffer.Length, SocketFlags.None,
                    new AsyncCallback(SendCallback), server);

                contentByteSent += numberOfBytesToSend;
                Array.Clear(contentBuffer, bufferOffset, contentBuffer.Length);
            }
        }

        /// <summary>
        ///     Send server details
        /// </summary>
        ///
        /// <param name="transmitted">Transmitted</param>
        private void SendDetails(Transmitted transmitted)
        {
            byte[] details = transmitted.GetByteArrayDetails();
            byte[] detailsLength = BitConverter.GetBytes((long)details.Length);
            byte[] contentLength = BitConverter.GetBytes(transmitted.GetContentLength());
            byte[] detailsBuffer = new byte[detailsLength.Length
                + details.Length + contentLength.Length];

            const int stertIndex = 0;
            detailsLength.CopyTo(detailsBuffer, stertIndex);
            details.CopyTo(detailsBuffer, detailsLength.Length);
            contentLength.CopyTo(detailsBuffer,
                detailsLength.Length + details.Length);

            const int offset = 0;
            server.BeginSend(detailsBuffer, offset,
                detailsBuffer.Length, SocketFlags.None,
                new AsyncCallback(SendCallback), server);
        }

        /// <summary>
        ///     Send data callback
        /// </summary>
        ///
        /// <param name="asyncResult">Async result parameter</param>
        private void SendCallback(IAsyncResult asyncResult)
        {
            try
            {
                var sender = (Socket)asyncResult.AsyncState;
                int bytesSent = sender.EndSend(asyncResult);
                sendDone.Set();
            }
            catch
            {

            }
        }

        /// <summary>
        ///     Stop TCP server
        /// </summary>
        public void Stop()
        {
            try
            {
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            catch
            {

            }
        }
    }
}
