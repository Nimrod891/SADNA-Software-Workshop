
using System.Collections.Generic;
using StorePack;

namespace Userpack
{
    public abstract class AbsStorePermission : AbsPermission
    {

        protected Store store;

        protected StorePermission(Store store)
        {
            this.store = store;
        }

        public Store getStore()
        {
            return store;
        }

        public bool equals(object o)
        {
            if (this == o) return true;
            if (o == null || this.GetType().Name != o.GetType.Name || !base.Equals(o)) return false;
            StorePermission that = (StorePermission)o;
            return object.Equals(store, that.store);
        }


        public int GetHashCode()
        {
            return base.GetType().GetHashCode() + store.GetType().GetHashCode();
        }


        public String toString()
        {
            return GetType().ToString() + "{" +
                    "store=" + (store == null ? null : store.getName()) +
                    '}';
        }
    }
}