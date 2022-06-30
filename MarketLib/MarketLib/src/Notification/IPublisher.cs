using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.NotificationPackage
{
    public interface IPublisher
    {
        void subscribe(Subscriber observer);

        void unsubscribe(Subscriber observer);

        void notifyShopOpen(ShopOpenNotification notification);

        void notifyShopClose(ShopCloseNotification notification);

        void notifyShopPurchase(PurchaseNotification notification);

    }
}
