
using MarketLib.src.ExternalService.Payment;
using MarketLib.src.ExternalService.Supply;
using MarketLib.src.Security;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MarketLib.src.StorePermission;
using MarketLib.src.Data;

namespace MarketLib.src.MarketSystemNS
{
    public sealed class MarketSystem
    {
        private ConcurrentDictionary<string, User> connections;
        private ConcurrentDictionary<string, Subscriber> members;
        private ConcurrentDictionary<int, Store> stores; // key: store id
        private ConcurrentDictionary<int, Bid> bids; // key: bidCounter
        private int bidCounter = 0;
        private int storeCounter = 0;
        private int auctionCounter = 0;
        //ConcurrentDictionary<int, Auction> auctions; // key: auctionCounter
        private ItemSearchManager search = new ItemSearchManager();
        private  static UserSecurity usersecure = new UserSecurity();
        private int storeIdCounter = 0;
        private int counterId = 0;
        private static object locker1;
        private static object locker2;
        private PaymentSystem payment;
        private SupplySystem supply;
        public const int REGULAR_SELL = 0;
        public const int BID_SELL = 1;
        public const int AUCTION_SELL = 2;
        public const int LOTTERY_SELL = 3;
        DataAccessHandler db = new DataAccessHandler();



        private static readonly object locker = new object ();  
    private static MarketSystem instance = null;
        public static MarketSystem Instance
        {
            get
            {
                lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new MarketSystem();
                        }
                        return instance;
                    }
            }
        }

        public Subscriber getSubscriberByUserName(string userName)
        {
            Subscriber subscriber = members[userName];
            if (subscriber == null) throw new Exception(userName);
            return subscriber;
        }
        public void removeBid(int bidId)
        {
            this.bids.TryRemove(bidId, out _);
        }

 


        public MarketSystem()
        {
            connections = new ConcurrentDictionary<string, User>();
            members = new ConcurrentDictionary<string, Subscriber>();
            stores = new ConcurrentDictionary<int, Store>();
            payment = new PaymentSystemImpl(new PaymentAdapter());
            supply = new SupplySystemImpl(new DeliveryAdapter());       
            bids = new ConcurrentDictionary<int, Bid>();
        }


        public string connect()
        {
            string unqid = Guid.NewGuid().ToString();//generates a random user id.
            connections[unqid]= new User();
            return unqid;
        }

        public void exit(string connectid)
        {
            User v;
            connections.TryRemove(connectid, out v);
        }


        public void register(string connectionid, string username, string password)
        {
            if (connections.ContainsKey(connectionid)) //if you are a vistor
            {
                if (members.ContainsKey(username))//if this username already exists.
                    throw new Exception("this username already exists");
                Subscriber member = new Subscriber(username);
                members[username] = member;
                usersecure.storeUser(username, password);
            }

        }

        public void closeShop(string username, int storeID)
        {
            Store store = stores[storeID];
            store.closeShop(username);
        }

        //when a user logs in we return his username to use as an identifier
        //when we will have a data base this will maybe look al different.
        public Subscriber login(string connection, string username, string password)
        {
            if (connections.ContainsKey(connection) && members.ContainsKey(username))
            {
                usersecure.verifyUser(username, password); //verify user info 
                Subscriber m = members[username];
                m.Logedin = true;
                return members[username];
            }
            throw new Exception("login is not succeful");
        }

        public void removeStoreOwner(string username, string target, int storeId)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store store = stores[storeId];
            Subscriber tar = getSubscriberByUserName(target);
            user.validatePermission(new AppointerPermission(tar, store));
            tar.removeOwnerPermission(store);
        }

        public List<Subscriber> getAllUsers(string username)
        {
            Subscriber user = getSubscriberByUserName(username);
            if (user.IsAdmin)
                return members.Values.ToList();
            else
                throw new Exception("user is not an admin");
        }

        public List<Product> filterProducts( string connection,int store_rating, string name, string category, double startprice, double endprice, double rating)
        {
            if (connections.ContainsKey(connection))
                return search.filterProducts(stores.Values, store_rating, name, category, startprice, endprice, rating).ToList();
            throw new Exception("filter error");
        }

        public void giveManagerHistoryPermmission(string username, int storeId, string managerUserName)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store store = stores[storeId];
            Subscriber manager_target = getSubscriberByUserName(managerUserName);
            user.addHistoryPermission(manager_target, store);
        }

        //the logouy the user from the system and will retun hum as a new vistor
        //in this function we infer that the username value in the presentation/service layer is
        //back to null.
        public void logout(string connectionId)
        {
            User guest = new User();
            connections[connectionId] = guest;//with this we remove the permission to attain access to the subscriber.
        }

        /// <summary>
        /// gets info of all stores in the system,
        /// will  be used to make a dynamic gui.
        /// </summary>
        /// <returns>a collection of stores</returns>
        public List<Store> StoresInfo(string connection_id)
        {
            if (connections.ContainsKey(connection_id))
            {
                List<Store> stores = this.stores.Values.ToList();
                return stores;
            }
            else throw new Exception();
        }//TODO: we should make a store controller class which has all the functionality that store has 
         // and make store a data class with superficial data.


        /// <summary>
        /// adds an item into the right basket
        /// NOTE: this is before the purchase therfore he can take any quantity as he likes.
        /// </summary>
        /// <param name="userID">the connection id</param>
        /// <param name="storeId">the id of the store we are adding from </param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        public void addItemToBasket(string connectionID, int storeId, int productId, int amount)
        {
            User user = getUserByConnectionId(connectionID);
            Store store = stores[storeId];
            Product p = store.searchItemById(productId);
            if (p.mode == BID_SELL)
            {
                Console.WriteLine("this product is only open for bidding");
                return;
            }
            user.getBasket(storeId).addProduct(p, amount);
        }

        public void openShop(string username, int storeID)
        {
            Store store = stores[storeID];
            store.openShop(username);
        }

        public User getUserByConnectionId(string connectionId)
        {
            User user = connections[connectionId];
            if (user == null)
                throw new Exception("non such user exists");
            return user;
        }

        /// <summary>
        /// will show the cart of the user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>the users cart</returns>
        public  List<Basket> showCart(string userID)
        {
            User user = getUserByConnectionId(userID);
            return user.getCart();
        }

        internal void removeItemFromBasket(string connectionID, int storeId, int productId)
        {
            User user = getUserByConnectionId(connectionID);
            Store store = stores[storeId];
            Basket basket = user.getBasket(storeId);
            Product p = store.searchItemById(productId);
            basket.removeProduct(p);
        }

        internal void removeManager(string username, int storeId, string managerUserName)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store store = stores[storeId];
            Subscriber tar = getSubscriberByUserName(managerUserName);
            tar.removeManagerPermission(tar, store);
        }

        /// <summary>
        /// update a product amount in a specific store basket.        /// </summary>
        /// <param name="userID"></param>
        /// <param name="storeId"></param>
        /// <param name="productId"></param>
        /// <param name="newAmount"></param>
        public void updateProductAmountInBasket(string connectionID, int storeId, int productId, int newAmount)
        {
            User user = getUserByConnectionId(connectionID);
            Basket basket = user.getBasket(storeId);
            Store store = stores[storeId];
            Product p = store.searchItemById(productId);
            basket.setQuantity(p, newAmount);
        }

        public void removeUserFromSystem(string username, string target_name)
        {
            User user = getSubscriberByUserName(username);
            members.TryRemove(username, out var x);
        }



        /// <summary>
        /// makes the purchase of the cart. this method should be thread safe
        /// to not mistake in the product quantity.
        /// a purchase record should also be made for each basket(store) and user.(//TODO: think how to make purchase records in the system). 
        /// </summary>
        /// <param name="userID"></param>
        public void purchaseCart(string connectionID)
        {
            
            User user = getUserByConnectionId(connectionID);
            List<Basket> baskets = user.getCart();
            double totalprice = 0;
            try ///first try the external systems , then check if the purchase itslef is valid 
            {
                payment.handShake();
                supply.handshake();
                foreach ( Basket entry in baskets)
                {
                    Store store = stores[entry.Storeid];
                    double store_total = store.calculateBasket(user.getBasket(store.getId()));
                    totalprice += store_total;
                    PurchaseRecord purchaseDetails = new PurchaseRecord(user.getBasket(store.getId()).Products, DateTime.Now.ToString("dd-MM-yyyy"), store_total);
                }
                user.clearBaskets();
            }
            catch
            {
                throw new Exception("illegal purchase");
            }
        }

        public Store searchStore(string connection, int store_id)
        {
            getUserByConnectionId(connection);
            Store store = stores[store_id];
            if (store != null)
                return store;
            else
                throw new Exception("store does not exist");
        }



        /// <summary>
        /// a member makes a new store.
        /// </summary>
        /// <param name="username"> the members username</param>
        /// <param name="newStoreName"></param>
        /// <returns>return the storeid</returns>
        public async Task<int> openNewStore(string username, string newStoreName)
        {
            //lock (locker1)
            //{
                Subscriber user = getSubscriberByUserName(username);
                Store newStore = new Store(storeCounter, newStoreName, username);
                user.addOwnerPermission(newStore);
                stores.TryAdd(storeCounter, newStore);
                int current = storeCounter;
                storeCounter++;
                newStore.subscribe(user);
                await db.CreateStore(newStore);
                return current;
            //}
        }

        public void takeManagerGetHistoryPermmision(string username, int storeId, string managerUserName)
        {
            Subscriber user = getSubscriberByUserName(username);
            Subscriber target = getSubscriberByUserName(managerUserName);
            Store s = stores[storeId];
            user.removeHistoryPermmision(target, s);
        }

        /// <summary>
        /// apoint a new member to the store as a store Manager.
        /// the member has to be somone who doesnt already has Mangmenr/Ownership permissions.
        /// the manager has one appointer which is a store owner and he only got permmsion to recieve data
        /// of the store.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="assigneeUserName"></param>
        /// <param name="storeId"></param>
        public void appointStoreManager(string username, string assigneeUserName, int storeId)
        {
            Subscriber user = getSubscriberByUserName(username);
            Subscriber appointed = getSubscriberByUserName(assigneeUserName);
            Store s = stores[storeId];
            user.addManagerPermission(appointed, s);
        }

        /// <summary>
        /// add a new type of product to the store with its details.
        /// only a owner/manager can have the permmision to do so.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="storeId"></param>
        /// <param name="productName"></param>
        /// <param name="category"></param>
        /// <param name="subCategory"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// 



        public int addProductToStore(string username, int storeId, string productName, string category, string subCategory,
         int quantity, double price)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store s = stores[storeId];
            return user.addStoreItem(s, productName, category, subCategory, quantity, price);
        }

        public int bidOnItemAsBuyer(string username, int storeId, string product_name, double price, string category, string subcat)
        {
            if (price < 0 || stores[storeId] == null || members[username] == null || !(stores[storeId].getItem(product_name, category, subcat).mode == BID_SELL))
                return -1;

            Subscriber u = getSubscriberByUserName(username);
            Store s = stores[storeId];
            Product p = s.getItem(product_name, category, subcat);

            Bid b = new Bid(s, p, price, username);

            bids.TryAdd(bidCounter, b);
            this.bidCounter = this.bidCounter + 1;

            return bidCounter - 1;
        }

        public Bid getBid(int bidId)
        {
            if (!bids.ContainsKey(bidId) || bids[bidId] == null)
                return null;
            return this.bids[bidId];
        }

        public void acceptBidAsManager(string username, int bidId)
        { // bidId is the int in the <int, Bid> dictionary (bids).
            if (!bids.ContainsKey(bidId) || bids[bidId] == null)
            {
                Console.WriteLine("bid " + bidId + " does not exist in bids, or was already approved");
                return;
            }

            // Check if connectionId belongs to an actual manager of the stores
            if (!bids[bidId].related_store.checkIfManager(username))
            {
                Console.WriteLine("subscriber " + username + " is not a manager of store " + bids[bidId].related_store.getName());
                return;
            }

            Subscriber manager = getSubscriberByUserName(username);

            Bid b = bids[bidId];

            Store related_store_to_bid = b.getRelatedStore();

            if (!manager.havePermission(new StorePermission.ManagerPermission(related_store_to_bid)))
            {
                Console.WriteLine("\nerror - subscriber " + username + " doesnt have permissions to store " + related_store_to_bid.getName());
                return;
            }

            int ret = b.acceptBidAsManager();

            if (ret == 1)
            {
                // means bid was finally approved (by all managers)

                ConcurrentDictionary<Product, int> dict = new ConcurrentDictionary<Product, int>();
                dict.TryAdd(b.related_product, 0);
                PurchaseRecord purchaseDetails = new PurchaseRecord(dict, DateTime.Now.ToString("dd-MM-yyyy"), b.currrent_price);

                // complete user payment for b.related_product.Price ?


                bids[bidId] = null;
            }
        }


        public void declineBidAsManager(string username, int bidId)
        { // bidId is the int in the <int, Bid> dictionary (bids).
            if (!bids.ContainsKey(bidId) || bids[bidId] == null || !bids[bidId].isOpen)
            {
                Console.WriteLine("bid " + bidId + " does not exist in bids, or was already approved/declined");
                return;
            }

            // Check if connectionId belongs to an actual manager of the stores
            if (!bids[bidId].related_store.checkIfManager(username))
            {
                Console.WriteLine("user " + username + " is not a manager of store " + bids[bidId].related_store.getName());
                return;
            }

            Subscriber manager = getSubscriberByUserName(username);

            Bid b = bids[bidId];
            Bid tmp = new Bid();
            Store related_store_to_bid = b.getRelatedStore();

            if (!manager.havePermission(new StorePermission.ManagerPermission(related_store_to_bid)))
            {
                Console.WriteLine("\nerror - subscriber " + username + " doesnt have permissions to store " + related_store_to_bid.getName());
                return;
            }

            b.declineBidAsManager();
            this.bids[bidId] = null;
            removeBid(bidId);

            Console.WriteLine("bid " + bidId + " was declined by " + username);
        }

        public void counterOfferAsManager(string username, int bidId, double newPrice)
        {
            // bidId is the int in the <int, Bid> dictionary (bids).
            if (!bids.ContainsKey(bidId) || bids[bidId] == null)
            {
                Console.WriteLine("bid " + bidId + " does not exist in bids, or was already approved");
                return;
            }

            // Check if connectionId belongs to an actual manager of the stores
            if (!bids[bidId].related_store.checkIfManager(username))
            {
                Console.WriteLine("\nerror - user " + username + " is not a manager of store " + bids[bidId].related_store.getName());
                return;
            }

            Subscriber manager = getSubscriberByUserName(username);

            Bid b = bids[bidId];
            Store related_store_to_bid = b.getRelatedStore();

            if (!manager.havePermission(new StorePermission.ManagerPermission(related_store_to_bid)))
            {
                Console.WriteLine("\nerror - subscriber " + username + " doesnt have permissions to store " + related_store_to_bid.getName());
                return;
            }
            b.giveCounterOfferAsManager(newPrice);

        }

        public void acceptCounterOfferAsBuyer(string connectionId, int bidId)
        {
            // bidId is the int in the <int, Bid> dictionary (bids).
            if (!bids.ContainsKey(bidId) || bids[bidId] == null)
            {
                Console.WriteLine("bid " + bidId + " does not exist in bids, or was already approved");
                return;
            }
            Bid b = bids[bidId];
            b.acceptCounterOfferAsBuyer();
            removeBid(bidId);
            ConcurrentDictionary<Product, int> dict = new ConcurrentDictionary<Product, int>();
            dict.TryAdd(b.related_product, 0);
            PurchaseRecord purchaseDetails = new PurchaseRecord(dict, DateTime.Now.ToString("dd-MM-yyyy"), b.currrent_price);
        }

        public void declineCounterOfferAsBuyer(string connectionId, int bidId)
        {
            // bidId is the int in the <int, Bid> dictionary (bids).
            if (!bids.ContainsKey(bidId) || bids[bidId] == null)
            {
                Console.WriteLine("bid " + bidId + " does not exist in bids, or was already approved");
                return;
            }
            Bid b = bids[bidId];
            b.acceptCounterOfferAsBuyer();
            removeBid(bidId);

        }
       
        public void deleteProductFromStore(string username, int storeId, int productID)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store s = stores[storeId];
            user.removeStoreItem(s, productID);
        }

        public void updateProductDetails(int storeid, string username, int productID, string newSubCategory, int newQuantity, double newPrice)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store s = stores[storeid];
            user.updateStoreItem(s, productID, newSubCategory, newQuantity, newPrice);
        }

        public void appointStoreOwner(string username, string assigneeUserName, int storeId)
        {
            Subscriber user = getSubscriberByUserName(username);
            Store s = stores[storeId];
            user.addOwnerPermission(s);
        }

        public void giveManagerUpdateProductsPermmission(string username, int storeId, string managerUserName)
        {
            Subscriber user = getSubscriberByUserName(username);
            Subscriber target = getSubscriberByUserName(managerUserName);
            Store s = stores[storeId];
            user.addInventoryManagementPermission(target, s);
        }

        public void takeManagerUpdatePermmission(string username, int storeId, string managerUserName)
        {
            Subscriber user = getSubscriberByUserName(username);
            Subscriber target = getSubscriberByUserName(managerUserName);
            Store s = stores[storeId];
            user.removeManagerPermission(target, s);
        }



    }
}
