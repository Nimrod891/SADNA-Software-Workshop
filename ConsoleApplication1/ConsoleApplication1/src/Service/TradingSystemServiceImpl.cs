using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System.Collections.ObjectModel;
using java.lang;
using tradingSystem;
using String = System.String;

namespace Service.TradingSystemServiceImpl{
//this class offers all the functionality that serve the system
public class TradingSystemServiceImpl: ITradingSystemService
{
    private  TradingSystenImpl trading;
    public TradingSystemServiceImpl(TradingSystenImpl tradingServiceImpl)
    {
       // this.tradingSystemImpl = tradingSystemImpl;
    }


    //will happen when someone joins the thread, will initialize him as a user(visitor).
    public string connect()
    {
       // return tradingSystemImpl.connect();
       return "";
    }

    //will happen when the vistor presses the X button. will erase him from the system.
    //if its a member this function wont do nothing
    public void exit(string userid)
    {
        //logger
        //tradingSystemImpl.exit(userid);
    }



    public void register(string userName, string password)  {
        //tradingSystemImpl.register(userName, password);
    }
 
    public void login(string connectID, string userName, string pass)  {
       // eventLog.writeToLogger("Login with userName: " + userName + ", password: *********");
       // tradingSystemImpl.login(connectID, userName, pass);
    }

    public void logout(String connectID)  {
        //eventLog.writeToLogger("Logout subscriber");
        //tradingSystemImpl.logout(connectID);
    }

    public Collection<string> getItems(string keyWord, string productName, string category, string subCategory, double ratingItem,
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

    public Collection<string> getStoresInfo(string userID)
    {
        throw new System.NotImplementedException();
    }

    public Collection<string> getItemsByStore(string userID, string storeId)
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

    public string addProductToStore(string userID, string storeId, string productName, string category, string subCategory,
        int quantity, double price)
    {
        throw new System.NotImplementedException();
    }

    public void deleteProductFromStore(string userID, string storeId, string productID)
    {
        throw new System.NotImplementedException();
    }

    public void updateProductDetails(string userID, string productID, string newSubCategory, int newQuantity, double newPrice)
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

    public int makeQuantityPolicy(string userID, string storeId, Collection<string> items, int minQuantity, int maxQuantity)
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

    public int makeQuantityDiscount(string userID, string storeId, int discount, Collection<string> items, int policyId)
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