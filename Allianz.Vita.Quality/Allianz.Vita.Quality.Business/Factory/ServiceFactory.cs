using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Factory
{
    public class ServiceFactory
    {

        static object lockObj = new object() { };

        static Dictionary<Type, IService> _Services = new Dictionary<Type, IService>();

        public static bool IsDefined<TService>() where TService : IService
        {
            return _Services.ContainsKey(typeof(TService));
        }


        public static TService Get<TService>() where TService : IService
        {
            IService service;
            if (!_Services.TryGetValue(typeof(TService), out service))
            {
                throw new NotSupportedException("Non definito servizio " + typeof(TService).Name);
            }

            return (TService)service;
        }

        public static TService Register<TService,TInstance>(params object[] parameters) 
            where TService : IService
            where TInstance : TService
        {
            lock (lockObj)
            {
                if (!_Services.ContainsKey(typeof(TService)))
                {
                    _Services.Add(typeof(TService), null);
                }

                _Services[typeof(TService)] = (TInstance)Activator.CreateInstance(typeof(TInstance), parameters);
            }

            return Get<TService>();
        }

        public static void Register<TService>(TService instance)
           where TService : IService
        {
            lock (lockObj)
            {
                if (!_Services.ContainsKey(typeof(TService)))
                {
                    _Services.Add(typeof(TService), null);
                }
            }

            _Services[typeof(TService)] = instance;
            
        }

        public static TService Ensure<TService, TInstance>()
            where TService : IService
            where TInstance : TService
        {
            lock (lockObj)
            {
                return IsDefined<TService>() ?
                Get<TService>()
                : Register<TService, TInstance>();
            }
        }
    }
}
