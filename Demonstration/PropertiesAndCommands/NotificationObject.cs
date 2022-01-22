using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Threading;

namespace Rdp.Demonstration.PropertiesAndCommands
{
    [Serializable]
    // Класс для поддержки элементов с сигналами событий
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        // Конструктор
        protected NotificationObject()
        {
        }

        // Вызывается при изменении свойств компонента   
        public event PropertyChangedEventHandler PropertyChanged;

        // Вызывается при изменении одного свойства компонента (установка этого изменения)
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Вызывается при изменении нескольких свойств компонента (установка этих изменений)
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

            // Преобразование массива параметров в массив строк и перебор каждого
            string[] strArrays = propertyNames;
            foreach (string propertyName in strArrays)
            {
                this.RaisePropertyChanged(propertyName);
            }
        }

        // Вызывается при изменении приотритетного свойства компонента на основе выражения свойста (установка этого изменения)
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            // Получение имени свойства и установка изменения
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