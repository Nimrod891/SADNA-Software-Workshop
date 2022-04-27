
using StorePack;
using System.Collections.Generic;
using System;


namespace Userpack
{

    public class ManageInventoryPermission : AbsStorePermission
    {
        private ManageInventoryPermission(Store store) : base(store){
       }

        public static ManageInventoryPermission getInstance(Store store)
        {

            ManageInventoryPermission mip = new ManageInventoryPermission(store);
            return (ManageInventoryPermission)pool.putIfAbsent(mip,new WeakReference(mip));
        }


        public String toString()
        {
            return "ManageInventoryPermission{" +
                    "store=" + (store == null ? null : store.GetType().Name) +
                    '}';
        }

        private static V ComputeIfAbsent<K, V>(Dictionary<K, V> dict, K key, V generator)
        {
            bool exists = dict.TryGetValue(key, out var value);
            if (exists)
            {
                return value;
            }
            V generated = generator;
            dict.Add(key, generated);
            return generated;
        }
    }
}
