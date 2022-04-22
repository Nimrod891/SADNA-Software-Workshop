using System;
namespace Userpack;
using StorePack;
using externalService;
using policies;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Collections;
public class Subscriber : User {

    private int id;
    private string userName;
    private  HashSet<AbsPermission> permissions; // synchronized manually
    private  ConcurrentDictionary<Store, ArrayList<Product>> itemsPurchased;
    private  ArrayList<string> purchaseHistory; // synchronized in constructor
    private List<Notification> notifications;

    public Subscriber(int id, string userName) {
       this.id = id;
       this.userName = userName;
       this.permissions = new HashSet<AbsPermission>();
       this.itemsPurchased = new ConcurrentDictionary<Store, ArrayList<Product>>();
       this.purchaseHistory = new ArrayList<string>();
       this.notifications = new ArrayList<>();
    }

    Subscriber(int id, string userName, HashSet<Permission> permissions, ConcurrentDictionary<Store, ArrayList<Product>> itemsPurchased, ArrayList<string> purchaseHistory) {
        this.id = id;
        this.userName = userName;
        this.permissions = permissions;
        this.itemsPurchased = itemsPurchased;
        this.purchaseHistory = ArrayList<string>.Synchronized(purchaseHistory);
        this.notifications = new ArrayList<>();
    }

    public string getUserName() {
        return userName;
    }

     public Subscriber getSubscriber() {
        return this;
    }

   
    public void addCartToPurchases(Dictionary<Store,string> details) { // TODO unit test

        foreach (Dictionary<Store, Basket>.Enumerator entry in this.baskets) {
            Store store = entry.Current.Key;
            Basket basket = entry.Current.Value;
            ArrayList<Product> itemsPurchasedFromStore = computeIfAbsent(itemsPurchased, store, k=> new ArrayList<Product>());
            itemsPurchasedFromStore.AddRange(basket.getItems().Keys);
        }

        // add each store purchase details string to the user's purchase history collection
        string cartPurchase = "";
        foreach (Dictionary<Store, string>.Enumerator entry in details)
            cartPurchase += "Store: " + entry.Current.Key.GetType().Name + "\n" + entry.Current.Value;
        purchaseHistory.Add(cartPurchase);

    }

    public void addPermission(AbsPermission permission) {

        lock (permissions)
        {
         permissions.Add(permission);
        }
    }

    public void removePermission(AbsPermission permission) {

        lock (permissions) {
            permissions.Remove(permission);
        }
    }

    public bool havePermission(AbsPermission permission) {

        lock (permissions) {
            return permissions.Contains(permission);
        }
    }

    public void validatePermission(AbsPermission permission) {

        lock (permissions) {
            if (!havePermission(permission))
                throw NoPermissionException(permission.toString());
        }
    }

    public void validateAtLeastOnePermission(ArrayList<AbsPermission> permissionss)  {

        lock (this.permissions) {
            foreach (AbsPermission per in permissionss) {
                if (havePermission(per))
                    return;
            }
            throw  NoPermissionException(ToString(permissionss));
        }
    }

    public void addManagerPermission(Subscriber target, Store store) {

        lock (target.id < id ? target.permissions : permissions) {
            lock (target.id < id ? permissions : target.permissions) {

                // check this user has the permission to perform this action
                validatePermission(OwnerPermission.getInstance(store));

                // check if the target is already a manager at this store
                Permission managerPermission = ManagerPermission.getInstance(store);
                if (target.havePermission(managerPermission))
                    throw AlreadyManagerException(userName);

                // add manager permission to the target
                target.addPermission(managerPermission);

                // give the user permission to delete the new permission that was added to the target
                addPermission(AppointerPermission.getInstance(target, store));
            }
        }
    }

    public void removeManagerPermission(Subscriber target, Store store) {

        removeOwnerPermission(target, store); // removes all store permissions
    }

    public void addOwnerPermission(Store store) {

        lock (permissions) {

            addPermission(OwnerPermission.getInstance(store));
            addPermission(ManagerPermission.getInstance(store));
            addPermission(EditPolicyPermission.getInstance(store));
            addPermission(ManageInventoryPermission.getInstance(store));
            addPermission(GetHistoryPermission.getInstance(store));
        }
    }

    public void addOwnerPermission(Subscriber target, Store store) {

        lock (target.id < id ? target.permissions : permissions) {
            lock (target.id < id ? permissions : target.permissions) {

                // check this user has the permission to perform this action
                Permission ownerPermission = OwnerPermission.getInstance(store);
                validatePermission(ownerPermission);

                // check if the target is already an owner at this store
                if (target.havePermission(ownerPermission))
                    throw  AlreadyOwnerException(userName);

                // check if the target is a manager that was appointed by someone else
                ManagerPermission managerPermission = ManagerPermission.getInstance(store);
                if (target.havePermission(managerPermission))
                    validatePermission(AppointerPermission.getInstance(target, store));

                target.addOwnerPermission(store);

                // give the user permission to delete the new permission that was added to the target
                addPermission(AppointerPermission.getInstance(target, store));
            }
        }
    }

    public void removeOwnerPermission(Store store) {

        lock (permissions) {

            ArrayList<AbsPermission> permissionsToRemove = new ArrayList<AbsPermission>();

            // look for any managers or owners that were appointed by this owner for this store and remove their permission
            foreach (AbsPermission per in permissions)
                if (per.GetType().Name == AppointerPermission.GetType().Name && ((AppointerPermission)permission).getStore() == store) {
                    Subscriber target = ((AppointerPermission)per).getTarget();
                    target.removeOwnerPermission(store);

                    permissionsToRemove.Add(permission); // store this permission to remove it after the foreach loop
                }

                foreach(AbsPermission per in permissionsToRemove)
                {
                    if(permissions.Contains(per)){
                        permissions.Remove(per);
                        }
                }

                permissionsToRemove.Clear();

            removePermission(OwnerPermission.getInstance(store));
            removePermission(EditPolicyPermission.getInstance(store));
            removePermission(ManageInventoryPermission.getInstance(store));
            removePermission(GetHistoryPermission.getInstance(store));
            removePermission(ManagerPermission.getInstance(store));
        }
    }

    public void removeOwnerPermission(Subscriber target, Store store) {

        lock (target.id < id ? target.permissions : permissions) {
            lock (target.id < id ? permissions : target.permissions) {

                // check this user has the permission to perform this action
                validatePermission(AppointerPermission.getInstance(target, store));

                target.removeOwnerPermission(store);

                // remove this user's permission to change the target's permissions
                removePermission(AppointerPermission.getInstance(target, store));
            }
        }
    }

    public void addInventoryManagementPermission(Subscriber target, Store store) {

        addPermissionToManager(target, store, ManageInventoryPermission.getInstance(store));
    }

    public void removeInventoryManagementPermission(Subscriber target, Store store){

        removePermissionFromManager(target, store, ManageInventoryPermission.getInstance(store));
    }

    public void addEditPolicyPermission(Subscriber target, Store store) {

        addPermissionToManager(target, store, EditPolicyPermission.getInstance(store));
    }

    public void removeEditPolicyPermission(Subscriber target, Store store){

        removePermissionFromManager(target, store, EditPolicyPermission.getInstance(store));
    }

    public void addGetHistoryPermission(Subscriber target, Store store) {

        addPermissionToManager(target, store, GetHistoryPermission.getInstance(store));
    }

    public void removeGetHistoryPermission(Subscriber target, Store store) {

        removePermissionFromManager(target, store, GetHistoryPermission.getInstance(store));
    }

    void addPermissionToManager(Subscriber target, Store store, Permission permission){

        lock (target.id < id ? target.permissions : permissions) {
            lock (target.id < id ? permissions : target.permissions) {

                // check this user has the permission to perform this action
                validatePermission(AppointerPermission.getInstance(target, store));

                if (!target.havePermission(ManagerPermission.getInstance(store)))
                    throw TargetIsNotManagerException(target.getUserName(), store.getName());

                // add the permission to the target (if he doesn't already have it)
                target.addPermission(permission);
            }
        }
    }

    void removePermissionFromManager(Subscriber target, Store store, Permission permission){

        lock (target.id < id ? target.permissions : permissions) {
            lock (target.id < id ? permissions : target.permissions) {

                // check this user has the permission to perform this action
                validatePermission(AppointerPermission.getInstance(target, store));

                if (target.havePermission(OwnerPermission.getInstance(store)))
                    throw  TargetIsOwnerException(target.getUserName(), store.getName());

                target.removePermission(permission);
            }
        }
    }

    public int addStoreItem(Store store, string itemName, string category, string subCategory, int quantity, double price) {

        // check this user has the permission to perform this action
        validatePermission(ManageInventoryPermission.getInstance(store));

        return store.addItem(itemName, price, category, subCategory, quantity);
    }

    public void removeStoreItem(Store store, int itemId)  {

        // check this user has the permission to perform this action
        validatePermission(ManageInventoryPermission.getInstance(store));

        store.removeItem(itemId);
    }

    public void updateStoreItem(Store store, int itemId, string newSubCategory, Int64 newQuantity, Double newPrice) {

        // check this user has the permission to perform this action
        validatePermission(ManageInventoryPermission.getInstance(store));

        store.changeItem(itemId,  newSubCategory, newQuantity, newPrice);
    }

    public Collection<Store> getAllStores(Collection<Store> stores){

        // check this user has the permission to perform this action
        validatePermission(AdminPermission.getInstance());

        return stores;
    }

    public Collection<Item> getStoreItems(Store store){

        // check this user has the permission to perform this action
        validateAtLeastOnePermission(AdminPermission.getInstance(), ManagerPermission.getInstance(store));

        return store.getItems().keySet();
    }

    public String storePermissionsToString(Store store) {

        lock (permissions) {

            StringBuilder result = new StringBuilder();

            Permission ownerPermission = OwnerPermission.getInstance(store);
            Permission managerPermission = ManagerPermission.getInstance(store);
            Permission manageInventoryPermission = ManageInventoryPermission.getInstance(store);
            Permission getHistoryPermission = GetHistoryPermission.getInstance(store);
            Permission editPolicyPermission = EditPolicyPermission.getInstance(store);

            if (havePermission(ownerPermission))
                result.Append(ownerPermission.toString()).Append(" ");
            if (havePermission(managerPermission))
                result.Append(managerPermission.toString()).Append(" ");
            if (havePermission(manageInventoryPermission))
                result.Append(manageInventoryPermission.toString()).Append(" ");
            if (havePermission(getHistoryPermission))
                result.Append(getHistoryPermission.toString()).Append(" ");
            if (havePermission(editPolicyPermission))
                result.Append(editPolicyPermission.toString()).Append(" ");

            return result.toString();
        }
    }

    public ArrayList<string> getEventLog(ArrayList<string> log) {

        // check this user has the permission to perform this action
        validatePermission(AdminPermission.getInstance());

        return log;
    }

    public ArrayList<string> getSalesHistoryByStore(Store store) {

        validateAtLeastOnePermission(AdminPermission.getInstance(), GetHistoryPermission.getInstance(store));

        return store.getPurchaseHistory();
    }

    public ArrayList<string> getPurchaseHistory() {

        return new ArrayList<>(purchaseHistory);
    }

    public void writeOpinionOnProduct(Store store, int itemId, string review) {

        if (review == null || review.trim().isEmpty())
            throw  WrongReviewException("Review can't be empty or null");

        Product item = store.searchItemById(itemId);
        if (!itemsPurchased.get(store).contains(item))
            throw  ItemNotPurchasedException("Item ID: " + itemId + " item name: " + item.getName());

        Review review1 = new Review(this, store, item, review);
        item.addReview(review1);
        store.notifyItemOpinion(review1);

    }

    public void subscribe(Store store){
        store.subscribe(this);
    }

    public void unsubscribe(Store store){
        store.unsubscribe(this);

    }

    //todo: should we return notifications? hot to connect it to the GUI?
//    public PurchaseNotification notifyObserverPurchase(PurchaseNotification notification) {
//        //todo: decide if to postpone the notification
//        return notification;
//    }
//
//    public StoreStatusNotification notifyObserverStoreStatus(StoreStatusNotification notification) {
//        //todo: decide if to postpone the notification
//        return notification;
//    }
//
//    public ItemReviewNotification notifyObserverItemReview(ItemReviewNotification notification) {
//        //todo: decide if to postpone the notification
//        return notification;
//    }
//
//    public void notifyObserverLotteryStatus() {
//        //todo: implement
//
//    }
//
//    public MessageNotification notifyObserverMessage(MessageNotification notification){
//        //todo: implement
//        return notification;
//    }
//
//    public SubscriberRemoveNotification notifyObserverSubscriberRemove(SubscriberRemoveNotification notification){
//        //todo: implement
//        return notification;
//    }

    public Notification notifyNotification(Notification notification){
        //todo: implement
        return notification;
    }

    public ArrayList<Notification> checkPendingNotifications() {
        ArrayList<Notification> collection = new ArrayList<>();
        foreach (Notification n in this.notifications) {
            if(n.isShown() == false){
                collection.Add(n);
                n.setShown(true);
            }
        }
        return collection;
    }

    private static V ComputeIfAbsent<K, V>(this Dictionary<K, V> dict, K key, Func<K, V> generator) {
    bool exists = dict.TryGetValue(key, out var value);
    if (exists) {
        return value;
    }
    var generated = generator(key);
    dict.Add(key, generated);
    return generated;
}
}


