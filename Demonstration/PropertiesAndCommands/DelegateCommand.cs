using System;
using System.Windows.Input;

namespace Rdp.Demonstration.PropertiesAndCommands
{
    /// <summary>
    ///     Delegate command class
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        ///     Check if command can be executed
        /// </summary>
        private readonly Predicate<object> _canExecute;
        
        /// <summary>
        ///     Execute command
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        ///     Check if command can be executed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Default constructor
        /// </summary>
        ///
        /// <param name="execute">Execute command</param>
        public DelegateCommand(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        ///
        /// <param name="execute">Execute command</param>
        /// <param name="canExecute">Check if command can be executed</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        
        /// <summary>
        ///     Get can execute changed event
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Get can execute variable
        /// </summary>
        ///
        /// <param name="parameter">Parameter</param>
        ///
        /// <returns>bool</returns>
        bool ICommand.CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        /// <summary>
        ///     Execute command
        /// </summary>
        ///
        /// <param name="parameter">Parameter</param>
        void ICommand.Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}