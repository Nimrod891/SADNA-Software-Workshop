using MarketLib.src.StoreNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.NotificationPackage
{
    public class ShopCloseNotification : NotificationStrat
    {
        private Store store;

        public ShopCloseNotification(Store store)
        {
            this.store = store;
        }
        public string makeNotification()
        {
            return $"store {store.getName()} now  closed";
        }
    }
}
