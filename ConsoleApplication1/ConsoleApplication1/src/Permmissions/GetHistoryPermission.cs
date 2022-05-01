using System;
using StorePack;

namespace ConsoleApplication1.Permmissions
{
    public class GetHistoryPermission : AbsStorePermission
    {
        public GetHistoryPermission(Store store) : base(store)
        {
           
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
