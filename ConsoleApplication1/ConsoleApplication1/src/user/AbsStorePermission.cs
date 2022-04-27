
using System;
using StorePack;

namespace Userpack
{
    public abstract class AbsStorePermission : AbsPermission
    {

        protected Store store;

        protected AbsStorePermission(Store store)
        {
            this.store = store;
        }

        public Store getStore()
        {
            return store;
        }

        public bool equals(Object o)
        {
            if (this == o) return true;
            if (o == null || this.GetType().Name != o.GetType().Name || !base.Equals(o)) return false;
            var that = (AbsStorePermission)o;
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