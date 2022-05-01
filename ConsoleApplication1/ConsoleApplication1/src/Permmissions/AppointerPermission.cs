using System;
using java.util;
using StorePack;
using Userpack;

namespace ConsoleApplication1.Permmissions {
    
    /// <summary>
    /// A class  that is supposed to act as the apointer permision to a target.
    /// gives him the permmision to change permisions for the target.
    /// </summary>
    public class AppointerPermission : AbsStorePermission
    {
        private Subscriber target;

        public AppointerPermission(Subscriber target, Store store) : base(store)
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


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        

    }
}