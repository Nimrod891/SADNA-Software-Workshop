
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

            var mip = new ManageInventoryPermission(store);
            return (ManageInventoryPermission)pool.putIfAbsent(mip,new WeakReference(mip));
        }


        public string toString()
        {
            return "ManageInventoryPermission{" +
                    "store=" + (store == null ? null : store.GetType().Name) +
                    '}';
        }

      
    }
}
