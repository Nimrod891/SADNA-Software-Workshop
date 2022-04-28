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
        subscriber = new  Subscriber(1, "Johnny", permissions.Object, itemsPurchased.Object, purchasesHistory.Object));

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
    void addOwnerPermissions_toSelf() {

        subscriber.addOwnerPermission(store);

        verify(subscriber).addPermission(OwnerPermission.getInstance(store));
        verify(subscriber).addPermission(ManagerPermission.getInstance(store));
        verify(subscriber).addPermission(ManageInventoryPermission.getInstance(store));
        verify(subscriber).addPermission(GetHistoryPermission.getInstance(store));
        verify(subscriber).addPermission(EditPolicyPermission.getInstance(store));
    }

    [Fact]
    void addOwnerPermissions_toTarget()  {

        doNothing().when(subscriber).validatePermission(ownerPermission);
        subscriber.addOwnerPermission(target, store);
        verify(target).addOwnerPermission(store);
        verify(subscriber).addPermission(appointerPermission);
    }

    [Fact]
    void addOwnerPermissions_targetIsAlreadyOwner() {

        when(subscriber.havePermission(ownerPermission)).thenReturn(true);
        when(target.havePermission(ownerPermission)).thenReturn(true);
        assertThrows(AlreadyOwnerException.class, () -> subscriber.addOwnerPermission(target, store));
        verify(target, never()).addPermission(any());
        verify(subscriber, never()).addPermission(any());
    }

    [Fact]
    void addOwnerPermissions_targetIsManagerAppointedByCaller()  {

        when(subscriber.havePermission(ownerPermission)).thenReturn(true);
        when(subscriber.havePermission(appointerPermission)).thenReturn(true);
        when(target.havePermission(ownerPermission)).thenReturn(false);
        when(target.havePermission(managerPermission)).thenReturn(true);
        subscriber.addOwnerPermission(target, store);
        verify(target).addOwnerPermission(store);
        verify(subscriber).addPermission(appointerPermission);
    }

    [Fact]
    void addOwnerPermission_targetIsManagerAppointedByAnother() {

        when(subscriber.havePermission(ownerPermission)).thenReturn(true);
        when(target.havePermission(ownerPermission)).thenReturn(false);
        when(target.havePermission(managerPermission)).thenReturn(true);
        assertThrows(NoPermissionException.class, () -> subscriber.addOwnerPermission(target, store));
        verify(target, never()).addPermission(any());
        verify(subscriber, never()).addPermission(any());
    }

    [Fact]
    void removeOwnerPermission_fromTarget()  {

        doNothing().when(subscriber).validatePermission(appointerPermission);
        subscriber.removeOwnerPermission(target, store);
        verify(target).removeOwnerPermission(store);
    }

    [Fact]
    void removeOwnerPermission_fromSelf() {

        Set<Permission> permissions = new HashSet<>();

        Field privateField = subscriber.getClass().getDeclaredField("permissions");
        privateField.setAccessible(true);
        privateField.set(subscriber, permissions);
        privateField.setAccessible(false);

        subscriber.removeOwnerPermission(store);
        verify(subscriber).removePermission(ownerPermission);
        verify(subscriber).removePermission(manageInventoryPermission);
        verify(subscriber).removePermission(getHistoryPermission);
        verify(subscriber).removePermission(editPolicyPermission);
        verify(subscriber).removePermission(managerPermission);
    }

    [Fact]
    void removeOwnerPermission_fromSelf_recursive()  {

        Set<Permission> permissions = new HashSet<>();
        permissions.add(appointerPermission);
        permissions.add(managerPermission);
        permissions.add(getHistoryPermission);
        permissions.add(editPolicyPermission);
        permissions.add(manageInventoryPermission);

        Field privateField = subscriber.getClass().getDeclaredField("permissions");
        privateField.setAccessible(true);
        privateField.set(subscriber, permissions);
        privateField.setAccessible(false);

        subscriber.removeOwnerPermission(store);
        verify(target).removeOwnerPermission(store);
        verify(subscriber).removeOwnerPermission(store);
    }

    [Fact]
    void removeManagerPermission()  {

        doNothing().when(subscriber).validatePermission(appointerPermission);
        subscriber.removeManagerPermission(target, store);
        verify(target).removeOwnerPermission(store);
    }

    [Fact]
    void addStoreItem()  {

        String item = "PS5";
        String category = "Electronics";

        doNothing().when(subscriber).validatePermission(manageInventoryPermission);
        subscriber.addStoreItem(store, item, category, subCategory, quantity, price);
        verify(store).addItem(item, price, category, subCategory, quantity);
    }

    [Fact]
    void removeStoreItem()  {

        doNothing().when(subscriber).validatePermission(manageInventoryPermission);
        subscriber.removeStoreItem(store, itemId);
        verify(store).removeItem(itemId);
    }

    [Fact]
    void updateStoreItem() {

        doNothing().when(subscriber).validatePermission(manageInventoryPermission);
        subscriber.updateStoreItem(store, itemId, subCategory, quantity, price);
        verify(store).changeItem(itemId, subCategory, quantity, price);
    }

    [Fact]
    void getEventLog()  {

        doNothing().when(subscriber).validatePermission(AdminPermission.getInstance());
        subscriber.getEventLog(null);
    }

    [Fact]
    void addPermissionToManager()  {

        doNothing().when(subscriber).validatePermission(appointerPermission);
        when(target.havePermission(managerPermission)).thenReturn(true);
        subscriber.addPermissionToManager(target, store, permission);
        verify(target).addPermission(permission);
    }

    [Fact]
    void addPermissionToManager_targetNotManager()  {

        doNothing().when(subscriber).validatePermission(appointerPermission);
        when(target.havePermission(managerPermission)).thenReturn(false);
        assertThrows(TargetIsNotManagerException.class, () -> subscriber.addPermissionToManager(target, store, permission));
        verify(target, never()).addPermission(any());
    }

    [Fact]
    void removePermissionFromManager()  {

        doNothing().when(subscriber).validatePermission(appointerPermission);
        when(target.havePermission(ownerPermission)).thenReturn(false);
        subscriber.removePermissionFromManager(target, store, permission);
        verify(target).removePermission(permission);
    }

    [Fact]
    void removePermissionFromManager_targetIsOwner() {

        doNothing().when(subscriber).validatePermission(appointerPermission);
        when(target.havePermission(ownerPermission)).thenReturn(true);
        assertThrows(TargetIsOwnerException.class, () -> subscriber.removePermissionFromManager(target, store, permission));
        verify(target, never()).removePermission(permission);
    }

    [Fact]
    void writeOpinionOnProductGoodDetails()  {
        Collection<Item> items = new LinkedList<>();
        items.add(item);

        when(itemsPurchased.get(store)).thenReturn(items);
        when(store.searchItemById(0)).thenReturn(item);

        assertEquals(0, item.getReviews().size());
        subscriber.writeOpinionOnProduct(store, item.getId(), "good product");
        assertEquals(1, item.getReviews().size());
    }

    [Fact]
    void writeOpnionOnProductBadReviewDetails() {
        assertThrows(WrongReviewException.class, ()-> subscriber.writeOpinionOnProduct(store, item.getId(), null));
        assertThrows(WrongReviewException.class, ()-> subscriber.writeOpinionOnProduct(store, item.getId(), "    "));
    }

    [Fact]
    void writeOpnionOnProductNotPurchasedItem()  {
        Collection<Item> items = new LinkedList<>();
        items.add(item);
        when(store.searchItemById(0)).thenReturn(item2);
        when(itemsPurchased.get(store)).thenReturn(items);

        assertThrows(ItemNotPurchasedException.class, ()-> subscriber.writeOpinionOnProduct(store, item2.getId(), "good product"));
        assertEquals(0, item.getReviews().size());
        assertEquals(0, item2.getReviews().size());
    }

    [Fact]
    void getPurchaseHistory() {
        assertEquals(0, subscriber.getPurchaseHistory().size());
        purchasesHistory.add("milk");
        purchasesHistory.add("cheese");
        assertEquals(2, subscriber.getPurchaseHistory().size());
        assertTrue(subscriber.getPurchaseHistory().contains("milk"));
        assertTrue(subscriber.getPurchaseHistory().contains("cheese"));
    }

    [Fact]
    void getSalesHistoryByStore()  {
        purchasesHistory.add("milk");
        purchasesHistory.add("cheese");
        when(store.getPurchaseHistory()).thenReturn(purchasesHistory);
        when(subscriber.havePermission(getHistoryPermission)).thenReturn(true);

        assertEquals(2, subscriber.getSalesHistoryByStore(store).size());
        assertTrue(subscriber.getSalesHistoryByStore(store).contains("milk"));
        assertTrue(subscriber.getSalesHistoryByStore(store).contains("cheese"));
    }

    [Fact]
    void getSalesHistoryByStoreNoPremission() {
        when(subscriber.havePermission(getHistoryPermission)).thenReturn(false);
        assertThrows(NoPermissionException.class, ()-> subscriber.getSalesHistoryByStore(store));
    }
}