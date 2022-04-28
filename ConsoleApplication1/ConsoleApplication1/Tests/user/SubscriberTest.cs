using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;
using ikvm.extensions;
using java.lang;
using java.lang.reflect;
using java.util;
using Userpack;
using StorePack;
using Xunit;
using Moq;
using java.util.concurrent;
using ArrayList = System.Collections.ArrayList;
public class SubscriberTest {

    private Subscriber subscriber;

    private Mock<AbsPermission> permission;
    private Mock<HashSet<AbsPermission>> permissions;
    private Mock<HashSet<AbsPermission>> targetPermissions;
    private Mock<Collection<Store>> stores;
    private Mock<ConcurrentHashMap> itemsPurchased; //Store, Collection<Product>
    private Mock<ArrayList> purchasesHistory = new Mock<ArrayList>(new ArrayList()); // arrylist<string>
    private Mock<Product> p;
    private Mock<Product> p2;

    private static Mock<Store> store = new Mock<Store>();
    private static Mock<Subscriber> target = new Mock<Subscriber>();

    private  AbsPermission adminPermission = AdminPermission.getInstance();
    private  AbsPermission managerPermission = ManagerPermission.getInstance(store.Object);
    private  AbsPermission ownerPermission = OwnerPermission.getInstance(store.Object);
    private  AbsPermission manageInventoryPermission = ManageInventoryPermission.getInstance(store.Object);
    private  AbsPermission getHistoryPermission = GetHistoryPermission.getInstance(store.Object);
    private  AbsPermission editPolicyPermission = EditPolicyPermission.getInstance(store.Object);
    private  AbsPermission appointerPermission = AppointerPermission.getInstance(target.Object, store.Object);

    private  double price = 500.0;
    private  int quantity = 3;
    private  int itemId = 37373;
    private  string subCategory = "Gaming Consoles";

    [Fact]
    void setUp()  {
        subscriber = new  Subscriber(1, "Johnny", permissions.Object, itemsPurchased.Object, purchasesHistory.Object);

        store  = new Mock<Store>();
        target =  new Mock<Subscriber>();

        // set target's private fields so that the "synchronized" would not throw an exception

        Field privateField = target.getClass().getDeclaredField("id");
        privateField.setAccessible(true);
        privateField.set(target, 123456);
        privateField.setAccessible(false);

        privateField = target.getClass().getDeclaredField("permissions");
        privateField.setAccessible(true);
        privateField.set(target, targetPermissions);
        privateField.setAccessible(false);
    }

    [Fact]
    void validatePermission_havePermission()
    {

        var res = subscriber.havePermission(permission.Object);
        Assert.Equal(true,res);
        subscriber.validatePermission(permission.Object);
    }

    [Fact]
    void validatePermission_noPermission() {

      //  Assert.Throws(NoPermissionException.class, () -> subscriber.validatePermission(permission));
    }

    [Fact]
    void validateAtLeastOnePermission()
    {

        var res = subscriber.havePermission(permission.Object);
        Assert.Equal(false,res);
        var res2 = subscriber.havePermission(adminPermission);
        Assert.Equal(true,res2);
        Mock<ArrayList> a = new Mock<ArrayList>();
        a.Object.Add(permission.Object);
        a.Object.Add(adminPermission);
        subscriber.validateAtLeastOnePermission(a.Object);
    }

    [Fact]
    void getAllStores()  {

        subscriber.validatePermission(AdminPermission.getInstance());
        subscriber.getAllStores(stores.Object);
    }

    [Fact]
    void addManagerPermission()  {

       subscriber.validatePermission(ownerPermission);
        subscriber.addManagerPermission(target.Object, store.Object); 
        target.Object.addPermission(managerPermission);
        subscriber.addPermission(appointerPermission);
    }

    [Fact]
    void addManagerPermission_alreadyManager()  {

       subscriber.validatePermission(ownerPermission);
       Assert.Equal(true,target.Object.havePermission(managerPermission));
       //assertThrows(AlreadyManagerException.class, () -> subscriber.addManagerPermission(target, store));
       AbsPermission absPermission = ManagerPermission.getInstance(store.Object);
       target.Object.addPermission(absPermission);
       subscriber.addPermission(absPermission);
    }

   [Fact]
    void addOwnerPermissions_toSelf()
    {


        Mock<Subscriber> s = new Mock<Subscriber>(subscriber);
        s.Object.addOwnerPermission(store.Object);
        subscriber.addPermission(OwnerPermission.getInstance(store.Object));
        subscriber.addPermission(ManagerPermission.getInstance(store.Object));
        subscriber.addPermission(ManageInventoryPermission.getInstance(store.Object));
       subscriber.addPermission(GetHistoryPermission.getInstance(store.Object));
        subscriber.addPermission(EditPolicyPermission.getInstance(store.Object));
    }

    [Fact]
    void addOwnerPermissions_toTarget()  {

        doNothing().when(subscriber).validatePermission(ownerPermission);
        subscriber.addOwnerPermission(target.Object, store.Object);
        verify(target).addOwnerPermission(store);
        verify(subscriber).addPermission(appointerPermission);
    }

    [Fact]
    void addOwnerPermissions_targetIsAlreadyOwner() {

        Assert.True(subscriber.havePermission(appointerPermission));
        Assert.True(target.Object.havePermission(appointerPermission));
       // assertThrows(AlreadyOwnerException.class, () -> subscriber.addOwnerPermission(target, store));
        verify(target, never()).addPermission(any());
        verify(subscriber, never()).addPermission(any());
    }

    [Fact]
    void addOwnerPermissions_targetIsManagerAppointedByCaller()  {

        Assert.True(subscriber.havePermission(ownerPermission));
        Assert.True(subscriber.havePermission(appointerPermission));
        Assert.False(target.Object.havePermission(ownerPermission) );
        Assert.True(target.Object.havePermission(managerPermission));
        subscriber.addOwnerPermission(target.Object, store.Object);
        target.Object.addOwnerPermission(store.Object);
        subscriber.addPermission(appointerPermission);
    }

    [Fact]
    void addOwnerPermission_targetIsManagerAppointedByAnother() {

        Assert.True(subscriber.havePermission(ownerPermission));
        Assert.False(target.Object.havePermission(ownerPermission));
        Assert.True(target.Object.havePermission(managerPermission));
       // assertThrows(NoPermissionException.class, () -> subscriber.addOwnerPermission(target, store));
        verify(target, never()).addPermission(any());
        verify(subscriber, never()).addPermission(any());
    }

    [Fact]
    void removeOwnerPermission_fromTarget()  {

        doNothing().when(subscriber).validatePermission(appointerPermission);
        subscriber.removeOwnerPermission(target.Object, store.Object);
        verify(target).removeOwnerPermission(store);
    }

    [Fact]
    void removeOwnerPermission_fromSelf() {

        Set permissions = new HashSet(); // <AbsPermission> 

        Field privateField = subscriber.getClass().getDeclaredField("permissions");
        privateField.setAccessible(true);
        privateField.set(subscriber, permissions);
        privateField.setAccessible(false);

        subscriber.removeOwnerPermission(store.Object);
        subscriber.removePermission(ownerPermission);
        subscriber.removePermission(manageInventoryPermission);
        subscriber.removePermission(getHistoryPermission);
        subscriber.removePermission(editPolicyPermission);
        subscriber.removePermission(managerPermission);
    }

    [Fact]
    void removeOwnerPermission_fromSelf_recursive()  {

        Set permissions = new HashSet(); //AbsPermission
        permissions.add(appointerPermission);
        permissions.add(managerPermission);
        permissions.add(getHistoryPermission);
        permissions.add(editPolicyPermission);
        permissions.add(manageInventoryPermission);

        Field privateField = subscriber.getClass().getDeclaredField("permissions");
        privateField.setAccessible(true);
        privateField.set(subscriber, permissions);
        privateField.setAccessible(false);

        subscriber.removeOwnerPermission(store.Object);
        target.Object.removeOwnerPermission(store.Object);
        subscriber.removeOwnerPermission(store.Object);
    }

    [Fact]
    void removeManagerPermission()  {

        subscriber.validatePermission(appointerPermission);
        subscriber.removeManagerPermission(target.Object, store.Object);
        target.Object.removeOwnerPermission(store.Object);
    }

    [Fact]
    void addStoreItem()  {

        var item = "PS5";
        var category = "Electronics";

        subscriber.validatePermission(manageInventoryPermission);
        subscriber.addStoreItem(store.Object, item, category, subCategory, quantity, price);
        store.Object.addItem(item, price, category, subCategory, quantity);
    }

    [Fact]
    void removeStoreItem()  {

     subscriber.validatePermission(manageInventoryPermission);
        subscriber.removeStoreItem(store.Object, itemId);
        store.Object.removeItem(itemId);
    }

    [Fact]
    void updateStoreItem() {

        subscriber.validatePermission(manageInventoryPermission);
        subscriber.updateStoreItem(store.Object, itemId, subCategory, quantity, price);
       store.Object.changeItem(itemId, subCategory, quantity, price);
    }

    [Fact]
    void getEventLog()  {

        subscriber.validatePermission(AdminPermission.getInstance());
        subscriber.getEventLog(null);
    }

    [Fact]
    void addPermissionToManager()  {

        subscriber.validatePermission(appointerPermission);
        Assert.True(target.Object.havePermission(managerPermission));
        subscriber.addPermissionToManager(target.Object, store.Object, permission.Object);
        target.Object.addPermission(permission.Object);
    }

    [Fact]
    void addPermissionToManager_targetNotManager()  {

        subscriber.validatePermission(appointerPermission); 
        Assert.True(target.Object.havePermission(managerPermission));
       // assertThrows(TargetIsNotManagerException.class, () -> subscriber.addPermissionToManager(target, store, permission));
        verify(target, never()).addPermission(any());
    }

    [Fact]
    void removePermissionFromManager()  {

        subscriber.validatePermission(appointerPermission);
        Assert.False(target.Object.havePermission(ownerPermission));
        subscriber.removePermissionFromManager(target.Object, store.Object, permission.Object);
        target.Object.removePermission(permission.Object);
    }

    [Fact]
    void removePermissionFromManager_targetIsOwner() {

        subscriber.validatePermission(appointerPermission);
        Assert.True(target.Object.havePermission(ownerPermission));
        //assertThrows(TargetIsOwnerException.class, () -> subscriber.removePermissionFromManager(target, store, permission));
        verify(target, never()).removePermission(permission);
    }

    [Fact]
    void writeOpinionOnProductGoodDetails()  {
        Collection items = new LinkedList(); // <Product>
        items.add(p.Object);

        Assert.Equal(p.Object, itemsPurchased.Object.get(store.Object));
        Assert.Equal(p.Object,store.Object.searchItemById(0));

        Assert.Equal(0, p.Object.getReviews().size());
        subscriber.writeOpinionOnProduct(store.Object, p.Object.ProductId, "good product");
        Assert.Equal(1, p.Object.getReviews().size());
    }

    [Fact]
    void writeOpnionOnProductBadReviewDetails() {
        // assertThrows(WrongReviewException.class, ()-> subscriber.writeOpinionOnProduct(store, item.getId(), null));
        //assertThrows(WrongReviewException.class, ()-> subscriber.writeOpinionOnProduct(store, item.getId(), "    "));
    }

    [Fact]
    void writeOpnionOnProductNotPurchasedItem()  {
        Collection products = new LinkedList(); //<Product>
        products.add(p.Object);
        Assert.Equal(p2.Object,store.Object.searchItemById(0));
        Assert.Equal(p.Object,itemsPurchased.Object.get(store));

       // assertThrows(ItemNotPurchasedException.class, ()-> subscriber.writeOpinionOnProduct(store, item2.getId(), "good product"));
        Assert.Equal(0, p.Object.getReviews().size());
        Assert.Equal(0, p2.Object.getReviews().size());
    }

    [Fact]
    void getPurchaseHistory() {
        Assert.Equal(0, subscriber.getPurchaseHistory().Count);
        purchasesHistory.Object.Add("milk");
        purchasesHistory.Object.Add("cheese");
        Assert.Equal(2, subscriber.getPurchaseHistory().Count);
        Assert.True(subscriber.getPurchaseHistory().Contains("milk"));
        Assert.True(subscriber.getPurchaseHistory().Contains("cheese"));
    }

    [Fact]
    void getSalesHistoryByStore()  {
        purchasesHistory.Object.Add("milk");
        purchasesHistory.Object.Add("cheese");
        Assert.Equivalent(purchasesHistory.Object,store.Object.getPurchaseHistory());
        Assert.True(subscriber.havePermission(getHistoryPermission));

        Assert.Equal(2, subscriber.getSalesHistoryByStore(store.Object).Count);
        Assert.True(subscriber.getSalesHistoryByStore(store.Object).Contains("milk"));
        Assert.True(subscriber.getSalesHistoryByStore(store.Object).Contains("cheese"));
    }

    [Fact]
    void getSalesHistoryByStoreNoPremission() {
        Assert.False(subscriber.havePermission(getHistoryPermission));
        //assertThrows(NoPermissionException.class, ()-> subscriber.getSalesHistoryByStore(store));
    }
}