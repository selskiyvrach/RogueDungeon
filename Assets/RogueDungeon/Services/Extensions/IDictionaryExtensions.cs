using System.Collections.Generic;

namespace RogueDungeon.Services.Extensions
{
    public static class IDictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if(!dictionary.ContainsKey(key))
                dictionary.Add(key, new TValue());
            return dictionary[key];
        }
    }
}