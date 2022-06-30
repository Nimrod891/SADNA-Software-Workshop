using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.NotificationPackage
{
    public class PurchaseNotification : NotificationStrat
    {
        private User user;
        private Basket basket;
        private Store store;

        public PurchaseNotification(User user, Basket basket, Store store)
        {
            this.user = user;
            this.basket = basket;
            this.store = store;
        }

        public string makeNotification()
        {
            return "PurchaseNotification{" + "buyer=" + user + ", basket=" + basket +'}';
        }
    }
}
