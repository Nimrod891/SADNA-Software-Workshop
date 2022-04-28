using ikvm.extensions;
using Userpack;
using StorePack;
using Xunit;
using Moq;
using java.util.concurrent;
public class BasketTest {

    private Basket basket;

    Mock<Store> store;
    Mock<Product> product;

    private  ConcurrentHashMap products = new ConcurrentHashMap(); // k:Product , v: int

    private int quantity = 3;
    private int differentQuantity = 5;

    [Fact]
    void setUp() {
        products.clear();
        products.put(product, quantity);
        basket = new Basket(store.Object, products);
    }

    [Fact]
    void addItem_notInBasket() {
        products.clear();
        basket.addItem(product.Object, quantity);
        Assert.Equal(quantity, products.get(product.Object));
    }

    [Fact]
    void addItem_alreadyInBasket() {
        basket.addItem(product.Object, differentQuantity);
        Assert.Equal(quantity + differentQuantity, products.get(product.Object));
    }

    [Fact]
    void getQuantity() {
        Assert.Equal(quantity, basket.getQuantity(product.Object));
    }

    [Fact]
    void setQuantity_notInBasket() {
        products.clear();
        basket.setQuantity(product.Object, differentQuantity);
        Assert.Equal(differentQuantity, products.get(product.Object));
    }

    [Fact]
    void setQuantity_alreadyInBasket() {
        basket.setQuantity(product.Object, differentQuantity);
        Assert.Equal(differentQuantity, products.get(product));
    }

    [Fact]
    void removeItem() {
        basket.removeProduct(product.Object);
        Assert.Equal(0, products.size());
    }
}