using System;

using StorePack;

using java.util;

namespace Userpack {
    public class AppointerPermission : AbsStorePermission
    {
        private Subscriber target;

        private AppointerPermission(Subscriber target, Store store) : base(store)
        {
            this.target = target;
        }

        public Subscriber getTarget() {
            return target;
        }

        public static AppointerPermission getInstance(Subscriber target, Store store) {
            var ap = new AppointerPermission(target, store);
            return (AppointerPermission)pool.putIfAbsent(ap, new WeakReference(ap));
        }

        public bool equals(object o) {
            if (this == o) return true;
            if (o == null || GetType().Name != o.GetType().Name || !base.equals(o)) return false;
            var that = (AppointerPermission)o;
            return object.Equals(target, that.target);
        }

        public int hashCode()
        {
            return Objects.hash(base.hashCode(), target);
        }


        public string toString() {
            return "AppointerPermission{" +
                    "store=" + (store == null ? null : store.GetType().Name) +
                    " target=" + (target == null ? null : target.getUserName()) +
                    '}';
        }


    }
}