using System;
using System.Collections.Generic;
namespace Domain;
using datalayer;

public class Basket {
private Dictionary<int,int> basketMap; //  key-itemId , value-quantity
public Basket()
{
    basketMap = new Dictionary<int, int>();
}

public void addProductToBasket(int itemId, int quantity){
    if (quantity <= 0 ) throw new addProductToBasketException("Quantity is lower or equal to 0 .");
    if (!this.basketMap.ContainsKey(itemId)) this.basketMap.Add(itemId , quantity);
    else this.basketMap[itemId] += quantity;
}


public void removeProductToBasket(int itemId, int quantity){
    if (quantity <= 0  || !this.basketMap.ContainsKey(itemId) ) throw new removeProductToBasketException("worng quantity or itemId .");
    else this.basketMap[itemId] -= quantity;
    if (this.basketMap[itemId]<=0)
        this.basketMap.Remove(itemId);
}

public Dictionary<int,int> getDictionaryBasket(){
    return basketMap;
}

}
