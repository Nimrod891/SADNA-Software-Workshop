using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

 //this class has functions related to the stcok of a store , item search methods and a caculate method
public class Inventory  
{
    private Dictionary<Product, int> products;
    private int idpatcher;

    public Dictionary<Product, int> Products { get => products; }

    public Inventory()
    {
        products = new Dictionary<Product, int>();
    }

    private bool scompare(string s1, string s2)
    {
        return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///  adds a new item into the store inventory 
    ///  needs to be sychronized so it will stay stable with multiple users
    /// </summary>
    /// <returns></returns>
    public void addProduct(string name, double price, string category, int amount)
    {
        if (name == null || name.Equals(""))
            throw new Exception("item name is illegal");
        if (price <= 0)
            throw new Exception("item price is iilegal");
        if (amount <= 0)
            throw new Exception("ilegal item amount");

        lock (products)
        {
            foreach (Product p in products.Keys)
                if (scompare(p.ProductName, name) && scompare(p.Category, category))
                    throw new Exception("item already exists");
            products.Add(new Product(this.idpatcher, name, price, category, 0), amount);
            idpatcher++;

        }
    }

    //searches for the item with the matching name
    public ICollection<Product> searchItemByName(string name)
    {
        ICollection<Product> foundProducts = new LinkedList<Product>();
        foreach (var item in products.Keys)
        {
            if (scompare(item.ProductName, name))
                foundProducts.Add(item);
        }
        return foundProducts;
    }

    //find the matching items by category
    public ICollection<Product> searchItemByCategory(string category)
    {
        ICollection<Product> foundProducts = new LinkedList<Product>();
        foreach (var item in products.Keys)
        {
            if (scompare(item.Category, category))
                foundProducts.Add(item);
        }
        return foundProducts;
    }

    public Product searchItem(int itemId)
    {
        foreach (var item in products.Keys)
            if (item.ProductId == itemId)
                return item;
        throw new Exception("item not found");
    }

    //gives you the list of products by the pricemark
    public ICollection<Product> filterByPrice(double startPrice, double endprice)
    {
        ICollection<Product> foundProducts = new LinkedList<Product>();
        foreach (var item in products.Keys)
        {
            if (item.Price >= startPrice && item.Price <= endprice)
                foundProducts.Add(item);
        }
        return foundProducts;
    }


    public ICollection<Product> filterByRating(double rating)
    {
        ICollection<Product> foundProducts = new LinkedList<Product>();

        foreach (var item in products.Keys)
        {
            if (item.Rating >= rating)
                foundProducts.Add(item);
        }
        return foundProducts;
    }

    public void changeQuantity(int itemId, int amount)
    {
        if (amount < 0)
            throw new Exception("item amount should be 0 or more than that");
        products[searchItem(itemId)] = amount;
    }

    //checks the stock for a certin item 
    public bool checkAmount(int itemId, int amount)
    {
        if (amount > products[searchItem(itemId)])
            throw new Exception("there is not enough from the item");
        if (amount < 0)
            throw new Exception("amount can't be a negative number");
        return true;
    }

    //removes an item from the inventory.
    public Product removeItem(int itemID)
    {
        Product item = searchItem(itemID);
        products.Remove(item);
        return item;
    }

    public void changeItemDetails(int itemID, string newSubCategory, int newQuantity, double newPrice)
    {
        //synchronized (this.items)
        //{
        foreach (var item in products.Keys)
        {
            if (item.ProductId == itemID)
            {

                if (newQuantity != null)
                    changeQuantity(itemID, newQuantity);

                if (newPrice != null)
                    item.Price = newPrice;

                return;
            }
        }
        throw new Exception("no item in inventory matching item id");
        // }
    }

}
