using System.Collections.Generic;
using Userpack;

namespace policies
{

    public class DefaultPurchasePolicy : AbsSimplePurchasePolicy
    {

        public override bool isValidPurchase(Basket purchaseBasket)
        {
            return true;
        }

    }
}