using System;
using System.Collections.Generic;
using System.Text;
using java.util;
using ReviewPack;

namespace StorePack
{


    public class Product
    {
        private int pId;
        private string pName;
        private double price;

        private string category;

        //private string subcategory
        private double rating;
        private bool Opened = true;
        private Collection reviews = new LinkedList();

        public Product(int pid, string name, double price, string category, double rating)
        {
            this.pId = pid;
            this.pName = name;
            this.price = price;
            this.category = category;
            this.rating = rating;
        }

        public Product()
        {
        }

        public int ProductId
        {
            get => pId;
        }

        public string ProductName
        {
            get => pName;
        }

        public double Price
        {
            get => price;
            set => price = value;
        }

        public string Category
        {
            get => category;
        }

        public double Rating
        {
            get => rating;
            set => rating = value;
        }

        public void unlockstore()
        {
            this.Opened = false;
        }

       

        public void lockstore()
        {
            this.Opened = true;
        }

        public bool isOpened()
        {
            return Opened;
        }

        public void addReview(Review review)
        {
            reviews.add(review);
        }

        public Collection getReviews()
        {
            return this.reviews;
        }
    }
}