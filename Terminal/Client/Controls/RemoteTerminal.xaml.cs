using System.Windows;
using Rdp.Terminal.Core.Client.Data;
using Rdp.Terminal.Core.Client.Models;

namespace Rdp.Terminal.Core.Client.Controls
{
    /// <summary>
    ///     Remote terminal class
    /// </summary>
    public partial class RemoteTerminal
    {
        public static DependencyProperty RdpManagerProperty;
        
        internal readonly RemoteTeminalManager Manager;

        /// <summary>
        ///     Initialize dependencies
        /// </summary>
        static RemoteTerminal()
        {
            RemoteTeminalBehavior.InitializeDependencyProperties();
        }

        /// <summary>
        ///     Default constructor
        /// </summary>
        public RemoteTerminal()
        {
            DataContext = this;
            InitializeComponent();
            Manager = new RemoteTeminalManager(RdpViewer);
        }

        /// <summary>
        ///     RDP manager
        /// </summary>
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