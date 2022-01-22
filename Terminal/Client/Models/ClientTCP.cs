using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Rdp.Terminal.Core.Client.Models
{
    public class ClientTCP
    {
        // Состояние потоков установки соединения и передачи файла
        private readonly ManualResetEvent connectDone;
        // Клиентский сокет
        private Socket client;

        public ClientTCP()
        {
            // Задание начального состояния потоков установки соединения и передачи файла
            connectDone = new ManualResetEvent(false);
        }

        public void Start(string ipString, int port)
        {
            try
            {
                // Генерация связки ip-адрес + порт
                var ipAddress = IPAddress.Parse(ipString);
                var endPoint = new IPEndPoint(ipAddress, port);

                // Создание нового сокета
                client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Установка запроса асинхронного соединения
                client.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), client);
                // Завершение работы потока
                connectDone.WaitOne();
            }
            catch
            {

            }
        }

        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Завершение запроса асинхронного соединения
                var res = (Socket)asyncResult.AsyncState;
                res.EndConnect(asyncResult);
                // Продолжение работы потока
                connectDone.Set();
            }
            catch
            {

            }
        }

        public void Stop()
        {
            try
            {
                // Закрытие соединения в обе стороны
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch
            {

            }
        }
    }
}
