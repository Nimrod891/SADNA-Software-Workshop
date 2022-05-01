using System;
using StorePack;

namespace ConsoleApplication1.Permmissions
{
    public class ManagerPermission : AbsStorePermission
    {
        public ManagerPermission(Store store) : base(store)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
