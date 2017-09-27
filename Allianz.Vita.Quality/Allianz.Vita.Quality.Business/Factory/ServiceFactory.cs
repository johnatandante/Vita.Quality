using Allianz.Vita.Quality.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Factory
{
    public class ServiceFactory
    {

        static Dictionary<Type, IService> _Services = new Dictionary<Type, IService>();

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

            if (!_Services.ContainsKey(typeof(TService)))
            {
                _Services.Add(typeof(TService), null);
            }
                        
            _Services[typeof(TService)] = (TInstance)Activator.CreateInstance(typeof(TInstance), parameters);

            return Get<TService>();
        }

        public static void Register<TService>(TService instance)
           where TService : IService
        {

            if (!_Services.ContainsKey(typeof(TService)))
            {
                _Services.Add(typeof(TService), null);
            }

            _Services[typeof(TService)] = instance;
            
        }
        
    }
}
