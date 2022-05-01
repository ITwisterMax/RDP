using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Rdp.Terminal.Core.Client.Models
{
    /// <summary>
    ///     TCP client class
    /// </summary>
    public class ClientTCP
    {
        private readonly ManualResetEvent connectDone;

        private Socket client;

        /// <summary>
        ///     Initialize properties
        /// </summary>
        public ClientTCP()
        {
            connectDone = new ManualResetEvent(false);
        }

        /// <summary>
        ///     Start connection
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

                client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                client.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
            }
            catch
            {

            }
        }

        /// <summary>
        ///     Connection callback
        /// </summary>
        ///
        /// <param name="asyncResult">Async resut parameter</param>
        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                var res = (Socket)asyncResult.AsyncState;
                res.EndConnect(asyncResult);

                connectDone.Set();
            }
            catch
            {

            }
        }

        /// <summary>
        ///     Stop connection
        /// </summary>
        public void Stop()
        {
            try
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch
            {

            }
        }
    }
}
