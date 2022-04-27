
using System;
using System.Collections.Generic;
using Userpack;

namespace policies
{
    public interface PurchasePolicyIF
    {

        bool isValidPurchase(Basket purchaseBasket);

        ICollection<PurchasePolicyIF> getPurchasePolicies();

    }
}