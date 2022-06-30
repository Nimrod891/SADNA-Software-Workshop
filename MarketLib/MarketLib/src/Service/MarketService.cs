using MarketLib.src.MarketSystemNS;
using MarketLib.src.StoreNS;
using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.Service
{
    public class MarketService : IMarketSystemService
    {
        private MarketSystem market= MarketSystem.Instance;

        public Response acceptBidAsManager(string connectionId, int bidId)
        {
            Response r;
            try
            {
                market.acceptBidAsManager(connectionId , bidId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }


        public Response acceptCounterOfferAsBuyer(string connectionId, int bidId)
        {
            try
            {
                market.acceptCounterOfferAsBuyer(connectionId, bidId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response addItemToBasket(string connectionID, int storeId, int productId, int amount)
        {
            Response r;
            try
            {
                market.addItemToBasket(connectionID,storeId,productId,amount);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response addProductToStore(string username, int storeId, string productName, string category, string subCategory, int quantity, double price)
        {
            Response r;
            try
            {
                market.addProductToStore(username, storeId,productName, category,subCategory,quantity, price);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response appointStoreManager(string username, string assigneeUserName, int storeId)
        {
            Response r;
            try
            {
                market.appointStoreManager(username, assigneeUserName,storeId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response appointStoreOwner(string username, string assigneeUserName, int storeId)
        {
            Response r;
            try
            {
                market.appointStoreOwner(username, assigneeUserName, storeId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<int> bidOnItemAsBuyer(string connectionId, int storeId, string product_name, double price, string category, string subcat)
        {
            Response<int> res;
            try
            {
                int ans = market.bidOnItemAsBuyer(connectionId, storeId,  product_name,  price,  category, subcat);
                res = new Response<int>(ans);
            }
            catch (Exception e)
            {
                res =new Response<int>(e.Message);
            }
            return res;
        }

        public Response closeShop(string username, int storeID)
        {
            Response r;
            try
            {
                market.closeShop(username, storeID);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<string> connect()
        {
            try
            {
                return new Response<string>(market.connect());                
            }
            catch(Exception e)
            {
                return new Response<string>(e.Message);
            }
        }

        public Response counterOfferAsManager(string connectionId, int bidId, double newPrice)
        {
            Response res;
            try
            {
                 market.counterOfferAsManager(connectionId, bidId,newPrice);
                res = new Response();
            }
            catch (Exception e)
            {
                res = new Response<int>(e.Message);
            }
            return res;
        }

        public Response declineBidAsManager(string connectionId, int bidId)
        {
            Response res;
            try
            {
                market.declineBidAsManager(connectionId, bidId);
                res = new Response();
            }
            catch (Exception e)
            {
                res = new Response<int>(e.Message);
            }
            return res;
           
        }

        public Response declineCounterOfferAsBuyer(string connectionId, int bidId)
        {
            Response res;
            try
            {
                market.declineCounterOfferAsBuyer(connectionId, bidId);
                res = new Response();
            }
            catch (Exception e)
            {
                res = new Response<int>(e.Message);
            }
            return res;
        }

        public Response deleteProductFromStore(string username, int storeId, int productID)
        {
            Response r;
            try
            {
                market.deleteProductFromStore(username, storeId, productID);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response fireStoreOwner(string username, string target, int storeId)
        {
            Response r;
            try
            {
                market.removeStoreOwner(username, target,storeId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<List<Subscriber>> getAllUsers(string username)
        {
            Response<List<Subscriber>> r;
            try
            {
                List<Subscriber> ans = market.getAllUsers(username);
                return new Response<List<Subscriber>>(ans);
            }
            catch (Exception e)
            {
                return new Response<List<Subscriber>>(e.Message);
            }
        }

        public Response<List<PurchaseRecord>> getStoreHistory(string username, string storeId)
        {
            throw new NotImplementedException();
        }

        public Response giveManagerGetHistoryPermmision(string username, int storeId, string managerUserName)
        {
            Response r;
            try
            {
                market.giveManagerHistoryPermmission(username,storeId,managerUserName);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response giveManagerUpdateProductsPermmission(string username, int storeId, string managerUserName)
        {
            Response r;
            try
            {
                market.giveManagerUpdateProductsPermmission(username, storeId, managerUserName);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<Subscriber> login(string connectionid, string userName, string pass)
        {
            Response<Subscriber> r;
            try
            {
                Subscriber ans =market.login(connectionid, userName, pass);
                return new Response<Subscriber>(ans);
            }
            catch (Exception e)
            {
                return new Response<Subscriber>(e.Message);
            }
        }

        public Response logout(string username)
        {
            Response r;
            try
            {
                market.logout(username);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<int> openNewStore(string username, string newStoreName)
        {
            Response<int> r;
            try
            {
                int ans = market.openNewStore(username, newStoreName).Result;
                return new Response<int>(ans);
            }
            catch (Exception e)
            {
                return new Response<int>(e.Message);
            }
        }

        public Response openShop(string username, int storeID)
        {
            Response r;
            try
            {
                market.openShop(username,storeID);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }


        //TODO: EXCEPTION.
        public Response purchaseCart(string userID)
        {
            Response r;
            try
            {
                market.purchaseCart(userID);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response register(string connectionId, string userName, string password)
        {
            Response r;
            try
            {
                market.register(connectionId, userName,password);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response removeItemFromBasket(string connectionID, int storeId, int productId)
        {
            Response r;
            try
            {
                market.removeItemFromBasket(connectionID, storeId, productId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response removeManager(string username, int storeId, string managerUserName)
        {
            Response r;
            try
            {
                market.removeManager(username, storeId, managerUserName);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response removeUserFromSystem(string username, string target_name)
        {
            Response r;
            try
            {
                market.removeUserFromSystem(username, target_name);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<List<Product>> searchProducts(string connection, string name, string category, int minprice, int maxprice, int product_rating, int store_rating)
        {
            Response<List<Product>> r;
            try
            {
                List<Product> ans = market.filterProducts(connection,store_rating,name,category,minprice,maxprice,product_rating);
                return new Response<List<Product>>(ans);
            }
            catch (Exception e)
            {
                return new Response<List<Product>>(e.Message);
            }
        }

        public Response<Store> searchStore(string connection, int store_id)
        {
            Response<Store> r;
            try
            {
                Store ans = market.searchStore(connection, store_id);
                return new Response<Store>(ans);
            }
            catch (Exception e)
            {
                return new Response<Store>(e.Message);
            }
        }

        public Response<List<Basket>> showCart(string userID)
        {
            Response<List<Basket>> r;
            try
            {
                List<Basket> ans = market.showCart(userID);
                return new Response<List<Basket>>(ans);
            }
            catch (Exception e)
            {
                return new Response<List<Basket>>(e.Message);
            }
        }

        public Response<List<string>> showStaffInfo(string username, int storeId)
        {
            throw new NotImplementedException();
        }

        public Response<List<Store>> storesInfo(string connectionid)
        {
            Response<List<Store>> r;
            try
            {
                List<Store> ans = market.StoresInfo(connectionid);
                return new Response<List<Store>>(ans);
            }
            catch (Exception e)
            {
                return new Response<List<Store>>(e.Message);
            }
        }

        public Response takeManagerGetHistoryPermmision(string username, int storeId, string managerUserName)
        {
            Response r;
            try
            {
                market.takeManagerGetHistoryPermmision(username, storeId, managerUserName);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response takeManagerUpdatePermmission(string username, int storeId, string managerUserName)
        {
            Response r;
            try
            {
                market.takeManagerUpdatePermmission(username, storeId, managerUserName);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response UpdateItemQuantityBasket(string connectionID, int storeId, int productId, int amount)
        {
            Response r;
            try
            {
                market.updateProductAmountInBasket(connectionID, storeId, productId, amount);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response updateProductDetails(int storeid, string username, int productID, string newSubCategory, int newQuantity, double newPrice)
        {
            Response r;
            try
            {
                market.updateProductDetails(storeid,username,productID,newSubCategory,newQuantity,newPrice);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

    }
}
