using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Practices.ServiceLocation
{
    public static class ServiceLocatorExtensions
    {
        public static object TryResolve(this IServiceLocator locator, Type type)
        {
            try
            {
                return locator.GetInstance(type);
            }
            catch (ActivationException)
            {
                return null;
            }
        }

        public static T TryResolve<T>(this IServiceLocator locator) where T : class
        {
            return locator.TryResolve(typeof(T)) as T;
        }

        public static T TryResolve<T, K>(this IServiceLocator locator, Action<K> initialize)
            where T : class
            where K : class
        {
            try
            {
                var service = locator.TryResolve(typeof(T));
                var obj = service as K;
                if (obj != null) initialize(obj);
                return service as T;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
