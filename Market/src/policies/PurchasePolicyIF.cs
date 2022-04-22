namespace policies;
using System;
using System.Collections.Generic;


public interface PurchasePolicyIF {

    bool isValidPurchase(Basket purchaseBasket);

    List<PurchasePolicy> getPurchasePolicies();

}
