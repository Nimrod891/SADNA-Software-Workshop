namespace Userpack;
using StorePack;
using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
public class EditPolicyPermission : AbsStorePermission{

   private EditPolicyPermission(Store store) {
        base(store);
   }

    public static EditPolicyPermission getInstance(Store store) {

        EditPolicyPermission epc =  new EditPolicyPermission(store);
        
        AbsStorePermission abs; 
        (EditPolicyPermission)ComputeIfAbsent(pool,epc, new WeakReference(epc)).TryGetTarget(abs);
        if(abs != null) return abs;
        else throw  ExceptionEditPolicyPermission("editPolicyPermission something was worng");
    }

    
    public String toString() {
        return "EditPolicyPermission{" +
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