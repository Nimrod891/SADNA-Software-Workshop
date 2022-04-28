// using System;
// namespace acceptanceTests{
// public class TradingSystemServiceMock : TradingSystemService {
//
//     
//     public String connect() {
//         try {
//             return null;
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void register(String userName, String password) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void login(String userID, String userName, String pass) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void logout(String userID) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public Collection<String> getItems(String keyWord, String productName, String category, String subCategory, Double ratingItem, Double ratingStore, Double maxPrice, Double minPrice) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public void addItemToBasket(String userID, String storeId, String productId, int amount) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public Collection<String> showCart(String userID) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public Collection<String> showBasket(String userID, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public void updateProductAmountInBasket(String userID, String storeId, String productId, int newAmount) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void purchaseCart(String userID) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public Collection<String> getPurchaseHistory(String userID) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public void writeOpinionOnProduct(String userID, String storeID, String productId, String desc) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public Collection<String> getStoresInfo(String userID) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public Collection<String> getItemsByStore(String userID, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public String openNewStore(String userID, String newStoreName) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public void appointStoreManager(String userID, String assigneeUserName, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public String addProductToStore(String userID, String storeId, String productName, String category, String subCategory, int quantity, double price) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public void deleteProductFromStore(String userID, String storeId, String productID) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void updateProductDetails(String userID, String storeId, String productID, String newSubCategory, Integer newQuantity, Double newPrice) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void appointStoreOwner(String userID, String assigneeUserName, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void allowManagerToUpdateProducts(String userID, String storeId, String managerUserName) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void disableManagerFromUpdateProducts(String userID, String storeId, String managerUserName) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void allowManagerToEditPolicies(String userID, String storeId, String managerUserName) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void disableManagerFromEditPolicies(String userID, String storeId, String managerUserName) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void allowManagerToGetHistory(String userID, String storeId, String managerUserName) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void disableManagerFromGetHistory(String userID, String storeId, String managerUserName) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public boolean removeManager(String userID, String storeId, String managerUserName) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return false;
//     }
//
//     
//     public boolean removeOwner(String connId, String storeId, String targetUserName) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return false;
//     }
//
//     
//     public Collection<String> showStaffInfo(String userID, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public Collection<String> getSalesHistoryByStore(String userID, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public Collection<Integer> getStorePolicies(String userID, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public void assignStorePurchasePolicy(int policy, String userID, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void removePolicy(String userID, String storeId, int policy) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public int makeQuantityPolicy(String userID, String storeId, Collection<String> items, int minQuantity, int maxQuantity) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return 0;
//     }
//
//     
//     public int makeBasketPurchasePolicy(String userID, String storeId, int minBasketValue) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return 0;
//     }
//
//     
//     public int makeTimePolicy(String userID, String storeId, Collection<String> items, String time) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return 0;
//     }
//
//     
//     public int andPolicy(String userID, String storeId, int policy1, int policy2) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return 0;
//     }
//
//     
//     public int orPolicy(String userID, String storeId, int policy1, int policy2) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return 0;
//     }
//
//     
//     public int xorPolicy(String userID, String storeId, int policy1, int policy2) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return 0;
//     }
//
//     
//     public Collection<Integer> getStoreDiscounts(String userID, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public void assignStoreDiscountPolicy(int discountId, String userID, String storeId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public void removeDiscount(String userID, String storeId, int discountId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
//
//     
//     public int makeQuantityDiscount(String userID, String storeId, int discount, Collection<String> items, Integer policyId) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return 0;
//     }
//
//     
//     public int makePlusDiscount(String userID, String storeId, int discountId1, int discountId2) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return 0;
//     }
//
//     
//     public int makeMaxDiscount(String userID, String storeId, int discountId1, int discountId2) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return 0;
//     }
//
//     
//     public Collection<String> getEventLog(String userID) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
//
//     
//     public Collection<String> getErrorLog(String userID) {
//         try {
//             // COMPLETE CODE
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         return null;
//     }
// }
// }