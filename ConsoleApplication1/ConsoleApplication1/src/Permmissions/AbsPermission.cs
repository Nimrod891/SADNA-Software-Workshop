using java.util;
using java.util.concurrent;

namespace ConsoleApplication1.Permmissions
{
    public abstract class AbsPermission {

        protected static readonly ConcurrentHashMap pool =
            (ConcurrentHashMap)Collections.synchronizedMap(new WeakHashMap()); //ConcurrentHashMap<AbsPermission,WeakReference<AbsPermission>>
        
    }

}