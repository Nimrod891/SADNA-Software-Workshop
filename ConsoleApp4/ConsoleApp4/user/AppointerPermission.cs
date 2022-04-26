using System;

using StorePack;
using externalService;
using policies;
using System.Collections.Generic;
using System.Text;
namespace Userpack {
    public class AppointerPermission : AbsStorePermission
    {
        private Subscriber target;

        private AppointerPermission(Subscriber target, Store store) {
            base(store);
            this.target = target;
        }

        public Subscriber getTarget() {
            return target;
        }

        public static AppointerPermission getInstance(Subscriber target, Store store) {

            AppointerPermission ap = new AppointerPermission(target, store);

            AbsStorePermission abs;
            (AppointerPermission)ComputeIfAbsent(pool, ap, new WeakReference(ap)).TryGetTarget(abs);
            if (abs != null) return abs;
            else throw ExceptionAppointerPermission("AppointerPermission something was worng");
        }

        public bool equals(object o) {
            if (this == o) return true;
            if (o == null || GetType().Name != o.GetType().Name || !base.equals(o)) return false;
            AppointerPermission that = (AppointerPermission)o;
            return object.Equals(target, that.target);
        }

        public int hashCode() {
            return base.GetType().GetHashCode() + store.GetType().GetHashCode();
        }


        public String toString() {
            return "AppointerPermission{" +
                    "store=" + (store == null ? null : store.GetType().Name) +
                    " target=" + (target == null ? null : target.getUserName()) +
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
}