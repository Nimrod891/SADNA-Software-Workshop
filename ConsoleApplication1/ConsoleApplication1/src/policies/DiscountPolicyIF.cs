
using StorePack;
using System.Collections.Generic;
using System;
using Userpack;

namespace policies
{
    public interface DiscountPolicyIF
    {

        double cartTotalValue(Basket purchaseBasket);

        int getDiscount();

        List<Product> getItems();

        List<DiscountPolicyIF> getDiscountPolicies();
    }
}