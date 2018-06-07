using System;
using System.Collections.Generic;
using System.Text;

public static class DictionaryExtension {
    public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) {
        TValue ret = default(TValue);
        dictionary.TryGetValue(key, out ret);
        return ret;
    }
}
