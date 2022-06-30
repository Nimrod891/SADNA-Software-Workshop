using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace client.CostumClasses
{
    public class Data
    {
        public class Basket
        {
            string store_id;
            Dictionary<Product, int> products;

            public string Store_id { get => store_id; set => store_id = value; }
            public Dictionary<Product, int> Products { get => products; set => products = value; }
        }
        public class Product
        {
            int product_id;
            double price;
            string category;
            double rating;

            public int Product_id { get => product_id; set => product_id = value; }
            public double Price { get => price; set => price = value; }
            public string Category { get => category; set => category = value; }
            public double Rating { get => rating; set => rating = value; }
        }
    }
}