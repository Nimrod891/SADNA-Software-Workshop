using System;
using System;
namespace StorePack;
using System.Collections.Generic;
using System.Text;
using policies;
using System.Runtime;
using Userpack;
using spellingPack;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Collections;
namespace StorePack{
public class Store{

    private int id;
    private string name;
    private string description;
    private double rating;
    private DiscountPolicyIF discountPolicy;
    private PurchasePolicyIF purchasePolicy;
    //private String founder;
    private bool isActive;
    private  Inventory inventory = new Inventory();
    private  ArrayList<string> purchases = new ArrayList<>();
    //private Observable observable;

    public Store() {}

    /**
     * This method opens a new store and create its inventory
     *
     * @param name        - the name of the new store
     * @param description - the price of the new store
     *                    //  * @param founder - the fonder of the new store
     * @throws WrongNameException
     */
    public Store(int id, string name, string description, PurchasePolicyIF purchasePolicy, DiscountPolicyIF discountPolicy) {//, Observable observable) {
        if (name == null || name.isEmpty() || name.trim().isEmpty())
            throw  WrongNameException("store name is null or contains only white spaces");
        if (name.charAt(0) >= '0' && name.charAt(0) <= '9')
            throw  WrongNameException("store name cannot start with a number");
        if (description == null || description.Contains("") || description.Trim().isEmpty())
            throw  WrongNameException("store description is null or contains only white spaces");
        if (description.ToCharArray()[0] >= '0' && description.ToCharArray()[0] <= '9')
            throw  WrongNameException("store description cannot start with a number");
        this.id = id;
        this.name = name;
        this.description = description;
        this.rating = 0;
        // this.founder = founder; // TODO: should check how to implement
//        this.inventory = new Inventory(tradingSystem);
        if(purchasePolicy == null)
            this.purchasePolicy = new DefaultPurchasePolicy();
        else
            this.purchasePolicy = purchasePolicy;
        if(discountPolicy == null)
            this.discountPolicy = new DefaultDiscountPolicy(this.inventory.getItems().keySet());
        else
            this.discountPolicy = discountPolicy;
        this.isActive = true;
        //this.observable = observable;
    }

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public String getDescription() {
        return description;
    }

    public double getRating() {
        return rating;
    }

    public void setRating(double rating) {
        if (rating < 0)
            throw  WrongRatingException("rating must be a positive number");
        this.rating = rating;
    }

    public Inventory getInventory() {
        return inventory;
    }

    /**
     * This method returns the items in the store's inventory
     */
    public Dictionary<Product, int> getItems() {
        return this.inventory.getItems();
    }

//    /**
//     * This method changes an item's price in the store
//     * @param name - the name of the item
//     * @param price - the price of the item
//     * @param category - the category of the item
//     * @param subCategory - the sub category of the item
//     * @param price- the new price of the item
//     * @exception  ItemNotFound,WrongPrice  */
//    public void setItemPrice(String name, String category, String subCategory, double price) throws Exception {
//        this.inventory.setItemPrice(name, category, subCategory, price);
//    }

//    public void addItem(String name, double price, String category, String subCategory, double rating, int amount) throws Exception {
//        this.inventory.addItem(name, price, category, subCategory, rating, amount);
//    }

    /**
     * this adds a new item and it's amount to the store's inventory
     *
     * @param name        - the name of the new item
     * @param price       - the price of the new item
     * @param category    - the category of the new item
     * @param subCategory - the sub category of the new item
     * @param amount      the amount in the store for the new item
     * @throws WrongNameException,WrongPriceException,WrongAmountException,WrongCategoryException,ItemAlreadyExistsException
     */
    public int addItem(string name, double price, string category, string subCategory, int amount) {
        return this.inventory.addItem(name, price, category, subCategory, amount);
    }

//    /**
//     * This method is used to search the store's inventory for items that matches the param name.
//     * @param name - the name of the wanted item*/
//    public ConcurrentLinkedQueue<Item> searchItemByName(String name) {
//        return this.inventory.searchItemByName(name);
//    }

//    /**
//     * This method is used to search the store's inventory for items that matches the param category.
//     * @param category - the category of the wanted item */
//    public ConcurrentLinkedQueue<Item> searchItemByCategory(String category) {
//        return this.inventory.searchItemByCategory(category);
//    }
//
//    /**
//     * This method is used to search the store's inventory for items that matches the param keyword.
//     * @param keyword - the keyword of the wanted item */
//    public ConcurrentLinkedQueue<Item> searchItemByKeyWord(String keyword)  {
//        return this.inventory.searchItemByKeyWord(keyword);
//    }


    public ICollection<Product> searchAndFilter(string keyWord, string itemName, string category, double ratingItem,
                                                       double ratingStore, double maxPrice, double minPrice) {
        Spelling spelling = new Spelling();
        if(keyWord != null)
            keyWord = spelling.correct(keyWord.toLowerCase());
        if(itemName != null)
            itemName = spelling.correct(itemName.toLowerCase());
        if(category != null)
            category = spelling.correct(category.toLowerCase());
        CollectionBase<Product> search = searchItems(keyWord, itemName, category);
        return filterItems(search, ratingItem, ratingStore, maxPrice, minPrice);
    }

    //    /**
//     * This method searches the store's inventory for an item
//     * @param name - the name of the item
//     * @param category - the category of the item
//     * @param subCategory - the sub category of the item
//     * @exception  ItemNotFound  */
    public ICollection<Product> searchItems(string keyWord, string itemName, string category) {

        ICollection<Product> result = new HashSet<>(inventory.getItems().Keys);
        bool itemValue = itemName != null && !itemName.Trim().Equals("");
        bool categoryValue = category != null && !category.Trim().Equals("");
        bool keyWordValue = keyWord != null && !keyWord.Trim().Equals("");
        if(!itemValue && !categoryValue && !keyWordValue)
            return result;
        if (itemValue)
            result.retainAll(inventory.searchItemByName(itemName));
        if (categoryValue)
            result.retainAll(inventory.searchItemByCategory(category));
        if (keyWordValue)
            result.retainAll(inventory.searchItemByKeyWord(keyWord));
        return result;
    }


    public ICollection<Product> filterItems(ICollection<Product> p, double ratingItem, double ratingStore,
                                                   double maxPrice, double minPrice) {
        ICollection<Product> result = new List<Product>(p);
        if(ratingItem != null)
            result.retainAll(inventory.filterByRating(items, ratingItem));
        if(ratingStore != null && rating < ratingStore)
            result = new HashSet<>();
        if(minPrice != null && maxPrice != null)
            result.retainAll(inventory.filterByPrice(items, minPrice, maxPrice));
        return result;

    }

    /**
     * This method searches the inventory by name, category and sub-Category
     *
     * @param name        - name of the wanted item
     * @param category    - the category of the wanted item
     * @param subCategory - the sub category of the wanted item
     * @throws ItemNotFoundException - when there are no item that matches the giving parameters.
     */
    public Product getItem(string name, string category, string subCategory) {
        return this.inventory.getItem(name, category, subCategory);
    }

    /**
     * This method searches the store's inventory by name, category and sub-Category
     *
     * @param itemId- id of the wanted item
     * @throws ItemNotFoundException - when there are no item that matches the giving parameters.
     */
    public Product searchItemById(int productId)   {
        return this.inventory.searchItem(productId);
    }

//    /**
//     * This method is used to filter the store's inventory for items that their price is between start price and end price.
//     * @param startPrice - the startPrice of the items price
//     * @param endPrice - the endPrice of the items price
//     * @exception ItemNotFoundException - On non existing item with params startPrice and endPrice*/
//    public ConcurrentLinkedQueue<Item> filterByPrice(double startPrice, double endPrice) throws ItemException {
//        return this.inventory.filterByPrice(startPrice, endPrice);
//    }

    //    /**
//     * This method is used to filter the store's inventory for items that their price is between start price and end price.
//     *
//     *  @param startPrice - the startPrice of the items price
//     * @param endPrice - the endPrice of the items price */
    public CollectionBase<Product> filterByPrice(CollectionBase<Product> products, double startPrice, double endPrice) {
        if (products != null)
            return this.inventory.filterByPrice(products, startPrice, endPrice);
        return this.inventory.filterByPrice(startPrice, endPrice);
    }

    //    /**
//     * This method is used to filter the store's inventory for items that their ratings are equal or above the giving rating.
//     * @param rating - the keyword of the wanted item
//     * @exception ItemNotFoundException - On non existing item with param rating or greater*/
    public CollectionBase<Product> filterByRating(CollectionBase<Product> products, double rating) {
        if (products != null)
            return this.inventory.filterByRating(items, rating);
        return this.inventory.filterByRating(rating);
    }

//    /**
//     * This method changes the amount of an item in the store's inventory
//     * @param name - name of the wanted item
//     * @param category - category of the wanted item
//     * @param subCategory - the sub category of the wanted item
//     * @param amount - the new amount fo the item
//     * @exception WrongAmount when the amount is illegal*/
//    public void changeQuantity(String name, String category, String subCategory, int amount) throws ItemException {
//        this.inventory.changeQuantity(name, category, subCategory, amount);
//    }

    /**
     * This method checks if there is enough amount of an item in the inventory
     *
     * @param itemId - id of the item in the inventory
     * @param amount - the amount of the item to check
     * @throws WrongAmountException when the amount is illegal
     */
    public bool checkAmount(int productId, int amount)  {
        return this.inventory.checkAmount(productId, amount);
    }

    //    /**
//     * This method decreases the amount of the item in the store's inventory by param quantity.
//     * @param name - name of the wanted item
//     * @param category - category of the wanted item
//     * @param subCategory - the sub category of the wanted item
//     * @param quantity - the quantity of the wanted item
//     * @exception WrongAmountException - when the amount is illegal */
//    public void decreaseByQuantity(int itemId, int quantity) throws ItemException {
//        this.inventory.decreaseByQuantity(itemId, quantity);
//    }

    /**
     * This method removes an item from the store's inventory
     *
     * @param itemID- id of the item
     * @throws ItemNotFoundException - when the wanted item does not exist in the inventory
     */
    public Product removeItem(int productId) {
        return this.inventory.removeItem(productId);
    }

    /**
     * This method displays the items in the store's inventory
     * * @param name - name of the wanted item
     */
    public string toString() {
        return inventory.toString();
    }

    public PurchasePolicyIF getPurchasePolicy() {
        return purchasePolicy;
    }

    public DiscountPolicyIF getDiscountPolicy() {
        return discountPolicy;
    }

    public void setDiscountPolicy(DiscountPolicyIF discountPolicy) { this.discountPolicy = discountPolicy; }

    public void setPurchasePolicy(PurchasePolicyIF purchasePolicy) { this.purchasePolicy = purchasePolicy; }


    public void changeItem(int itemID, string newSubCategory, int newQuantity, double newPrice) {
        this.inventory.changeItemDetails(itemID, newSubCategory, newQuantity, newPrice);
    }

    public bool ifActive() {
        return isActive;
    }

    public void setNotActive(){
        if(this.isActive == false)
            return;
        this.isActive = false;
       // observable.notifyStoreStatus(isActive);
    }

    public void setActive(){
        if(this.isActive == true)
            return;
        this.isActive = true;
       // observable.notifyStoreStatus(isActive);
    }

    //TODO remember to deal with policies and types in a furure version
    public double processBasketAndCalculatePrice(Basket basket, StringBuilder details, DiscountPolicyIF storeDiscountPolicy) { // TODO should get basket
        return inventory.calculate(basket, details, storeDiscountPolicy);
    }

    public void rollBack(Dictionary<Product, int> products) {
        foreach (Dictionary<Product, int>.Enumerator entry in products) {    
          inventory.getItems()[entry.Current.Key] = inventory.getItems().get(entry.Current.Key) + entry.Current.Value;
        }
        unlockItems(items.keySet());
    }

    public void unlockItems(ArrayList<Product> products) {
        foreach (Product p in products) 
            p.unlock();
    }
}
/*
    public Observable getObservable() {
        return observable;
    }

    public void addPurchase(string purchaseDetails) {
        this.purchases.Add(purchaseDetails);
    }

    public ArrayList<string> getPurchaseHistory() { return purchases; }

    public void notifyPurchase(User buyer, Dictionary<Product, int> basket) {
        observable.notifyPurchase(buyer, basket);
    }

    public void subscribe(Subscriber subscriber) {
        observable.subscribe(subscriber);
    }

    public void unsubscribe(Subscriber subscriber) {
        observable.unsubscribe(subscriber);
    }

    public void notifyItemOpinion(Review review) {
        observable.notifyItemReview(review);
    }

    public void setObservable(Observable observable) { this.observable = observable; }

} */
}
