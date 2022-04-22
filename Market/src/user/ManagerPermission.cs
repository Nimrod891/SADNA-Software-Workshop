namespace Userpack;

using StorePack;
using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
public class ManagerPermission : AbsStorePermission
{
    private ManagerPermission(Store store) {
        this.store = store;
    }

    public static ManagerPermission getInstance(Store store) {

        
        ManagerPermission mp =  new ManagerPermission(store);
        
        AbsStorePermission abs; 
        (ManagerPermission)ComputeIfAbsent(pool,mp, new WeakReference(mp)).TryGetTarget(abs);
        if(abs != null) return abs;
        else throw  ExceptionManagerPermission("ManagerPermission something was worng");
    }

    
    public String toString() {
        return "ManagerPermission{" +
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
