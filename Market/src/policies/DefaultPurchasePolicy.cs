namespace policies;
using Userpack;

public class DefaultPurchasePolicy : AbsSimplePurchasePolicy {

    
    public bool isValidPurchase(Basket purchaseBasket) { return true; }
}
