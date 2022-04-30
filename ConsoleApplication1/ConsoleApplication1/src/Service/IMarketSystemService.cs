using System.Collections.Generic;
using System.Collections.ObjectModel;
using java.lang;

namespace Service.TradingSystemServiceImpl
{

 public interface IMarketSystemService
 {
  //returns a connectId.
  string connect();

  /* Register to system
     preconditions: userName, pass not null; userName not already exist. */
  void register(string userName, string password);

  /* Login to system */
  void login(string connectID, string userName, string pass);

  /* Logout from system */
  void logout(string connectID);

  /* Get product by filter, uses spellchecking. */
  Collection<string> getItems(string keyWord, string productName, string category, string subCategory,
   double ratingItem,
   double ratingStore, double maxPrice, double minPrice);
  // the String in the collection represent item.toString()
  // TODO use spellChecking

  /* Save product in basket of a store. */
  void addItemToBasket(string userID, string storeId, string productId, int amount);

  /* get cart's products. */
  Collection<string> showCart(string userID);

  /* get basket's products. */
  Collection<string> showBasket(string userID, string storeId);

  /* updates the amount of a product for user from a specific store. if new amount = 0 then the product will be deleted from the basket.
  * If trying to update an item which not exist in the basket, the amount will be updated. */
  void updateProductAmountInBasket(string userID, string storeId, string productId, int newAmount);

  /* make purchase for every product in all of the user's baskets */
  //each purchase matches to an item from a store with the appropriate quantity.
  void purchaseCart(string userID);

  /* get purchase history of a user by permissions: user himself / system manager.
  * every purchase represents buying of a cart.
  * for example, if userId1 bought 3 "milk" products and 2 "eggs" products from storeId1, there will be 1 purchases for the user. */
  Collection<string> getPurchaseHistory(string userID);

  /* enables user to write an opinion on a product he has purchased.
  preconditions: 1. the user has purchased the product
                 2. productId belongs to storeId (even if quantity in inventory is 0)
                 3. desc is neither null, nor empty. */
  void writeOpinionOnProduct(string userID, string storeID, string productId, string desc);


  // ***********************************************************************
  // Topics: store owner, store manager, system manager
  // ***********************************************************************


  /* Get info of all stores owners and managers, and the products in every store
  preconditions: invoker is a system manager. */
  Collection<string> getStoresInfo(string userID);

  /* Get all products of the store, with store id.
  preconditions: invoker is the owner/manager of the store or is a system manager.*/
  //each String element in the collection represents an item in the store.
  Collection<string> getItemsByStore(string userID, string storeId);

  /* creates a new store. username is the founder and owner.
     pre-condition: 1. storeName is not null or empty
                    2.userId is a subscriber and not a guest
      returns storeId; */
  string openNewStore(string userID, string newStoreName);

  /* appoints a new store manager. assignor is an owner of the store, assignee is the username of the new store manager
   precondition: assignee is not a manager in this store and is a subscriber (not guest)
   poscondition: assignee have the permissions of a new store manager, i.e the basic permissions for a manager, which are:
                 get info about roles in the store and their permissions, get info about products in the store,
                 get requests from users and answer them.*/
  void appointStoreManager(string userID, string assigneeUserName, string storeId);

  /* adds a product to a store.
  // returns the product ID
  preconditions: invoker is the store owner or is a manager of it, with permissions to make changes in products. */
  //category and subCategory can be null or empty string. productName cannot be null or empty string. quantity and price cannot be < 0.
  string addProductToStore(string userID, string storeId, string productName, string category, string subCategory,
   int quantity, double price);

  /* deletes a product from a store 
  preconditions: invoker is the store owner or is a manager of it, with permissions to make changes in products. */
  void deleteProductFromStore(string userID, string storeId, string productID);

  /* updates a product details of a store.
  // if there is null, no need to update the field. productId cannot be changed.
  preconditions: invoker is the store owner or is a manager of it, with permissions to make changes in products.*/
  void updateProductDetails(string userID, string productID, string newSubCategory, int newQuantity, double newPrice);

  /* appoints a new store owner. assignor is an owner of the store, assignee is the username of a new store owner
   * pre-condition: assignee is not an owner in this store and is a subscriber (not guest) */
  void appointStoreOwner(string userID, string assigneeUserName, string storeId);


  /*The next block of functions deals with store policies. */
  //******************************************************************************
/* get all policies of a store.
    preconditions: invoker is the store owner or is a manager of it, with permissions to create store policies.*/
  Collection<Integer> getStorePolicies(string userID, string storeId);

  /* assign a policy to a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to create store policies.*/
  void assignStorePurchasePolicy(int policyId, string userID, string storeId);

  /* remove policy of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  void removePolicy(string userID, string storeId, int policyId);

  /* create quantity policy of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  int makeQuantityPolicy(string userID, string storeId, Collection<string> items, int minQuantity, int maxQuantity);

  /* create minimum basket purchase value policy of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  int makeBasketPurchasePolicy(string userID, string storeId, int minBasketValue);

  /* create time policy of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  int makeTimePolicy(string userID, string storeId, Collection<string> items, string time);

  /* create and policy between two policies of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  int andPolicy(string userID, string storeId, int policy1, int policy2);

  /* create or policy between two policies of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  int orPolicy(string userID, string storeId, int policy1, int policy2);

  /* create xor policy between two policies of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  int xorPolicy(string userID, string storeId, int policy1, int policy2);

  /* get all discount policies of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to create store policies.*/
  Collection<int> getStoreDiscounts(string userID, string storeId);

  /* assign a discount policy to a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to create store policies.*/
  void assignStoreDiscountPolicy(int discountId, string userID, string storeId);

  /* remove discount policy of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  void removeDiscount(string userID, string storeId, int discountId);

  /* create quantity discount of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  int makeQuantityDiscount(string userID, string storeId, int discount, Collection<string> items, int policyId);

  /* create plus discount between two discount policies of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  int makePlusDiscount(string userID, string storeId, int discountId1, int discountId2);

  /* create max discount policy between two discount policies of a store.
  preconditions: invoker is the store owner or is a manager of it, with permissions to remove store policies.*/
  int makeMaxDiscount(string userID, string storeId, int discountId1, int discountId2);

  //end of block dealing with store policies
  //******************************************************************************


  /*The next block of functions deals with store manager permissions. A new store manager has only the
      basic permissions in the store. */
  //******************************************************************************

  /* allows manager to add, delete amd update product in a specific store.
   precondition: assignor is the assignor of the manager, assignee is a manager of the store
   postcondition: the manager has permissions to add, delete amd update product in the store. */
  void allowManagerToUpdateProducts(string userID, string storeId, string managerUserName);

  /* disables a manager from adding, deleting amd updating product in a specific store.
   pre-condition: assignor is the assignor of the manager
   postcondition: the manager DOESN'T have permissions to add, delete amd update product in the store. */
  void disableManagerFromUpdateProducts(string userID, string storeId, string managerUserName);

  /* allows manager to get info and edit purchase and discount policies in a specific store.
   precondition: assignor is the assignor of the manager.
   postcondition: the manager has permissions to get info and edit purchase and discount policies in the store. */
  void allowManagerToEditPolicies(string userID, string storeId, string managerUserName);

  /* disables a manager from getting info and editing purchase and discount policies in a specific store.
   pre-condition: assignor is the assignor of the manager
   postcondition: the manager DOESN'T have permissions to get info and edit purchase and discount policies in the store. */
  void disableManagerFromEditPolicies(string userID, string storeId, string managerUserName);

  /* allows manager to get purchases history of the store.
   precondition: assignor is the assignor of the manager. managerUserName is a subscriber and a manager of the store.
   postcondition: the manager has permissions to get purchases history of the store. */
  void allowManagerToGetHistory(string userID, string storeId, string managerUserName);

  /* disables a manager from getting purchases history of the store.
   pre-condition: assignor is the assignor of the manager
   postcondition: the manager DOESN'T have permissions to get purchases history of the store. */
  void disableManagerFromGetHistory(string userID, string storeId, string managerUserName);

  //end of block dealing with store manager permissions
  //******************************************************************************


  /* removes a user from a store manager role.
   * pre-condition: the invoker is an owner of the store and is the assignor of the manager*/
  //returns true if manager removed, else returns false.
  bool removeManager(string userID, string storeId, string managerUserName);

  /* removes a user from a store owner role.
   * pre-condition: the invoker is an owner of the store and is the assignor of the owner */
  //returns true if manager removed, else returns false.
  bool removeOwner(string connId, string storeId, string targetUserName);

  /* shows store staff information and their permissions in the store
  precondition: invoker has the permissions to get the info. */
  //every string element in the collection represents one staff member username and his permissions.
  ICollection<string> showStaffInfo(string userID, string storeId);

  /* shows sales History of a specific store by permissions: system manager / store owner / store manager.
  precondition: invoker has the permissions to get the info. */
  //every string element in the collection represents a purchase of a basket, with the quantity that was sale to a specific user.
  Collection<string> getSalesHistoryByStore(string userID, string storeId);

  // ***********************************************************************
  // Topics: service level, external systems
  // ***********************************************************************

  /* shows the event log.
     every string element represents an event, which is an application to the system and its parameters.
     precondition: invoker has the permissions to get the info - only system manager. */
  Collection<string> getEventLog(string userID);

  /* shows the error log.
     every string element represents an error.
     precondition: invoker has the permissions to get the info - only system manager. */
  Collection<string> getErrorLog(string userID);
 }
}