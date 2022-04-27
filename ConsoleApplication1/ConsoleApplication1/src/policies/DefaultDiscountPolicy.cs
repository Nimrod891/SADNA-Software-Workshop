
using System.Collections.Generic;
using java.util;
using StorePack;
using Userpack;

namespace policies
{


    public class DefaultDiscountPolicy : AbsSimpleDiscountPolicy
    {

        public DefaultDiscountPolicy(Set p) : base(0,p)
        {
            
        }
        

        public override double cartTotalValue(Basket purchaseBasket)
        {
            double value = 0;
            foreach (KeyValuePair<Product, int> itemsAndQuantity in purchaseBasket.getItems())
            {
                Product p = itemsAndQuantity.Key;
                int quantity = itemsAndQuantity.Value;
                value += (p.Price * quantity);
            }
            return value;
        }
    }
}