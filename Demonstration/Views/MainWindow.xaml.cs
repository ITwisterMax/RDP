using System.Windows;
using Rdp.Demonstration.ViewModels;

namespace Rdp.Demonstration.Views
{
    // Главное окно
    public partial class MainWindow : Window
    {
        // Конструктор
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
    }
}