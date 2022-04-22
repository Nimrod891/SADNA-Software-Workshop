namespace User;
using StorePack;
using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
public class GetHistoryPermission : AbsStorePermission
{
    private GetHistoryPermission(Store store) {
        this.store = store;
    }

    public static GetHistoryPermission getInstance(Store store) {

        
        GetHistoryPermission ghp =  new GetHistoryPermission(store);
        
        AbsStorePermission abs; 
        (GetHistoryPermission)ComputeIfAbsent(pool,ghp, new WeakReference(ghp)).TryGetTarget(abs);
        if(abs != null) return abs;
        else throw  ExceptionGetHistoryPermission("GetHistoryPermission something was worng");
    }

    
    public String toString() {
        return "GetHistoryPermission{" +
                "store=" + (store == null ? null : store.GetType().Name) +
                '}';
    }

  private static V ComputeIfAbsent<K, V>(this Dictionary<K, V> dict, K key, Func<K, V> generator) {
    bool exists = dict.TryGetValue(key, out var value);
    if (exists) {
        return value;
    }
    var generated = generator;
    dict.Add(key, generated);
    return generated;
}
}
