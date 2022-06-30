using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.Service
{
   
        public interface IMarketSystemService
        {



        /// <summary>
        /// connects the visitor to the system and return his connection id
        /// that is used to help other functionality in the system.
        /// user requirment: 1.1 users
        /// </summary>
        /// <returns></returns>.
        Response<string> connect();

        Response<int> bidOnItemAsBuyer(string connectionId, int storeId, string product_name, double price, string category, string subcat);


        Response acceptBidAsManager(string connectionId, int bidId);

        Response declineBidAsManager(string connectionId, int bidId);


        Response counterOfferAsManager(string connectionId, int bidId, double newPrice);


        Response acceptCounterOfferAsBuyer(string connectionId, int bidId);




        Response declineCounterOfferAsBuyer(string connectionId, int bidId);




        /// <summary>
        /// registers a new member into the system.
        /// is supposed to work only if the current user is a visitor.
        /// visitor requirment: 1.3 users
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        Response register(string connectionId, string userName, string password);

        /// <summary>
        /// logins into the system
        /// should be an option only for a vistor.
        /// will return the username if successful.
        /// requirment: 1.4 users
        /// </summary>
        /// <param name="connectID"></param>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        Response<Subscriber> login(string connectionid, string userName, string pass);

        /// <summary>
        /// returns back all stores of the system
        /// in a dictionary format, the key being the store id and 
        /// value being its name.
        /// </summary>
        /// <param name="connectionid"></param>
        /// <returns></returns>
        Response<List<Store>> storesInfo(string connectionid);


        /// <summary>
        /// we pass the id of the store we want 
        /// and we get in return a store object.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="store_id"></param>
        /// <returns></returns>
        Response<Store> searchStore(string connection, int store_id);

        /// <summary>
        /// will return a list of products filtered by name, category,...
        /// we avoid non legal value/ stirngs and do not filter with them.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <param name="minprice"></param>
        /// <param name="maxprice"></param>
        /// <param name="product_rating"></param>
        /// <param name="store_rating"></param>
        /// <returns></returns>
        Response<List<Product>> searchProducts(string connection, string name, string category, int minprice, int maxprice, int product_rating, int store_rating);



        /// <summary>
        /// adds an item into the right basket
        /// </summary>
        /// <param name="userID">the connection id</param>
        /// <param name="storeId">the id of the store we are adding from </param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        Response addItemToBasket(string connectionID, int storeId, int productId, int amount);


        /// <summary>
        /// removes an existing item in the basket
        /// </summary>
        /// <param name="userID">the connection id</param>
        /// <param name="storeId">the id of the store we are adding from </param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        Response removeItemFromBasket(string connectionID, int storeId, int productId);


        /// <summary>
        /// adds an item into the right basket
        /// </summary>
        /// <param name="userID">the connection id</param>
        /// <param name="storeId">the id of the store we are adding from </param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        Response UpdateItemQuantityBasket(string connectionID, int storeId, int productId, int amount);

        /// <summary>
        /// will show the cart of the user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>the users cart</returns>
        Response<List<Basket>> showCart(string userID);

        /// <summary>
        /// makes the purchase of the cart. this method should be thread safe
        /// to not mistake in the product quantity.
        /// a purchase record should also be made for each basket(store) and user.(//TODO: think how to make purchase records in the system). 
        /// </summary>
        /// <param name="userID"></param>
        Response purchaseCart(string userID);


        //////////////////////////////////////////////////////////////////////ABOVE IS A NON REGISTERED USER FUNTIONS



        /// <summary>
        /// logouts from the system.
        /// a function that should be available only for members 
        /// </summary>
        /// <param name="username"></param>
        Response logout(string username);


        /// <summary>
        /// a member makes a new store.
        /// </summary>
        /// <param name="username"> the members username</param>
        /// <param name="newStoreName"></param>
        /// <returns>return the storeid</returns>
        Response<int> openNewStore(string username, string newStoreName);






        ///////////////Member methods above

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
        Response addProductToStore(string username, int storeId, string productName, string category, string subCategory,
         int quantity, double price);

        /// <summary>
        /// deletes a product from a store
        /// invoker is the store owner/manager  with permissions to make changes in products.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="storeId"></param>
        /// <param name="productID"></param>
        Response deleteProductFromStore(string username, int storeId, int productID);




        /// <summary>
        /// deletes a product from a store
        /// invoker is the store owner/manager  with permissions to make changes in products.
        /// </summary>
        Response updateProductDetails(int storeid, string username, int productID, string newSubCategory, int newQuantity, double newPrice);


        /// <summary>
        /// appoints a new store owner
        /// </summary>
        /// <param name="username">the owner</param>
        /// <param name="assigneeUserName"></param>
        /// <param name="storeId"></param>
        Response appointStoreOwner(string username, string assigneeUserName, int storeId);


        /// <summary>
        /// notice that this method should cascade with all the the useres the were apointed by him.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="target"></param>
        /// <param name="storeId"></param>
        Response fireStoreOwner(string username, string target, int storeId);

        /// <summary>
        /// apoint a new member to the store as a store Manager.
        /// the member has to be somone who doesnt already has Mangmenr/Ownership permissions.
        /// the manager has one appointer which is a store owner and he only got permmsion to recieve data
        /// of the store.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="assigneeUserName"></param>
        /// <param name="storeId"></param>
        Response appointStoreManager(string username, string assigneeUserName, int storeId);





        /// <summary>
        /// get the purchase history of a user, only the same user has the permmision to do so
        /// and the store Manager.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        //Collection<string> getPurchaseHistory(string userID);TODO: think how to make purchase records in the system). 






        /// <summary>
        /// give a manager the permmision to update the product data.
        /// only manage who appointed him can give him this permission
        /// </summary>
        /// <param name="username">the owner</param>
        /// <param name="storeId"></param>
        /// <param name="managerUserName"></param>
        Response giveManagerUpdateProductsPermmission(string username, int storeId, string managerUserName);

        /// <summary>
        /// take from manager the permmision to update the product data.
        /// only manage who appointed him can disable this permission.
        /// </summary>
        /// <param name="username">the owner</param>
        /// <param name="storeId"></param>
        /// <param name="managerUserName"></param>
        Response takeManagerUpdatePermmission(string username, int storeId, string managerUserName);


        /// <summary>
        /// allows manager to get purchases history of the store. same conditions...
        /// </summary>
        /// <param name="username">the owner</param>
        /// <param name="storeId"></param>
        /// <param name="managerUserName"></param>
        Response giveManagerGetHistoryPermmision(string username, int storeId, string managerUserName);


        /// <summary>
        /// disables a manager from getting purchases history of the store. same conditions...
        /// </summary>
        /// <param name="username">the owner</param>
        /// <param name="storeId"></param>
        /// <param name="managerUserName"></param>
        Response takeManagerGetHistoryPermmision(string username, int storeId, string managerUserName);

        /// <summary>
        /// removes a manager from the store. only the owner who apointed the
        /// manaager can get rid of him.
        /// </summary>
        /// <param name="username">the owner</param>
        /// <param name="storeId"></param>
        /// <param name="managerUserName"></param>
        /// <returns></returns>
        Response removeManager(string username, int storeId, string managerUserName);


        /// <summary>
        /// closes the shop , meaning the users who arent working in the shop cannot gain shop details.
        /// supposed to make a notification to the uworkers
        /// this function is only available for the founder of the shop.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="storeID"></param>
        Response closeShop(string username, int storeID);

        /// <summary>
        /// opens the shop .
        /// supposed to make a notification to the uworkers
        /// this function is only available for the founder of the shop.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="storeID"></param>
        Response openShop(string username, int storeID);

        /// <summary>
        /// shows store staff information and their permissions in the store
        /// TODO: UNDERSTAND WHAT THEY WANT IN 4.11 for now it  returns stirng .
        /// </summary>
        /// <param name="username"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Response<List<string>> showStaffInfo(string username, int storeId);


        /// <summary>
        /// get the purchase history of a store by its id.
        ///  req 6.4 and 4.13.
        /// TODO: nee to think how to ass the history , maybe a list of purchases in the store class and user class or
        /// an assication class of history which keeps all the purchase data.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Response<List<PurchaseRecord>> getStoreHistory(string username, string storeId);


        /// <summary>
        /// admin method, see all subscribers that are logged in or not.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Response<List<Subscriber>> getAllUsers(string username);


        /// <summary>
        /// admin method, removes a user from the system.
        /// can remove only users that dont have any other role in the system.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="target"></param>
        Response removeUserFromSystem(string username, string target_name);

        }
    
}
