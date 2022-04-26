namespace policies;
using User;
using StorePack;
using System.Collections.Generic;
using System;

public interface DiscountPolicyIF {

    double cartTotalValue(Basket purchaseBasket);

    int getDiscount();

    List<Product> getItems();

    List<DiscountPolicy> getDiscountPolicies();
}
