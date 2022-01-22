using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Rdp.Demonstration.PropertiesAndCommands
{
    // Предоставляет поддержку для извлечения информации о свойствах на основе выражения свойства
    public static class PropertySupport
    {
        // Извлечение имени свойства
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }
            MemberExpression body = propertyExpression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException();
            }
            PropertyInfo member = body.Member as PropertyInfo;
            if (member == null)
            {
                throw new ArgumentException();
            }
            if (member.GetGetMethod(true).IsStatic)
            {
                throw new ArgumentException();
            }
            return body.Member.Name;
        }
    }
}