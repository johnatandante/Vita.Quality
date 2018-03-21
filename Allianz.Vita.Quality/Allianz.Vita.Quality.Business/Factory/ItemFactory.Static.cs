using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Factory
{
    public partial class ItemFactory
    {
        static Dictionary<Type, Type> _ItemTypes = new Dictionary<Type, Type>();

        public static bool IsDefined<TItem>() where TItem : IItem
        {
            return _ItemTypes.ContainsKey(typeof(TItem));
        }


        public static TItem Get<TItem>(params object[] parameters) where TItem : IItem
        {
            Type typeInstance;
            if (!_ItemTypes.TryGetValue(typeof(TItem), out typeInstance))
            {
                throw new NotSupportedException("Non definito item " + typeof(TItem).Name);
            }

            return (TItem)Activator.CreateInstance(typeInstance, parameters);

        }

        public static void Register<TItem, TInstance>()
            where TItem : IItem
            where TInstance : TItem
        {

            if (!_ItemTypes.ContainsKey(typeof(TItem)))
            {
                _ItemTypes.Add(typeof(TItem), null);
            }

            _ItemTypes[typeof(TItem)] = typeof(TInstance);

        }

    }
}
