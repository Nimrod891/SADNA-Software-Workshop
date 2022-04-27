
using StorePack;
using System;


namespace Userpack
{
    public class EditPolicyPermission : AbsStorePermission
    {

        private EditPolicyPermission(Store store) : base(store)
        {
        }

        public static EditPolicyPermission getInstance(Store store)
        {

            var epc = new EditPolicyPermission(store);
            return (EditPolicyPermission)pool.putIfAbsent(epc, new WeakReference(epc));

        }


        public string toString()
        {
            return "EditPolicyPermission{" +
                    "store=" + (store == null ? null : store.GetType().Name) +
                    '}';
        }
        
    }
}