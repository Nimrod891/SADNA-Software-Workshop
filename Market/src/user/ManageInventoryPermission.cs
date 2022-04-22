namespace User;
using StorePack;
using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
public class ManageInventoryPermission : AbsStorePermission
{
    private ManageInventoryPermission(Store store) {
        this.store = store;
    }

    public static ManageInventoryPermission getInstance(Store store) {

        
        ManageInventoryPermission mip =  new ManageInventoryPermission(store);
        
        AbsStorePermission abs; 
        (ManagerPermission)ComputeIfAbsent(pool,mip, new WeakReference(mip)).TryGetTarget(abs);
        if(abs != null) return abs;
        else throw  ExceptionManageInventoryPermission("ManageInventoryPermission something was worng");
    }

    
    public String toString() {
        return "ManageInventoryPermission{" +
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
