using System;
using System.Collections.Concurrent;
using System.Security.Policy;
using ConsoleApp4.authentication;
using externalService.ConsoleApp4.authentication;
using StorePack;
using externalService;
using policies;
using System.Collections.Generic;
using System.Text;
using java.util;
using java.util.concurrent;

namespace Userpack {
    public class Vistor {
        protected ConcurrentHashMap baskets; // store , basket

        public Vistor() {
            this.baskets = new ConcurrentHashMap(); // store , basket
        }

        public Vistor(ConcurrentHashMap baskets) {
            this.baskets = baskets;
        }

        public ConcurrentHashMap getCart() {
            return this.baskets;
        }

        public void makeCart(Vistor from) {

            if (baskets.isEmpty())
                baskets.putAll(from.getCart());
        }

        public Basket getBasket(Store store) {

            if (baskets.contains(store))
            {
                return (Basket)baskets.get(store);
            }
            baskets.put(store, new Basket(store, new ConcurrentHashMap()));
            return (Basket)(baskets.get(store));
        }


        public void purchaseCart(PaymentSystem paymentSystem, DeliverySystem deliverySystem) {

            double totalPrice = 0;
            HashMap storePurchaseDetails = new HashMap(); // k:Store, v:string
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
                foreach (KeyValuePair<Store, Basket> entry in baskets)
                    entry.Key.rollBack(entry.Value.getItems());
                throw e;
            }
            if (totalPrice == 0)
                return;
            // add each purchase details string to the store it was purchased from
            if (storePurchaseDetails != null)
            {
                foreach (KeyValuePair<Store,string> entry in storePurchaseDetails)
                    entry.Key.addPurchase(entry.Value);

                foreach (KeyValuePair<Store, Basket> storeBasketEntry in baskets)
                {
                    Store store = storeBasketEntry.Key;
                    ConcurrentHashMap basket = storeBasketEntry.Value.getItems();
                    // store.notifyPurchase(this, basket);
                }

                addCartToPurchases(storePurchaseDetails);
            }

            baskets.clear();
        }

        private double processCartAndCalculatePrice(double totalPrice, Map storePurchaseDetails) {
            bool validPolicy;
            PurchasePolicyIF storePurchasePolicy;
            DiscountPolicyIF storeDiscountPolicy;

            foreach (KeyValuePair<Store, Basket> storeBasketEntry in baskets)
            {
                storePurchasePolicy = storeBasketEntry.Key.getPurchasePolicy();
                storeDiscountPolicy = storeBasketEntry.Key.getDiscountPolicy();
                validPolicy = storePurchasePolicy.isValidPurchase(storeBasketEntry.Value);
                if (!validPolicy)
                    throw new PolicyException("policy problem");

                StringBuilder purchaseDetails = new StringBuilder();
                Store store = storeBasketEntry.Key;
                Basket basket = storeBasketEntry.Value;
                double price = store.processBasketAndCalculatePrice(basket, purchaseDetails, storeDiscountPolicy);
                totalPrice += price;
                purchaseDetails.Append("Total basket price: ").Append(price).Append("\n");
                storePurchaseDetails.put(store, purchaseDetails.ToString());
            }
            return totalPrice;
        }
        
        public void addCartToPurchases(HashMap details) {
            // overridden in subclass
        }



        private static V ComputeIfAbsent<K, V>( ConcurrentDictionary<K, V> dict, K key, Func<K, V> generator) {
            var exists = dict.TryGetValue(key, out var value);
            if (exists) {
                return value;
            }
            var generated = generator(key);
            dict.TryAdd(key, generated);
            return generated;
        }


    }
}