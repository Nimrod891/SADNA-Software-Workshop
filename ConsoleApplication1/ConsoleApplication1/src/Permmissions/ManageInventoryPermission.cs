using System;
using StorePack;

namespace ConsoleApplication1.Permmissions
{

    public class ManageInventoryPermission : AbsStorePermission
    {
       public ManageInventoryPermission(Store store) : base(store){ }

       public override bool Equals(object obj)
       {
           return base.Equals(obj);
       }
    }
}
