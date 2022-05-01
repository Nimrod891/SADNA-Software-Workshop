using StorePack;

namespace ConsoleApplication1.Permmissions
{
    public abstract class AbsStorePermission : AbsPermission
    {

        protected Store store;

        protected AbsStorePermission(Store store)
        {
            this.store = store;
        }

        public Store getStore()
        {
            return store;
        }
        
        public string toString()
        {
            return GetType().ToString() + "{" +
                    "store=" + (store == null ? null : store.getName()) +
                    '}';
        }
    }
}