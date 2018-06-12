using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace Task2
{
    public class ServiceLocator
    {
        private static IContainer _container;

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public static TService Resolve<TService>()
        {
            return _container.Resolve<TService>();
        }

        public static TService Resolve<TService>(params Parameter[] parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            return _container.Resolve<TService>(parameters);
        }
        public static IEnumerable<TService> ResolveAll<TService>()
        {
            var type = typeof(IEnumerable<>).MakeGenericType(new[] { typeof(TService) });
            return (IEnumerable<TService>)_container.Resolve(type);
        }
        public static IContainer GetContainer()
        {
            return _container;
        }
    }
}
