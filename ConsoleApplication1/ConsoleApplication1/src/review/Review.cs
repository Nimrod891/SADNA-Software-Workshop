using Userpack; 
using StorePack;
namespace ReviewPack
{
    
    public class Review {

        private Vistor vistor;
        private Store store;
        private Product p;
        private string review;

        public Review(Vistor vistor, Store store, Product p, string review) {
            this.vistor = vistor;
            this.store = store;
            this.p = p;
            this.review = review;
        }

        public string getReview() {return review; }

        public void editReview(string review) {this.review = review; }
    }
}

