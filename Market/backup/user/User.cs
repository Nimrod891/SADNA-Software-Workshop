using System;
namespace Userpack;
using StorePack;
using externalService;
using policies;
using System.Collections.Generic;
using System.Text;
using System.Windows;
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

        return ComputeIfAbsent(this.baskets,store, k =>new Basket(k, new Dictionary<Product,Int64>())); // TODO maybe change after finish basket class
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
            foreach (Dictionary<Store,Basket>.Enumerator entry in baskets)
                entry.Current.Key.rollBack(entry.Current.Value.getItems());
            throw e;
        }
        if(totalPrice == 0)
            return;
        // add each purchase details string to the store it was purchased from
        foreach (Dictionary<Store, String>.Enumerator entry in storePurchaseDetails)
            entry.Current.Key.addPurchase(entry.Values);

        foreach (Dictionary<Store, Basket>.Enumerator storeBasketEntry in baskets) {
            Store store = storeBasketEntry.Current.Key;
            Dictionary<Product,Int64> basket = storeBasketEntry.Current.Value.getItems();
           // store.notifyPurchase(this, basket);
        }        
        addCartToPurchases(storePurchaseDetails);
        baskets.Clear();
    }

    private double processCartAndCalculatePrice(double totalPrice, Dictionary<Store, String> storePurchaseDetails) {
        bool validPolicy;
        PurchasePolicyIF storePurchasePolicy;
        DiscountPolicyIF storeDiscountPolicy;

        foreach (Dictionary<Store, Basket>.Enumerator storeBasketEntry in baskets)
         {
            storePurchasePolicy = storeBasketEntry.Current.Key.getPurchasePolicy();
            storeDiscountPolicy = storeBasketEntry.Current.Key.getDiscountPolicy();
            validPolicy = storePurchasePolicy.isValidPurchase(storeBasketEntry.Current.Value);
            if(!validPolicy)
                throw new PolicyException("policy problem");

            StringBuilder purchaseDetails = new StringBuilder();
            Store store = storeBasketEntry.Current.Key;
            Basket basket = storeBasketEntry.Current.Value;
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