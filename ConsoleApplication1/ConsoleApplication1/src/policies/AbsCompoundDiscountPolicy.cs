using System.Collections.Generic;
using StorePack;
using Userpack;

namespace policies{
    public abstract class AbsCompoundDiscountPolicy : DiscountPolicyIF {
        public abstract double cartTotalValue(Basket purchaseBasket);
        public abstract int getDiscount();
        public abstract List<Product> getItems();
        public abstract List<DiscountPolicyIF> getDiscountPolicies();
    }

}