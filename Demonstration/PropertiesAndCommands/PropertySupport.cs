using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Rdp.Demonstration.PropertiesAndCommands
{
    /// <summary>
    ///     Support class for properties
    /// </summary>
    public static class PropertySupport
    {
        /// <summary>
        ///     Get property name
        /// </summary>
        ///
        /// <typeparam name="T">Type</typeparam>
        /// <param name="propertyExpression">Property expression</param>
        /// 
        /// <returns>string</returns>
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