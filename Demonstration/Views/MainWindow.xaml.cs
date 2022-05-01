using System.Windows;
using Rdp.Demonstration.ViewModels;

namespace Rdp.Demonstration.Views
{
    /// <summary>
    ///     Main window class
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
    }
}