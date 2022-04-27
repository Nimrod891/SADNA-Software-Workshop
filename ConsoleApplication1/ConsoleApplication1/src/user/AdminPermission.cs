
using WeakReference = System.WeakReference;

namespace Userpack
{
    public class AdminPermission : AbsPermission
    {
        private AdminPermission() 
        { }

        public static AdminPermission getInstance()
        {
            var ad = new AdminPermission();
            return (AdminPermission)pool.putIfAbsent(ad, new WeakReference(ad));
            
            /*(AdminPermission)ComputeIfAbsent(pool, ghp, new WeakReference(ghp)).TryGetTarget(abs);
            if (abs != null) return abs;
            else throw new Exception("ExceptionAdminPermission: AdminPermission something was worng");*/
        }


        public string toString()
        {
            return "AdminPermission";
        }

        /*private static V ComputeIfAbsent<K, V>(this Dictionary<K, V> dict, K key, Func<K, V> generator)
        {
            bool exists = dict.TryGetValue(key, out var value);
            if (exists)
            {
                return value;
            }
            var generated = generator;
            dict.Add(key, generated);
            return generated;
        }*/
    }
}