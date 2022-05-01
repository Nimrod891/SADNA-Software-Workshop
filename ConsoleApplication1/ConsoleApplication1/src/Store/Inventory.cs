using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using java.lang;
using java.util;

using StorePack;
using Userpack;
using Exception = System.Exception;
using String = System.String;
using StringBuilder = System.Text.StringBuilder;

//this class has functions related to the stcok of a store , item search methods and a caculate method
public class Inventory
{
    private Map products; //<k: Product, v: int>
    private int idpatcher;

    public Map Products { get => products; }

    public Inventory()
    {
        products = new HashMap();
    }

    private string toString()
    {
        string s = "";
        var set = products.keySet();
        var iterator = set.iterator();
        for (int i = 0; i < set.size(); i++)
        {
            if (iterator.hasNext())
            {
                s = s + iterator.next().ToString() + '\n';
            }
        }

        return s;
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
    public int addProduct(string name, double price, string category, int amount)
    {
        if (name == null || name.Equals(""))
            throw new Exception("item name is illegal");
        if (price <= 0)
            throw new Exception("item price is iilegal");
        if (amount <= 0)
            throw new Exception("ilegal item amount");

        lock (products)
        {
            foreach (Product p in (IEnumerable)products.keySet())
                if (scompare(p.ProductName, name) && scompare(p.Category, category))
                    throw new Exception("item already exists");
            products.put(new Product(this.idpatcher, name, price, category, 0), amount);
            return idpatcher;

        }
    }

    //searches for the item with the matching name
    public Collection searchItemByName(string name)
    {
        Collection foundProducts = new LinkedList(); // Products
        foreach (Product item in (IEnumerable)products.keySet())
        {
            if (scompare(item.ProductName, name))
                foundProducts.add(item);
        }
        return foundProducts;
    }

    //find the matching items by category
    public Collection searchItemByCategory(string category)
    {
        Collection foundProducts = new LinkedList();
        foreach (Product item in (IEnumerable)products.keySet())
        {
            if (scompare(item.Category, category))
                foundProducts.add(item);
        }
        return foundProducts;
    }
    
    public Collection searchItemByKeyWord(string keyword)
    {
        Collection foundItems = new LinkedList();
        foreach (Product p in  (IEnumerable) Products.keySet())
        if(p.ProductName.ToLower().Contains(keyword.ToLower()) || p.Category.ToLower().Contains(keyword.ToLower())
                                                               /*||
           p.getSubCategory().toLowerCase().contains(keyword.ToLower())*/)
            foundItems.add(p);
        return foundItems;
    }
    

    public Product searchItem(int itemId)
    {
        foreach (Product item in (IEnumerable)products.keySet())
            if (item.ProductId == itemId)
                return item;
        throw new Exception("item not found");
    }

    //gives you the list of products by the pricemark
    public Collection filterByPrice(double startPrice, double endprice)
    {
       Collection foundProducts = new LinkedList();
        foreach (Product item in (IEnumerable)products.keySet())
        {
            if (item.Price >= startPrice && item.Price <= endprice)
                foundProducts.add(item);
        }
        return foundProducts;
    }


    public Collection filterByRating(double rating)
    {
        Collection foundProducts = new LinkedList();

        foreach (Product item in (IEnumerable)products.keySet())
        {
            if (item.Rating >= rating)
                foundProducts.add(item);
        }
        return foundProducts;
    }
    
    public Map getItems() {
        return products;
    }

    /**
     * This method searches the inventory by name, category and sub-Category
     * @param name - name of the wanted item
     * @param category - the category of the wanted item
     * @param subCategory - the sub category of the wanted item
     * @exception ItemNotFoundException - when there are no item that matches the giving parameters.*/
    public Product getItem(String name, String category, String subCategory)
    {
        foreach (Product p in (IEnumerable)products.keySet())
        if(p.ProductName.Equals(name) && p.Category.Equals(category))
        return p;
    throw new Exception( "ItemNotFoundException : item not found");
}
    

    public void changeQuantity(int itemId, int amount)
    {
        if (amount < 0)
            throw new Exception("item amount should be 0 or more than that");
        lock (this.products)
        {
            var am = searchItem(itemId);
            products.put(am,amount);
        }
        
    }

    //checks the stock for a certin item 
    public bool checkAmount(int itemId, int amount)
    {
        var am = searchItem(itemId);
        if (amount > (int)products.get(am))
            throw new Exception("there is not enough from the item");
        if (amount < 0)
            throw new Exception("amount can't be a negative number");
        return true;
    }

    //removes an item from the inventory.
    public Product removeItem(int itemID)
    {
        Product item = searchItem(itemID);
        products.remove(item);
        return item;
    }

    public void changeItemDetails(int itemID, int newQuantity, double newPrice)
    {
        //synchronized (this.items)
        //{
        foreach (Product item in (IEnumerable)products.keySet())
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
    
    /// <summary>
    /// supposed to calculate our basket.
    /// </summary>
    /// <param name="basket"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public double calculate(Basket basket)
    {
        throw new NotImplementedException();
    }

}
