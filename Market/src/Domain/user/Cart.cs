using System.Collections.Generic;
namespace Domain;
using System;
using datalayer;

public class Cart {

  // Dictonary of all items in cart sorted by store and list of items from store 
            // key = store name , value = basket of store
private Dictionary<string,Basket> cartmap = new Dictionary<string,Basket>();
public Cart()
{
   
}
public void addProductToCart(string storeName, int itemId, int qun)
{
    if(!this.cartmap.ContainsKey(storeName))
        this.cartmap.Add(storeName,Basket());
    this.cartmap[storeName].addProductToBasket(itemId, qun);

}
public void removeProductToCart(string storeName, int itemId, int qun)
{
    if(!this.cartmap.ContainsKey(storeName))
        throw new removeProductToCartException("worng store");
    this.cartmap[storeName].removeProductToBasket(itemId, qun);

}

}