using System;
using System.Collections.Generic;
using System.Text;

public class Product
{
    private int pId;
    private string pName;
    private double price;
    private string category;
    //private string subcategory
    private double rating;
    private bool Opened = true;
    private ICollection<string> reviews = new LinkedList<string>();

    public Product(int pid, string name, double price, string category, double rating)
    {
        this.pId = pid;
        this.pName = name;
        this.price = price;
        this.category = category;
        this.rating = rating;
    }
    public Product() { }

    public int ProductId { get => pId; }

    public string ProductName { get => pName; }

    public double Price { get => price; set => price = value; }

    public string Category { get => category; }

    public double Rating { get => rating; set => rating = value; }

    public void lockstore() { this.Opened = false; }

    public void openstore() { this.Opened = true; }

    public bool isOpened() { return Opened; }

    public void addReview(string review) { reviews.Add(review); }

    public ICollection<string> getReviews() { return this.reviews; }
}
