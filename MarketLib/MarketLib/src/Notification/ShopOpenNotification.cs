using MarketLib.src.StoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.NotificationPackage
{
    public  class ShopOpenNotification : NotificationStrat
    {
        private Store store;

        public ShopOpenNotification(Store store)
        {
            this.store = store;
        }

        public string makeNotification()
        {
            return $"store {store.getName()} now  opened";
        }
    }
}
