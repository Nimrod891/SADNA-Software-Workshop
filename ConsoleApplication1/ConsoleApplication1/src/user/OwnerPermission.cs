
using StorePack;
using System;


namespace Userpack
{
    public class OwnerPermission : AbsStorePermission
    {
        private OwnerPermission(Store store) : base(store)
        {
            
        }

        public static OwnerPermission getInstance(Store store)
        {
            var mp = new OwnerPermission(store);
            return (OwnerPermission)pool.putIfAbsent(mp, new WeakReference(mp));
        }


        public string toString()
        {
            return "OwnerPermission{" +
                    "store=" + (store == null ? null : store.GetType().Name) +
                    '}';
        }
        
    }
}