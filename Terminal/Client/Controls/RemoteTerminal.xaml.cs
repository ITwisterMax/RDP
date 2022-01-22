using System.Windows;
using Rdp.Terminal.Core.Client.Data;
using Rdp.Terminal.Core.Client.Models;
namespace Rdp.Terminal.Core.Client.Controls
{
    // Логика работы RemoteTerminal.xaml
    public partial class RemoteTerminal
    {
        // Rdp менеджер свойств
        public static DependencyProperty RdpManagerProperty;
        internal readonly RemoteTeminalManager Manager;

        // Инициализация свойства зависимости
        static RemoteTerminal()
        {
            RemoteTeminalBehavior.InitializeDependencyProperties();
        }

        // Конструктор
        public RemoteTerminal()
        {
            DataContext = this;
            InitializeComponent();
            Manager = new RemoteTeminalManager(RdpViewer);
        }

        // Получение и установление событий на стороне пользователя
        public RdpManager RdpManager
        {
            get
            {
                return (RdpManager)GetValue(RdpManagerProperty);
            }
            set
            {
                SetValue(RdpManagerProperty, value);
            }
        }
    }
}