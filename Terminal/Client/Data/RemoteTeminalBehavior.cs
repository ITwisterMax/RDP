using System.Windows;
using Rdp.Terminal.Core.Client.Controls;
using Rdp.Terminal.Core.Client.Models;

namespace Rdp.Terminal.Core.Client.Data
{
    /// <summary>
    ///     Remote terminal behavior
    /// </summary>
    internal static class RemoteTeminalBehavior
    {
        /// <summary>
        ///     Register dependencies
        /// </summary>
        public static void InitializeDependencyProperties()
        {
            RemoteTerminal.RdpManagerProperty = DependencyProperty.Register(nameof(RemoteTerminal.RdpManager),
                typeof(RdpManager), typeof(RemoteTerminal), new PropertyMetadata(default(RdpManager), RdpManagerChangedCallback));
        }

        /// <summary>
        ///     RDP manager changed callback
        /// </summary>
        ///
        /// <param name="dependencyObject">Dependency</param>
        /// <param name="args">Arguments</param>
        private static void RdpManagerChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var terminal = (RemoteTerminal)dependencyObject;
            var oldManager = (RdpManager)args.OldValue;
            var newManager = (RdpManager)args.NewValue;

            oldManager?.Detach();
            newManager?.Attach(terminal.Manager);
        }
    }
}