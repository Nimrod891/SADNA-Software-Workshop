namespace policies;
using Userpack;
using StorePack;
using System.Collections.Generic;
using System.Text;
using policies;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Collections;
public class DefaultDiscountPolicy : AbsSimpleDiscountPolicy {

    public DefaultDiscountPolicy(CollectionBase<Product> products) {
        base(0, products);
    }

    public double cartTotalValue(Basket purchaseBasket) {
        double value = 0;
        foreach(Dictionary<Product, Int32>.Enumerator productAndQuantity in purchaseBasket.getItems().GetEnumerator())
        {
            Product product = itemsAndQuantity.Current.Ke;
            int quantity = itemsAndQuantity.Current.Value;
            value += (item.getPrice() * quantity);
        }
        return value;
    }
}
