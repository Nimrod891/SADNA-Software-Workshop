
using System;
using System.Collections.Generic;

namespace policies
{
    public interface PurchasePolicyIF
    {

        bool isValidPurchase(Basket purchaseBasket);

        List<PurchasePolicy> getPurchasePolicies();

    }
}