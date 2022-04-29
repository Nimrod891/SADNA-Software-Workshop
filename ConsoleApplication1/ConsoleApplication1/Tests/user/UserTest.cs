using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;
using ConsoleApp4.authentication;
using externalService;
using ikvm.extensions;
using java.lang;
using java.lang.reflect;
using java.util;
using Userpack;
using StorePack;
using Xunit;
using Moq;
using java.util.concurrent;
using policies;
using ArrayList = System.Collections.ArrayList;
public class UserTest {

    private Vistor vistor;

    private static ConcurrentHashMap items = new ConcurrentHashMap(); // <Product, int>

    private static Mock<Store> store; 
    private Mock<Product> item;
    private Basket b = new Basket(store.Object, items);
    private Mock<ConcurrentHashMap> baskets; // <Store, Basket>
    private Mock<PaymentSystem> paymentSystem;
    private Mock<DeliverySystem> deliverySystem;

    [Fact]
    void setUp()  {
        vistor = new Vistor((ConcurrentHashMap)baskets.Object);
        b = new Basket(store.Object, items);
        //store.setObservable(new Observable());
        store.Object.setPurchasePolicy(new DefaultPurchasePolicy());
        store.Object.setDiscountPolicy(new DefaultDiscountPolicy(store.Object.getItems().keySet()));
    }

    [Fact]
    void makeCart_WhenEmpty()
    {
        Mock<Vistor> from = new Mock<Vistor>();
        vistor.makeCart(from.Object);
        baskets.Object.putAll(vistor.getCart());
            
    }

    [Fact]
    void makeCart_WhenNotEmpty() {
        Assert.Equal(false, baskets.Object.isEmpty());
        Mock<Vistor> from = new Mock<Vistor>();
        vistor.makeCart(from.Object);
        baskets.Object.putAll(vistor.getCart());
    }

    [Fact]
    void getSubscriber() {
      //  assertThrows(NotLoggedInException.class, () -> user.getSubscriber());
    }

    [Fact]
    void getNewBasket() {
        Mock<Store> store = new Mock<Store>();
        Assert.NotNull(vistor.getBasket(store.Object));
    }

    [Fact]
    void getExistingBasket() {
        Mock<Store> store = new Mock<Store>();
        Basket basket = new Basket(store.Object,items);
        baskets.Object.put(store, basket);
        Assert.Same(basket, vistor.getBasket(store.Object));
    }

    [Fact]
    void purchaseCart()  {
        store.Object.addItem("cheese", 7.0, "cat1", "sub1", 5);
        item = new Mock<Product>(store.Object.searchItemById(0));
        baskets.Object.put(store, b);

        items.put(item, 3);
        Assert.Equal(1, vistor.getCart().size());
        Assert.Equal(5, store.Object.getItems().get(item));
        vistor.purchaseCart(paymentSystem.Object, deliverySystem.Object);
        Assert.Equal(0, vistor.getCart().size()); // checks that the cart is empty after the purchase
        Assert.Equal(2, store.Object.getItems().get(item)); // checks that the inventory quantity updated
    }

    [Fact]
    void purchaseCartNegativeQuantity() {
        // trying to purchase negative quantity
        store.Object.addItem("cheese", 7.0, "cat1", "sub1", 5);
        baskets.Object.put(store, b);
        items.put(item, -2);

       // assertThrows(WrongAmountException.class, () -> user.purchaseCart(paymentSystem, deliverySystem));
    }

    [Fact]
    void purchaseCartBigQuantityThanAvailable() {
        // trying to purchase more quantity than available
        store.Object.addItem("cheese", 7.0, "cat1", "sub1", 5);
        baskets.Object.put(store, b);
        items.put(item, 10);

       // assertThrows(WrongAmountException.class, () -> user.purchaseCart(paymentSystem, deliverySystem));
    }

    [Fact]
    void purchaseCartCorrectValueCalculation() {
        store.Object.addItem("cheese", 7.0, "cat1", "sub1", 5);
        baskets.Object.put(store, b);
        item = new Mock<Product>(store.Object.searchItemById(0));
        items.put(item, 3);

        vistor.purchaseCart(paymentSystem.Object, deliverySystem.Object);
        Assert.True(store.Object.getPurchaseHistory().toString().contains("21.0")); // checks that the purchase value correct
    }

    [Fact]
    void purchaseCartPurchaseHistoryUpdated()  {
        store.Object.addItem("cheese", 7.0, "cat1", "sub1", 5);
        baskets.Object.put(store, b);
        item = new Mock<Product>(store.Object.searchItemById(0));
        items.put(item, 3);

        vistor.purchaseCart(paymentSystem.Object, deliverySystem.Object);
        Assert.True(store.Object.getPurchaseHistory().toString().contains("cheese")); // checks that the purchase added to store history
    }
}