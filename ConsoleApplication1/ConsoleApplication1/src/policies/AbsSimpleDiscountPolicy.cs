using System.Collections.Generic;
using System.Collections.ObjectModel;
using java.util;
using StorePack;
using Userpack;

namespace policies{
    public abstract class AbsSimpleDiscountPolicy : DiscountPolicyIF {
        
        protected int discount;
        protected Collection items;

        public AbsSimpleDiscountPolicy(int discount, Collection items) {
            this.discount = discount;
            this.items = items;
        }

        public abstract double cartTotalValue(Basket purchaseBasket);
        public int getDiscount() { return discount; }

        public List<Product> getItems() { return (List<Product>)items; }

        public List<DiscountPolicyIF> getDiscountPolicies() {
            return new List<DiscountPolicyIF>();
        }
    }
}