using System;
using System.Collections.Generic;
namespace User;
using StorePack;
using System.Collections.Generic;
using System.Collections;
using StorePack;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
public record Basket(Store store, ConcurrentDictionary<Product, Int64> products) {
public Store getStore() {
        return store;
    }

    public void addItem(Product p, int quantity) {
        products.AddOrUpdate(p, (k, v) => v  == null ? quantity : v + quantity); // add to existing quantity
    }

    public int getQuantity(Product p) {
        return products.getOrDefault(p, 0);
    }

    public void setQuantity(Product p, int quantity) {
        products.put(p, quantity);
    }

    public Dictionary<Product, Int64> getItems() {
        return products;
    }

    public void removeProduct(Product p) {
        products.remove(p);
    }
}
