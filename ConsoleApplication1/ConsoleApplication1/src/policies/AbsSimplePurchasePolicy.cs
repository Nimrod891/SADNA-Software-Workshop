using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Userpack;

namespace policies{
    public abstract class AbsSimplePurchasePolicy : PurchasePolicyIF {

    public abstract bool isValidPurchase(Basket purchaseBasket);

    public ICollection<PurchasePolicyIF> getPurchasePolicies() { return new List<PurchasePolicyIF>(); }

    }
}