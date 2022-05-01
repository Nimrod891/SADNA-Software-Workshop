using System;
using StorePack;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using ConsoleApplication1.Permmissions;

using java.util;
using java.util.concurrent;
using ArrayList = System.Collections.ArrayList;


namespace Userpack {
    public class Subscriber : Vistor {

        private int id;
        private string userName;
        private HashSet<AbsPermission> permissions; // synchronized manually
        private ConcurrentHashMap itemsPurchased; // k: store v: arrylist<product>
        private ArrayList purchaseHistory; // synchronized in constructor // arrylist<string>
        //private List<Notification> notifications;

        public Subscriber(int id, string userName) {
            this.id = id;
            this.userName = userName;
            this.permissions = new HashSet<AbsPermission>();
            this.itemsPurchased = new ConcurrentHashMap();// k: store v: arrylist<product>
            this.purchaseHistory = new ArrayList();
          //  this.notifications = new ArrayList<>();
        }

        public Subscriber(int id, string userName, HashSet<AbsPermission> permissions, ConcurrentHashMap itemsPurchased, ArrayList purchaseHistory) {
            this.id = id;
            this.userName = userName;
            this.permissions = permissions;
            this.itemsPurchased = itemsPurchased;
            this.purchaseHistory = ArrayList.Synchronized(purchaseHistory);
            //this.notifications = new ArrayList<>();
        }

        public string getUserName() {
            return userName;
        }

        public Subscriber getSubscriber() {
            return this;
        }


        public void addCartToPurchases(Dictionary<Store, string> details) { // TODO unit test

            foreach (KeyValuePair<Store, Basket> entry in this.baskets) {
                var store = entry.Key;
                var basket = entry.Value;
                
                var itemsPurchasedFromStore = (ArrayList)itemsPurchased.putIfAbsent(store, new ArrayList());
                itemsPurchasedFromStore.AddRange((ICollection)basket.getItems().keys());
            }

            // add each store purchase details string to the user's purchase history collection
            var cartPurchase = "";
            foreach (var entry in details)
                cartPurchase += "Store: " + entry.Key.GetType().Name + "\n" + entry.Value;
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
                    throw new Exception ("NoPermissionException: " +permission.ToString());
            }
        }

        public void validateAtLeastOnePermission(ArrayList permissionss/* arraylist<AbsPermmisionss"*/) {

            lock (this.permissions) {
                foreach (AbsPermission per in permissionss) {
                    if (havePermission(per))
                        return;
                }
                throw new Exception("NoPermissionException: "+ permissionss.ToString());
            }
        }

        public void addManagerPermission(Subscriber target, Store store) {

            lock (target.id < id ? target.permissions : permissions) {
                lock (target.id < id ? permissions : target.permissions) {

                    // check this user has the permission to perform this action
                    validatePermission(new OwnerPermission(store));

                    // check if the target is already a manager at this store
                    AbsPermission managerPermission = new ManagerPermission(store);
                    if (target.havePermission(managerPermission))
                        throw new Exception("AlreadyManagerException: " +(userName));

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

                addPermission(new OwnerPermission(store));
                addPermission(new ManagerPermission(store));
                addPermission(new ManageInventoryPermission(store));
                addPermission(new GetHistoryPermission(store));
            }
        }

        public void addOwnerPermission(Subscriber target, Store store) {

            lock (target.id < id ? target.permissions : permissions) {
                lock (target.id < id ? permissions : target.permissions) {

                    // check this user has the permission to perform this action
                    AbsPermission ownerPermission = new OwnerPermission(store);
                    validatePermission(ownerPermission);

                    // check if the target is already an owner at this store
                    if (target.havePermission(ownerPermission))
                        throw new Exception("AlreadyOwnerException: " + (userName));

                    // check if the target is a manager that was appointed by someone else
                    ManagerPermission managerPermission = new ManagerPermission(store);
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

                ArrayList permissionsToRemove = new ArrayList(); // ARRAYLIST<Abspermission>

                // look for any managers or owners that were appointed by this owner for this store and remove their permission
                foreach (AbsPermission per in permissions)
                    if (per.GetType().Name == typeof(AppointerPermission).Name && ((AppointerPermission) per).getStore() == store) {
                        Subscriber target = ((AppointerPermission)per).getTarget();
                        target.removeOwnerPermission(store);

                        permissionsToRemove.Add(per); // store this permission to remove it after the foreach loop
                    }

                foreach (AbsPermission per in permissionsToRemove)
                {
                    if (permissions.Contains(per)) {
                        permissions.Remove(per);
                    }
                }

                permissionsToRemove.Clear();

                removePermission(new OwnerPermission(store));
                //removePermission(EditPolicyPermission.getInstance(store));
                removePermission(new ManageInventoryPermission(store));
                removePermission(new GetHistoryPermission(store));
                removePermission(new ManagerPermission(store));
            }
        }

        public void removeOwnerPermission(Subscriber target, Store store) {

            lock (target.id < id ? target.permissions : permissions) {
                lock (target.id < id ? permissions : target.permissions) {

                    // check this user has the permission to perform this action
                    validatePermission(new AppointerPermission(target, store));

                    target.removeOwnerPermission(store);

                    // remove this user's permission to change the target's permissions
                    removePermission(new AppointerPermission(target, store));
                }
            }
        }

        public void addInventoryManagementPermission(Subscriber target, Store store) {

            addPermissionToManager(target, store, new ManageInventoryPermission(store));
        }

        public void removeInventoryManagementPermission(Subscriber target, Store store) {

            removePermissionFromManager(target, store, new ManageInventoryPermission(store));
        }

        
        public void addGetHistoryPermission(Subscriber target, Store store) {

            addPermissionToManager(target, store,new GetHistoryPermission(store));
        }

        public void removeGetHistoryPermission(Subscriber target, Store store) {

            removePermissionFromManager(target, store, new GetHistoryPermission(store));
        }

       public void addPermissionToManager(Subscriber target, Store store, AbsPermission permission) {

            lock (target.id < id ? target.permissions : permissions) {
                lock (target.id < id ? permissions : target.permissions) {

                    // check this user has the permission to perform this action
                    validatePermission(AppointerPermission.getInstance(target, store));

                    if (!target.havePermission(new ManagerPermission(store)))
                        throw new Exception("TargetIsNotManagerException:  "+ "username : " +target.getUserName()+  ", store: " + store.getName());

                    // add the permission to the target (if he doesn't already have it)
                    target.addPermission(permission);
                }
            }
        }

       public void removePermissionFromManager(Subscriber target, Store store, AbsPermission permission) {

            lock (target.id < id ? target.permissions : permissions) {
                lock (target.id < id ? permissions : target.permissions) {

                    // check this user has the permission to perform this action
                    validatePermission(AppointerPermission.getInstance(target, store));

                    if (target.havePermission(new OwnerPermission(store)))
                        throw new Exception("TargetIsNotOwnerException:  "+ "username : " +target.getUserName()+  ", store: " + store.getName());

                    target.removePermission(permission);
                }
            }
        }

        public int addStoreItem(Store store, string itemName, string category, string subCategory, int quantity, double price) {

            // check this user has the permission to perform this action
            validatePermission(new ManageInventoryPermission(store));

            return store.addItem(itemName, price, category, subCategory, quantity);
        }

        public void removeStoreItem(Store store, int itemId) {

            // check this user has the permission to perform this action
            validatePermission(new ManageInventoryPermission(store));

            store.removeItem(itemId);
        }

        public void updateStoreItem(Store store, int itemId, string newSubCategory, int newQuantity, double newPrice) {

            // check this user has the permission to perform this action
            validatePermission(new ManageInventoryPermission(store));

            store.changeItem(itemId,  newQuantity, newPrice);
        }

        public Collection<Store> getAllStores(Collection<Store> stores) {

            // check this user has the permission to perform this action
            validatePermission(new AdminPermission());

            return stores;
        }

        public Set getStoreItems(Store store) {

            // check this user has the permission to perform this action
            var v = new ManagerPermission(store);
            var v2 = new AdminPermission();
            var a = new ArrayList
            {
                v,
                v2
            };
            validateAtLeastOnePermission(a); //ManagerPermission.getInstance(store));

            return store.getItems().keySet();
        }

        public string storePermissionsToString(Store store) {

            lock (permissions) {

                StringBuilder result = new StringBuilder();

                AbsStorePermission ownerPermission = new OwnerPermission(store);
                AbsStorePermission managerPermission = new ManagerPermission(store);
                AbsStorePermission manageInventoryPermission = new ManageInventoryPermission(store);
                AbsStorePermission getHistoryPermission = new GetHistoryPermission(store);
                //Permission editPolicyPermission = EditPolicyPermission.getInstance(store);

                if (havePermission(ownerPermission))
                    result.Append(ownerPermission.toString()).Append(" ");
                if (havePermission(managerPermission))
                    result.Append(managerPermission.toString()).Append(" ");
                if (havePermission(manageInventoryPermission))
                    result.Append(manageInventoryPermission.toString()).Append(" ");
                if (havePermission(getHistoryPermission))
                    result.Append(getHistoryPermission.toString()).Append(" ");
                //if (havePermission(editPolicyPermission))
                // result.Append(editPolicyPermission.toString()).Append(" ");

                return result.ToString();
            }
        }

        public ArrayList getEventLog(ArrayList log) { // arraylist <string>
            throw new NotImplementedException();
            // validatePermission(AdminPermission.getInstance());
            //
            // return log;
        }

        public ArrayList getSalesHistoryByStore(Store store) { // arraylist <string>
            throw new NotImplementedException();
            // var v = AdminPermission.getInstance();
            // var v2 = GetHistoryPermission.getInstance(store);
            // var a = new ArrayList();
            // a.Add(v);
            // a.Add(v2);
            //
            // validateAtLeastOnePermission(a); //GetHistoryPermission.getInstance(store));
            //
            // return store.getPurchaseHistory();
        }

        public ArrayList getPurchaseHistory() { // arraylist <string>

            return new ArrayList(purchaseHistory);
        }

        // public void writeOpinionOnProduct(Store store, int itemId, string rev) {
        //
        //     if (rev == null || rev.Trim().Equals(""))
        //         throw new Exception("WrongReviewException: Review can't be empty or null");
        //
        //     var product = store.searchItemById(itemId);
        //     var p = (ArrayList)itemsPurchased.get(store);
        //     if (p != null && !p.Contains(product))
        //         throw new Exception("ItemNotPurchasedException:   Item ID: " + itemId + " item name: " + product.ProductName);
        //
        //     var review = new Review(this, store, product, rev);
        //     product.addReview(review);
        //    // store.notifyItemOpinion(review1);
        //
        // }
        
        
    }
}


