
using System.Collections.ObjectModel;
using java.util;
using Service.TradingSystemServiceImpl;
using tradingSystem;
using Userpack;
using Xunit;

namespace ConsoleApplication1.Tests.tradingSystem
{
    public class TradingSystemServiceTest
    {
        private MarketSystemServiceImpl service;

        void SetUp()
        {
            service = new MarketSystemServiceImpl(new MarketSystenImpl(new MarketSystem(new UserAuthentication())));
        }
        void BreakDown()
        {
            service = new MarketSystemServiceImpl(new MarketSystenImpl(new MarketSystem(new UserAuthentication())));
        }
        
        void SetUpStores()
        {
            //Add Store and Product to store of current marketservice
            var store_id = service.openNewStore("1", "Store1");
            service.addProductToStore("1", store_id, "Product1", "Category1", "SubCategory1a", 1, 40.0);
        }
        [Fact]
        public void getStoresInfo(string userID)
        {
            //
            SetUp();
            SetUpStores();
            var expected_store_info_string_collection = service.getStoresInfo("1");
            Collection<string> c = new Collection<string>();
            c.Add("Product1"+"\n");
            var actual_store_info_string_collection = c;
            Assert.Equal(expected_store_info_string_collection,actual_store_info_string_collection);
            BreakDown();
        }

        [Fact]
        public Collection<string> getItemsByStore(string userID, string storeId)
        {
            throw new System.NotImplementedException();
        }
        [Fact]
        public Collection<string> getItems(string keyWord, string productName, string category, string subCategory,
            double ratingItem,
            double ratingStore, double maxPrice, double minPrice)
        {
            throw new System.NotImplementedException();
        }

        public void addItemToBasket(string userID, string storeId, string productId, int amount)
        {
            throw new System.NotImplementedException();
        }

        public Collection<string> showCart(string userID)
        {
            throw new System.NotImplementedException();
        }

        public Collection<string> showBasket(string userID, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public void updateProductAmountInBasket(string userID, string storeId, string productId, int newAmount)
        {
            throw new System.NotImplementedException();
        }

        public void purchaseCart(string userID)
        {
            throw new System.NotImplementedException();
        }

        public Collection<string> getPurchaseHistory(string userID)
        {
            throw new System.NotImplementedException();
        }

        public void writeOpinionOnProduct(string userID, string storeID, string productId, string desc)
        {
            throw new System.NotImplementedException();
        }


        public string openNewStore(string userID, string newStoreName)
        {
            throw new System.NotImplementedException();
        }

        public void appointStoreManager(string userID, string assigneeUserName, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public string addProductToStore(string userID, string storeId, string productName, string category,
            string subCategory,
            int quantity, double price)
        {
            throw new System.NotImplementedException();
        }

        public void deleteProductFromStore(string userID, string storeId, string productID)
        {
            throw new System.NotImplementedException();
        }

        public void updateProductDetails(string userID, string productID, string newSubCategory, int newQuantity,
            double newPrice)
        {
            throw new System.NotImplementedException();
        }

        public void appointStoreOwner(string userID, string assigneeUserName, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public Collection<Integer> getStorePolicies(string userID, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public void assignStorePurchasePolicy(int policyId, string userID, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public void removePolicy(string userID, string storeId, int policyId)
        {
            throw new System.NotImplementedException();
        }

        public int makeQuantityPolicy(string userID, string storeId, Collection<string> items, int minQuantity,
            int maxQuantity)
        {
            throw new System.NotImplementedException();
        }

        public int makeBasketPurchasePolicy(string userID, string storeId, int minBasketValue)
        {
            throw new System.NotImplementedException();
        }

        public int makeTimePolicy(string userID, string storeId, Collection<string> items, string time)
        {
            throw new System.NotImplementedException();
        }

        public int andPolicy(string userID, string storeId, int policy1, int policy2)
        {
            throw new System.NotImplementedException();
        }

        public int orPolicy(string userID, string storeId, int policy1, int policy2)
        {
            throw new System.NotImplementedException();
        }

        public int xorPolicy(string userID, string storeId, int policy1, int policy2)
        {
            throw new System.NotImplementedException();
        }

        public Collection<int> getStoreDiscounts(string userID, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public void assignStoreDiscountPolicy(int discountId, string userID, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public void removeDiscount(string userID, string storeId, int discountId)
        {
            throw new System.NotImplementedException();
        }

        public int makeQuantityDiscount(string userID, string storeId, int discount, Collection<string> items,
            int policyId)
        {
            throw new System.NotImplementedException();
        }

        public int makePlusDiscount(string userID, string storeId, int discountId1, int discountId2)
        {
            throw new System.NotImplementedException();
        }

        public int makeMaxDiscount(string userID, string storeId, int discountId1, int discountId2)
        {
            throw new System.NotImplementedException();
        }

        public void allowManagerToUpdateProducts(string userID, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public void disableManagerFromUpdateProducts(string userID, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public void allowManagerToEditPolicies(string userID, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public void disableManagerFromEditPolicies(string userID, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public void allowManagerToGetHistory(string userID, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public void disableManagerFromGetHistory(string userID, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public bool removeManager(string userID, string storeId, string managerUserName)
        {
            throw new System.NotImplementedException();
        }

        public bool removeOwner(string connId, string storeId, string targetUserName)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<string> showStaffInfo(string userID, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public Collection<string> getSalesHistoryByStore(string userID, string storeId)
        {
            throw new System.NotImplementedException();
        }

        public Collection<string> getEventLog(string userID)
        {
            throw new System.NotImplementedException();
        }

        public Collection<string> getErrorLog(string userID)
        {
            throw new System.NotImplementedException();
        }
    }
}