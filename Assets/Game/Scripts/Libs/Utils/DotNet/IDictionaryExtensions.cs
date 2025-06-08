using System.Collections.Generic;

namespace Libs.Utils.DotNet
{
    public static class IDictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if(!dictionary.ContainsKey(key))
                dictionary.Add(key, new TValue());
            return dictionary[key];
        }

        public static void AddRangeOfKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            foreach (var key in keys) 
                dictionary.Add(key, default(TValue));
        }
    }
}