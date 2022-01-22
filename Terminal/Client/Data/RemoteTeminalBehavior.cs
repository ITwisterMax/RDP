using System.Windows;
using Rdp.Terminal.Core.Client.Controls;
using Rdp.Terminal.Core.Client.Models;
namespace Rdp.Terminal.Core.Client.Data
{
    // Определение логики работы терминала
    internal static class RemoteTeminalBehavior
    {
        // Инициализация свойства зависимости
        public static void InitializeDependencyProperties()
        {
            RemoteTerminal.RdpManagerProperty = DependencyProperty.Register(nameof(RemoteTerminal.RdpManager),
                typeof(RdpManager), typeof(RemoteTerminal), new PropertyMetadata(default(RdpManager), RdpManagerChangedCallback));
        }

        // CallBack-фунция изменения текущих свойств Rdp менеджера
        private static void RdpManagerChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            // Сохранение состояния
            var terminal = (RemoteTerminal)dependencyObject;
            var oldManager = (RdpManager)args.OldValue;
            var newManager = (RdpManager)args.NewValue;

            // Присоединение пользователя к серверу (привязка свойств изменений)
            oldManager?.Detach();
            newManager?.Attach(terminal.Manager);
        }
    }
}