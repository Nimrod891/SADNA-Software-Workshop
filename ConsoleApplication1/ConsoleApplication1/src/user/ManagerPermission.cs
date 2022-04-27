using StorePack;
using System;


namespace Userpack
{
    public class ManagerPermission : AbsStorePermission
    {
        private ManagerPermission(Store store) : base(store)
        {
        }

        public static ManagerPermission getInstance(Store store)
        {
            
            ManagerPermission mp = new ManagerPermission(store);
            AbsStorePermission abs;
            return (ManagerPermission)pool.putIfAbsent(mp, new WeakReference(mp));

        }


        public String toString()
        {
            return "ManagerPermission{" +
                    "store=" + (store == null ? null : store.GetType().Name) +
                    '}';
        }
        
    }
}
