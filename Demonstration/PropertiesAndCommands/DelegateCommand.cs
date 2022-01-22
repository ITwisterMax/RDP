using System;
using System.Windows.Input;

namespace Rdp.Demonstration.PropertiesAndCommands
{
    // Класс для реализации поведения на команды
    public class DelegateCommand : ICommand
    {
        // Для проверки изменения поведения команды
        private readonly Predicate<object> _canExecute;
        // Для изменения поведения команды
        private readonly Action<object> _execute;

        // Конструкторы
        public DelegateCommand(Action<object> execute) : this(execute, null)
        {
        }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Событие проверки изменения поведения команды
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        // Разрешение или запрет на изменение поведения команды
        bool ICommand.CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        // Изменение на поведение команды
        void ICommand.Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}