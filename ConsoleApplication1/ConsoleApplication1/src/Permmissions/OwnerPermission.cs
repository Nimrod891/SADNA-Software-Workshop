using System;
using StorePack;

namespace ConsoleApplication1.Permmissions
{
    public class OwnerPermission : AbsStorePermission
    {
        public OwnerPermission(Store store) : base(store)
        {
            
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}