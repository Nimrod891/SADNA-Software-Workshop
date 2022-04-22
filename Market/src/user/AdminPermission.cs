namespace User;
using StorePack;
using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
public class AdminPermission : AbsPermission
{
    private AdminPermission(Store store) {
        this.store = store;
    }

    public static AdminPermission getInstance(Store store) {

        
        AdminPermission ad =  new AdminPermission(store);
        
        AbsStorePermission abs; 
        (AdminPermission)ComputeIfAbsent(pool,ghp, new WeakReference(ghp)).TryGetTarget(abs);
        if(abs != null) return abs;
        else throw  ExceptionAdminPermission("AdminPermission something was worng");
    }

    
    public String toString() {
        return "AdminPermission{" +
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