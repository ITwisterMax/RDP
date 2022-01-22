using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Rdp.Terminal.Core.Server.Models.Controls;

namespace Rdp.Terminal.Core.Server.Models.Models
{
    public class ServerTCP
    {
        // Размер буфера
        private const int BufferSize = 4096;
        // Максимальное количество клиентов
        private const int MaxConnections = 10;
        // Переменная управления состоянием потока
        private readonly ManualResetEvent allDone;
        private readonly ManualResetEvent sendDone;
        // Сокет сервера
        private Socket server;

        public ServerTCP()
        {
            // Установка начальных параметров
            allDone = new ManualResetEvent(false);
            sendDone = new ManualResetEvent(false);
        }

        public void Start(string ipString, int port)
        {
            try
            {
                // Генерация связки ip-адрес + порт
                var ipAddress = IPAddress.Parse(ipString);
                var endPoint = new IPEndPoint(ipAddress, port);

                // Создание нового сокета
                server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Связка сокета с ip-адрес + порт
                server.Bind(endPoint);

                // Указание максимального количества клиентов
                server.Listen(MaxConnections);

                while (true)
                {
                    // Блокировка работы потока
                    allDone.Reset();
                    // Установка запроса асинхронного приема
                    server.BeginAccept(new AsyncCallback(AcceptConnection), server);
                    // Завершение работы потока
                    allDone.WaitOne();
                }
            }
            catch
            {

            }
        }

        private void AcceptConnection(IAsyncResult asyncResult)
        {
            // Установка для потока в положение работы
            allDone.Set();

            // Завершение запроса асинхронного соединения
            var listener = (Socket)asyncResult.AsyncState;
            var handler = listener.EndAccept(asyncResult);

            var listenerState = new ServerState
            {
                WorkSocket = handler
            };
        }

        public void Send(string path)
        {
            // Получение информации
            var transmitted = new Transmitted(path);

            // Отправка информации
            SendDetails(transmitted);

            // Инициализаци параметров
            byte[] contentBuffer = new byte[BufferSize];
            long contentLength = transmitted.GetContentLength();
            long contentByteSent = 0;

            // Пока не переданы все байты
            while (contentByteSent != contentLength)
            {
                // Определяем очередной блок
                int numberOfBytesToSend = ((contentLength - contentByteSent) / BufferSize > 0) ?
                    BufferSize : (int)(contentLength - contentByteSent);

                // Читаем в буфер
                const int bufferOffset = 0;
                transmitted.ReadBytes(contentBuffer, bufferOffset,
                    numberOfBytesToSend, contentByteSent);

                // Отправляем блок данных
                server.BeginSend(contentBuffer, bufferOffset,
                    contentBuffer.Length, SocketFlags.None,
                    new AsyncCallback(SendCallback), server);

                // Обновляем информацию о переданных байтах
                contentByteSent += numberOfBytesToSend;
                Array.Clear(contentBuffer, bufferOffset, contentBuffer.Length);
            }
        }

        private void SendDetails(Transmitted transmitted)
        {
            // Инициализаци параметров
            byte[] details = transmitted.GetByteArrayDetails();
            byte[] detailsLength = BitConverter.GetBytes((long)details.Length);
            byte[] contentLength = BitConverter.GetBytes(transmitted.GetContentLength());
            byte[] detailsBuffer = new byte[detailsLength.Length
                + details.Length + contentLength.Length];

            // Копирование данных
            const int stertIndex = 0;
            detailsLength.CopyTo(detailsBuffer, stertIndex);
            details.CopyTo(detailsBuffer, detailsLength.Length);
            contentLength.CopyTo(detailsBuffer,
                detailsLength.Length + details.Length);

            // Отправка информации
            const int offset = 0;
            server.BeginSend(detailsBuffer, offset,
                detailsBuffer.Length, SocketFlags.None,
                new AsyncCallback(SendCallback), server);
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Завершение потока передачи
                var sender = (Socket)asyncResult.AsyncState;
                int bytesSent = sender.EndSend(asyncResult);
                sendDone.Set();
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
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            catch
            {

            }
        }
    }
}
