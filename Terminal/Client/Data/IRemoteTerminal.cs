namespace Rdp.Terminal.Core.Client.Data
{
    // Интерфейс RDP менеджера
    public interface IRemoteTerminal
    {
        // Масштабирование транслируемого экрана
        bool SmartSizing { get; set; }

        // Начало соединения с сервером
        void Connect(string connectionString, string groupName, string passowrd);
        
        // Инициирует прослушиватель для приема обратных подключений
        string StartReverseConnectListener(string connectionString, string groupName, string passowrd);

        // Прекращение соединения с сервером
        void Disconnect();
    }
}