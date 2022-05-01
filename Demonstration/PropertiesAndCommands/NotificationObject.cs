using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Threading;

namespace Rdp.Demonstration.PropertiesAndCommands
{
    /// <summary>
    ///     Event notification class
    /// </summary>
    [Serializable]
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        protected NotificationObject()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Raise property changed
        /// </summary>
        ///
        /// <param name="propertyName">Property name</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Raise property changed
        /// </summary>
        ///
        /// <param name="propertyNames">Property names</param>
        protected void RaisePropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            string[] strArrays = propertyNames;
            foreach (string propertyName in strArrays)
            {
                this.RaisePropertyChanged(propertyName);
            }
        }

        /// <summary>
        ///     Raise property changed
        /// </summary>
        ///
        /// <param name="propertyExpression">Property expression</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            var propertyName = PropertySupport.ExtractPropertyName<T>(propertyExpression);

            if (!Dispatcher.CurrentDispatcher.CheckAccess())
            {
                Dispatcher.CurrentDispatcher.Invoke(new Action(() => RaisePropertyChanged(propertyName)));
            }
            else
            {
                RaisePropertyChanged(propertyName);
            }
        }
    }
}