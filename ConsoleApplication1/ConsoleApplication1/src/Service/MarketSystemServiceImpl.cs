using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
using System.Runtime;
using System.Collections.ObjectModel;
using java.lang;
using StorePack;
using tradingSystem;
using Userpack;
using String = System.String;

namespace Service.TradingSystemServiceImpl
{
//this class offers all the functionality that serve the system
    public class MarketSystemServiceImpl : IMarketSystemService
    {
        private MarketSystem market;


        public int connect()
        {
            return market.connect();
        }

        public void exit(int connectionid)
        {
            market.exit(connectionid);
        }

        public void register(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public string login(int connectionid, string userName, string pass)
        {
            throw new System.NotImplementedException();
        }

        public void logout(string username)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Store> StoresInfo()
        {
            throw new System.NotImplementedException();
        }

        public void addItemToBasket(string connectionID, string storeId, string productId, int amount)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Basket> showCart(string userID)
        {
            throw new System.NotImplementedException();
        }

        public void updateProductAmountInBasket(string connectionID, string storeId, string productId, int newAmount)
        {
            throw new System.NotImplementedException();
        }

        public void purchaseCart(string userID)
        {
            throw new System.NotImplementedException();
        }

        public void writeOpinionOnProduct(string connectionID, string storeID, string productId, string desc)
        {
            throw new System.NotImplementedException();
        }
 
        public int openNewStore(string username, string newStoreName)
        {
            throw new System.NotImplementedException();
        }

        public void appointStoreManager(string username, string assigneeUserName, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public void addProductToStore(string username, string storeId, string productName, string category, string subCategory,
            int quantity, double price)
        {
            throw new System.NotImplementedException();
        }

        public void deleteProductFromStore(string username, string storeId, string productID)
        {
            throw new System.NotImplementedException();
        }

        public void updateProductDetails(string username, string productID, string newSubCategory, int newQuantity, double newPrice)
        {
            throw new System.NotImplementedException();
        }

        public void appointStoreOwner(string username, string assigneeUserName, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public void giveManagerUpdateProductsPermmission(string username, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public void takeManagerUpdatePermmission(string username, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public void giveManagerGetHistoryPermmision(string username, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public void takeManagerGetHistoryPermmision(string username, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public bool removeManager(string username, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public bool removeOwner(string username, string storeId, string targetUserName)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<string> showStaffInfo(string username, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<string> getStoreHistory(string username, string storeId)
        {
            throw new System.NotImplementedException();
        }
    }
}