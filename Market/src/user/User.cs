using System;
namespace User;
using StorePack;
using externalService;
using policies;
using System.Collections.Generic;
using System.Text;
public class User {

protected Dictionary<Store, Basket> baskets;
    public User() {
       this.baskets = new Dictionary<Store, Basket>();
    }

    public User(Dictionary<Store, Basket> baskets) {
        this.baskets = baskets;
    }

    public Dictionary<Store,Basket> getCart(){
        return this.baskets;
    }

     public void makeCart(User from) {

        if (this.baskets.Count == 0){
           foreach(s Store in from.baskets.Keys){
                 if(!this.baskets.ContainsKey(s))
                  {
                  this.baskets[s]= from.baskets[s];
                  }
             }
        }
    }
  
      public Basket getBasket(Store store) {

        return ComputeIfAbsent(this.baskets,store, k =>new Basket(k, new Dictionary<>())); // TODO maybe change after finish basket class
    }


    public void purchaseCart(PaymentSystem paymentSystem, DeliverySystem deliverySystem) {

        double totalPrice = 0;
        Dictionary<Store, string> storePurchaseDetails = new Dictionary<Store, string>();
        totalPrice = processCartAndCalculatePrice(totalPrice, storePurchaseDetails);
        PaymentData pd = null;
        
        bool paymentDone = false;
        try {
            pd = new PaymentData(totalPrice, null);
            paymentSystem.pay(pd);
            paymentDone = true;
            deliverySystem.deliver(new DeliveryData(null, null));
        } catch (Exception e) {
            if (paymentDone)
                paymentSystem.payBack(pd);
            // for each store, rollback the basket (return items to inventory)
            foreach (Dictionary<Store,Basket> entry in baskets)
                entry.Keys.rollBack(entry.Values.getItems());
            throw e;
        }
        if(totalPrice == 0)
            return;
        // add each purchase details string to the store it was purchased from
        foreach (Dictionary<Store, String> entry in storePurchaseDetails)
            entry.Keys.addPurchase(entry.Values);

        foreach (Dictionary<Store, Basket> storeBasketEntry in baskets) {
            Store store = storeBasketEntry.Keys;
            Dictionary<Product,Int64> basket = storeBasketEntry.Values.getItems();
           // store.notifyPurchase(this, basket);
        }        
        addCartToPurchases(storePurchaseDetails);
        baskets.Clear();
    }

    private double processCartAndCalculatePrice(double totalPrice, Dictionary<Store, String> storePurchaseDetails) {
        bool validPolicy;
        PurchasePolicyIF storePurchasePolicy;
        DiscountPolicyIF storeDiscountPolicy;

        foreach (Dictionary<Store, Basket> storeBasketEntry in baskets)
         {
            storePurchasePolicy = storeBasketEntry.Keys.getPurchasePolicy();
            storeDiscountPolicy = storeBasketEntry.Keys.getDiscountPolicy();
            validPolicy = storePurchasePolicy.isValidPurchase(storeBasketEntry.Values);
            if(!validPolicy)
                throw new PolicyException("policy problem");

            StringBuilder purchaseDetails = new StringBuilder();
            Store store = storeBasketEntry.Keys;
            Basket basket = storeBasketEntry.Values;
            double price = store.processBasketAndCalculatePrice(basket, purchaseDetails, storeDiscountPolicy);
            totalPrice += price;
            purchaseDetails.Append("Total basket price: ").Append(price).Append("\n");
            storePurchaseDetails.Add(store, purchaseDetails.toString());
        }
        return totalPrice;
    }



    private static V ComputeIfAbsent<K, V>(this Dictionary<K, V> dict, K key, Func<K, V> generator) {
    bool exists = dict.TryGetValue(key, out var value);
    if (exists) {
        return value;
    }
    var generated = generator(key);
    dict.Add(key, generated);
    return generated;
}


}