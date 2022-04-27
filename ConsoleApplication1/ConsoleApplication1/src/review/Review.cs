using Userpack; 
using StorePack;
namespace ReviewPack
{
    
    public class Review {

        private User user;
        private Store store;
        private Product p;
        private string review;

        public Review(User user, Store store, Product p, string review) {
            this.user = user;
            this.store = store;
            this.p = p;
            this.review = review;
        }

        public string getReview() {return review; }

        public void editReview(string review) {this.review = review; }
    }
}

