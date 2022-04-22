namespace Userpack;
using StorePack;
using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
public class OwnerPermission : AbsStorePermission
{
    private OwnerPermission(Store store) {
        this.store = store;
    }

    public static OwnerPermission getInstance(Store store) {

        
        OwnerPermission mp =  new OwnerPermission(store);
        
        AbsStorePermission abs; 
        (OwnerPermission)ComputeIfAbsent(pool,mp, new OwnerPermission(mp)).TryGetTarget(abs);
        if(abs != null) return abs;
        else throw  ExceptionOwnerPermission("OwnerPermission something was worng");
    }

    
    public String toString() {
        return "OwnerPermission{" +
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