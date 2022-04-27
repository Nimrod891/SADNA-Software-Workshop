using StorePack;
using java.util.concurrent;

namespace Userpack {
  

    public record struct Basket(Store store, ConcurrentHashMap products) {
        public Store getStore() {
            return store;
        }

        public void addItem(Product p, int quantity)
        {
           var flag =products.contains(p);
           if (flag)
           {
               int newquan = (int)products.get(p);
               newquan += quantity;
               products.replace(p, products.get(p), newquan);
               return;
           }

           products.put(p, quantity);
        }

        public int getQuantity(Product p)
        {
          
           return (int)products.get(p);
        }

        public void setQuantity(Product p, int quantity)
        {
            products.putIfAbsent(p, quantity);
        }

        public ConcurrentHashMap getItems() {
            return products;
        }

        public void removeProduct(Product p)
        {
            products.remove(p);
        }
    }
}