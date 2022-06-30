using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using MongoDB.Bson.Serialization.Attributes;

namespace MarketLib.src.PurchasePolicies
{
    class ProductQuantityPolicy : BasePurchasePolicy
    {
        [BsonElement]
        private readonly List<Product> products;
        [BsonElement]
        private readonly int minQuantity;
        [BsonElement]
        private readonly int maxQuantity;

        public ProductQuantityPolicy(List<Product> products, int minquantity, int max)
        {
            this.products = products;
            this.minQuantity = minquantity;
            this.maxQuantity = max;
        }
        public override bool checkValidPurchase(Basket basket)
        {
            foreach (KeyValuePair<Product, int> productAndQuantity in basket.Products)
            {
                Product p = productAndQuantity.Key;
                int q = productAndQuantity.Value;
                if (!products.Contains(p))
                {
                    return false;            
                }
                else if (!(minQuantity <= q) && !(maxQuantity >= q)) { return false; }
              
            }
            return true;

        }
    }
}
