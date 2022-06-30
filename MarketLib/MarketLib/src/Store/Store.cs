using MarketLib.src.DiscountPolicies;
using MarketLib.src.PurchasePolicies;
using MarketLib.src.UserP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketLib.src.NotificationPackage;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MarketLib.src.StoreNS
{
    public class Store : IPublisher
    {
        
        
        private PurchasePolicy purchase_policy;
        //private DiscountPolicy discount_policy;
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        private int id;

        [BsonElement]
        private string name;
        [BsonElement]
        private string description;
        [BsonElement]
        private double rating;
        [BsonElement]
        private bool isopen=false;
        [BsonElement]
        private List<Subscriber> observers;
        [BsonElement]
        public int managerCounter;
        [BsonElement]
        private readonly string founderUserName;
        [BsonElement]

        private Inventory inventory = new Inventory();
        [BsonElement]
        private ArrayList purchases = new ArrayList(); // arrayList<string> : purcheses

        public bool Isopen { get => isopen;  }

        public string FounderUserName => founderUserName;

 
        public void closeShop(string username)
        {
            if (username == founderUserName)
            {
                isopen = false;
                notifyShopClose(new ShopCloseNotification(this));
            }
            else
                throw new Exception("only store founder can close the shop");
        }

        public void openShop(string username)
        {
            if (username == founderUserName)
            {
                isopen = false;
                notifyShopOpen(new ShopOpenNotification(this));
            }
            else
                throw new Exception("only store founder can open the shop");
        }

        public Store() { }

        internal bool checkIfManager(string maybeManagerUserName)
        {
            foreach (Subscriber s in observers)
                if (s.UserName == maybeManagerUserName)
                    return true;
            return false;
        }

        /**
         * This method opens a new store and create its inventory
         *
         * @param name        - the name of the new store
         * @param description - the price of the new store
         *                    //  * @param founder - the fonder of the new store
         * @throws WrongNameException
         */
        public Store(int id, string name, string UserName, string description="default shop desc")
        {//, Observable observable) {
            if (name == null || name.Equals("") || name.Trim().Equals(""))
                throw new Exception("WrongNameException: store name is null or contains only white spaces");
            if (name.ToCharArray()[0] >= '0' && name.ToCharArray()[0] <= '9')
                throw new Exception("WrongNameException : store name cannot start with a number");
            if (description == null || description.Trim().Equals(""))
                throw new Exception("WrongNameException :  store description is null or contains only white spaces");
            this.id = id;
            this.name = name;
            this.description = description;
            this.rating = 0;
            this.founderUserName = UserName;
            this.observers = new List<Subscriber>();
            this.managerCounter = 0;
         
        }




        public int getId()
        {
            return id;
        }

        public String getName()
        {
            return name;
        }

        public void setDescription(string newDesc) {
            this.description = newDesc;
        }                                                  

        public String getDescription()
        {
            return description;
        }

        public double getRating()
        {
            return rating;
        }

        public void setRating(double rating)
        {
            if (rating < 0)
                throw new Exception(" WrongRatingException: rating must be a positive number");
            this.rating = rating;
        }

        public Inventory getInventory()
        {
            return inventory;
        }




        public int addItem(string name, double price, string category, string subCategory, int amount)
        {
            return this.inventory.addProduct(name, price, category, amount);
        }

        public Product getItem(string name, string category, string subCategory)
        {
            return this.inventory.getItem(name, category, subCategory);
        }


        public Product searchItemById(int productId)
        {
            return this.inventory.searchItem(productId);
        }

        public List<Product> searchProductByName(string name)
        {
            return inventory.searchItemByName(name);
        }

        public List<Product> searchProductByCategory(string category)
        {
            return inventory.searchItemByCategory(category);
        }

        public List<Product> filterByPrice(List<Product> products, double startPrice, double endPrice)
        {
            if (startPrice <= 0 || endPrice <= 0 || products == null)
                throw new Exception();
                return products.Intersect(inventory.filterByPrice(startPrice, endPrice)).ToList();

        }

        public List<Product> filterByRating(List<Product> products, double rating)
        {
            if (rating < 0 || products == null)
                throw new Exception();
            return products.Intersect(inventory.filterByRating(rating)).ToList();
        }


        public bool checkAmount(int productId, int amount)
        {
            return this.inventory.checkAmount(productId, amount);
        }


        public Product removeItem(int productId)
        {
            return this.inventory.removeItem(productId);
        }

        public string toString()
        {
            return inventory.toString();
        }

        public void changeItem(int itemID, int newQuantity, double newPrice)
        {
            this.inventory.changeItemDetails(itemID, newQuantity, newPrice);
        }



        public ArrayList getPurchaseHistory() { return purchases; }

        public void addPurchase(string purchaseDetails)
        {
            this.purchases.Add(purchaseDetails);
        }

        public double calculateBasket(Basket basket)
        {
            return inventory.calculateBasket(basket);
        }


        public void unlockItems(HashSet<Product> products)
        {
            foreach (Product p in products)
                p.unlockstore();
        }

        public void subscribe(Subscriber observer)
        {
            observers.Add(observer);
        }

        public void unsubscribe(Subscriber observer)
        {
            observers.Remove(observer);
        }


        public void notifyShopOpen(ShopOpenNotification notification)
        {
            foreach (Subscriber observer in observers)
                observer.update(notification);
        }

        public void notifyShopClose(ShopCloseNotification notification)
        {
            foreach (Subscriber observer in observers)
                observer.update(notification);
        }

        public void notifyShopPurchase(PurchaseNotification notification)
        {
            foreach (Subscriber observer in observers)
                observer.update(notification);
        }
    }
}
