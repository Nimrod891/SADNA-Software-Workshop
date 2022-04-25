using System;
namespace StorePack;
using StorePack;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Collections;
public class Product
{
  private int id;
    private string name;
    private double price;
    private string category;
    private string subCategory;
    private double rating;
    private bool isLocked = false;
    private  ArrayList<Review> reviews = new ArrayList<>();

    public Item() {}

    public Item(int id, string name, double price, string category, string subCategory, double rating) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.category = category;
        this.subCategory = subCategory;
        this.rating = rating;
    }

    public int getId() {
        return id;
    }

    public string getName() {
        return name;
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price)  {
        if(price < 0)
            throw WrongPriceException("item price must be positive");
        this.price = price;
    }

    public String getCategory() {
        return category;
    }

    public String getSubCategory() {
        return subCategory;
    }

    public void setSubCategory(String newSubCategory){ this.subCategory=newSubCategory;}

    public double getRating() {
        return rating;
    }

    public void setRating(double rating)  {
        if(rating < 0)
            throw WrongRatingException("rating must be positive");
        this.rating = rating;
    }

    public String toString() { return "id:" + id +
            "\nname:" + name +
            "\nprice:" + price +
            "\ncategory:" + category +
            "\nsub category:" + subCategory +
            "\nrating:" + rating + '\n';}

    public void lockIt() { isLocked = true; }

    public void unlock() {isLocked = false; }

    public boolean isLocked() {return isLocked; }

    public void addReview(Review review) {reviews.add(review); }

    public CollectionBase<Review> getReviews() {return reviews; }
}
