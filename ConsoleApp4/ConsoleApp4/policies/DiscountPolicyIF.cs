
using User;
using StorePack;
using System.Collections.Generic;
using System;

namespace policies
{
    public interface DiscountPolicyIF
    {

        double cartTotalValue(Basket purchaseBasket);

        int getDiscount();

        List<Product> getItems();

        List<DiscountPolicy> getDiscountPolicies();
    }
}