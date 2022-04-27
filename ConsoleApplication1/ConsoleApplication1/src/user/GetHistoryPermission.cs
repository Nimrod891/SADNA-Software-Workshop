using StorePack;
using System;

namespace Userpack
{
    public class GetHistoryPermission : AbsStorePermission
    {
        private GetHistoryPermission(Store store) : base(store)
        {
           
        }

        public static GetHistoryPermission getInstance(Store store)
        {
            
            var ghp = new GetHistoryPermission(store);
            AbsStorePermission abs;
            return   (GetHistoryPermission)pool.putIfAbsent(ghp, new WeakReference(ghp));
          
        }


        public string toString()
        {
            return "GetHistoryPermission{" +
                    "store=" + (store == null ? null : store.GetType().Name) +
                    '}';
        }

    }
}
