using System.Collections.Generic;

namespace Word_prefix.Extension
{
    public static class SortedDictionaryExtensions{
         public static void Increment<T>(this SortedDictionary<T, int> dictionary, T key)
    {
        int count;
        dictionary.TryGetValue(key, out count);
        dictionary[key] = count + 1;
    }
    }
}