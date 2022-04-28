//
//
// using System;
// using System.Collections.Generic;
// using System.Text;
// using System.Windows;
// namespace acceptanceTests{
//     public class ServiceProxy : TradingSystemService {
//         private TradingSystemService real;
//
//         public ServiceProxy()
//         {}
//
//         public void setReal(TradingSystemService real) {
//             this.real = real;
//         }
//
//         public override override String connect() {
//             try {
//                 if(real != null)
//                     return real.connect();
//                 return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//             
//         }
//
//         public override void register(String userName, String password) {
//             try {
//                 if(real != null)
//                     real.register(userName, password);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override override void login(String userID, String userName, String pass) {
//             try {
//                 if(real != null)
//                     real.login(userID, userName, pass);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void logout(String userID) {
//             try {
//                 if(real != null)
//                     real.logout(userID);
//         }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override Collection<String> getItems(String keyWord, String productName, String category, String subCategory, Double ratingItem, Double ratingStore, Double maxPrice, Double minPrice) {
//             try {
//                 if(real != null)
//                     return real.getItems(keyWord, productName, category, subCategory, ratingItem, ratingStore, maxPrice, minPrice);
//                 return null;
//                 }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void addItemToBasket(String userID, String storeId, String productId, int amount) {
//             try {
//                 if(real != null)
//                     real.addItemToBasket(userID, storeId, productId, amount);
//         }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override Collection<String> showCart(String userID) {
//             try {
//                 if(real != null)
//                     return real.showCart(userID);
//             return null;
//         }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override Collection<String> showBasket(String userID, String storeId) {
//             try {
//                 if(real != null)
//                     return real.showBasket(userID, storeId);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void updateProductAmountInBasket(String userID, String storeId, String productId, int newAmount) {
//             try {
//                 if(real != null)
//                 real.updateProductAmountInBasket(userID, storeId, productId, newAmount);
//         }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void purchaseCart(String userID) {
//             try {
//                 if(real != null)
//                     real.purchaseCart(userID);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override Collection<String> getPurchaseHistory(String userID) {
//             try {
//                 if(real != null)
//                     return real.getPurchaseHistory(userID);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void writeOpinionOnProduct(String userID, String storeID, String productId, String desc) {
//             try {
//                 if(real != null)
//                     real.writeOpinionOnProduct(userID, storeID, productId, desc);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override Collection<String> getStoresInfo(String userID) {
//             try {
//                 if(real != null)
//                     return real.getStoresInfo(userID);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override Collection<String> getItemsByStore(String userID, String storeId) {
//             try {
//                 if(real != null)
//                     return real.getItemsByStore(userID, storeId);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override String openNewStore(String userID, String newStoreName) {
//             try {
//                 if(real != null)
//                     return real.openNewStore(userID, newStoreName);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void appointStoreManager(String userID, String assigneeUserName, String storeId) {
//             try {
//                 if(real != null)
//                     real.appointStoreManager(userID, assigneeUserName, storeId);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override String addProductToStore(String userID, String storeId, String productName, String category, String subCategory, int quantity, double price) {
//             try {
//                 if(real != null)
//                     return real.addProductToStore(userID, storeId, productName, category, subCategory, quantity, price);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void deleteProductFromStore(String userID, String storeId, String productID) {
//             try {
//                 if(real != null)
//                     real.deleteProductFromStore(userID, storeId, productID);
//                     }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void updateProductDetails(String userID, String storeId, String productID, String newSubCategory, Integer newQuantity, Double newPrice) {
//             try {
//                 if(real != null)
//                     real.updateProductDetails(userID, storeId, productID, newSubCategory, newQuantity, newPrice);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void appointStoreOwner(String userID, String assigneeUserName, String storeId) {
//             try {
//                 if(real != null)
//                     real.appointStoreOwner(userID, assigneeUserName, storeId);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void allowManagerToUpdateProducts(String userID, String storeId, String managerUserName) {
//             try {
//                 if(real != null)
//                     real.allowManagerToUpdateProducts(userID, storeId, managerUserName);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void disableManagerFromUpdateProducts(String userID, String storeId, String managerUserName) {
//             try {
//                 if(real != null)
//                     real.disableManagerFromUpdateProducts(userID, storeId, managerUserName);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void allowManagerToEditPolicies(String userID, String storeId, String managerUserName) {
//             try {
//                 if(real != null)
//                     real.allowManagerToEditPolicies(userID, storeId, managerUserName);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void disableManagerFromEditPolicies(String userID, String storeId, String managerUserName) {
//             try {
//                 if(real != null)
//                     real.disableManagerFromEditPolicies(userID, storeId, managerUserName);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void allowManagerToGetHistory(String userID, String storeId, String managerUserName) {
//             try {
//                 if(real != null)
//                     real.allowManagerToGetHistory(userID, storeId, managerUserName);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         public override void disableManagerFromGetHistory(String userID, String storeId, String managerUserName) {
//             try {
//                 if(real != null)
//                     real.disableManagerFromGetHistory(userID, storeId, managerUserName);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override boolean removeManager(String userID, String storeId, String managerUserName) {
//             try {
//                 if(real != null)
//                     return real.removeManager(userID, storeId, managerUserName);
//             return false;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override boolean removeOwner(String userID, String storeId, String targetUserName) {
//             try {
//                 if(real != null)
//                     return real.removeOwner(userID, storeId, targetUserName);
//             return false;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override Collection<String> showStaffInfo(String userID, String storeId) {
//             try {
//                 if(real != null)
//                     return real.showStaffInfo(userID, storeId);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override Collection<String> getSalesHistoryByStore(String userID, String storeId) {
//             try {
//                 if(real != null)
//                     return real.getSalesHistoryByStore(userID, storeId);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override Collection<Integer> getStorePolicies(String userID, String storeId) {
//             try {
//                 if(real != null)
//                     return real.getStorePolicies(userID, storeId);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override void assignStorePurchasePolicy(int policy, String userID, String storeId) {
//             try {
//                 if(real != null)
//                     real.assignStorePurchasePolicy(policy, userID, storeId);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override void removePolicy(String userID, String storeId, int policy) {
//             try {
//                 if(real != null)
//                     real.removePolicy(userID, storeId, policy);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override int makeQuantityPolicy(String userID, String storeId, Collection<String> items, int minQuantity, int maxQuantity) {
//             try {
//                 if(real != null)
//                     return real.makeQuantityPolicy(userID, storeId, items, minQuantity, maxQuantity);
//             return -1;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override int makeBasketPurchasePolicy(String userID, String storeId, int minBasketValue) {
//             try {
//                 if(real != null)
//                     return real.makeBasketPurchasePolicy(userID, storeId, minBasketValue);
//             return -1;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override int makeTimePolicy(String userID, String storeId, Collection<String> items, String time) {
//             try {
//                 if(real != null)
//                     return real.makeTimePolicy(userID, storeId, items, time);
//             return -1;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override int andPolicy(String userID, String storeId, int policy1, int policy2) {
//             try {
//                 if(real != null)
//                     return real.andPolicy(userID, storeId, policy1, policy2);
//             return -1;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override int orPolicy(String userID, String storeId, int policy1, int policy2) {
//             try {
//                 if(real != null)
//                     return real.orPolicy(userID, storeId, policy1, policy2);
//             return -1;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override int xorPolicy(String userID, String storeId, int policy1, int policy2) {
//             try {
//                 if(real != null)
//                     return real.xorPolicy(userID, storeId, policy1, policy2);
//             return -1;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override Collection<Integer> getStoreDiscounts(String userID, String storeId) {
//             try {
//                 if(real != null)
//                     return real.getStoreDiscounts(userID, storeId);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override void assignStoreDiscountPolicy(int discountId, String userID, String storeId) {
//             try {
//                 if(real != null)
//                     real.assignStoreDiscountPolicy(discountId, userID, storeId);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override void removeDiscount(String userID, String storeId, int discountId) {
//             try {
//                 if(real != null)
//                     real.removeDiscount(userID, storeId, discountId);
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override int makeQuantityDiscount(String userID, String storeId, int discount, Collection<String> items, Integer policyId) {
//             try {
//                 if(real != null)
//                     return real.makeQuantityDiscount(userID, storeId, discount, items, policyId);
//             return -1;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override int makePlusDiscount(String userID, String storeId, int discountId1, int discountId2) {
//             try {
//                 if(real != null)
//                     return real.makePlusDiscount(userID, storeId, discountId1, discountId2);
//             return -1;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override int makeMaxDiscount(String userID, String storeId, int discountId1, int discountId2) {
//             try {
//                 if(real != null)
//                     return real.makeMaxDiscount(userID, storeId, discountId1, discountId2);
//             return -1;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override Collection<String> getEventLog(String userID) {
//             try {
//                 if(real != null)
//                     return real.getEventLog(userID);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//
//         
//         public override Collection<String> getErrorLog(String userID) {
//             try {
//                 if(real != null)
//                     return real.getErrorLog(userID);
//             return null;
//             }
//             catch (Exception e) {
//                 Log(e.ToString());  
//             } 
//         }
//     }
// }