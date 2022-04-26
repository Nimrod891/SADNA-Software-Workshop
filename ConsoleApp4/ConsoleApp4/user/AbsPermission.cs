

using System.Collections.Generic;
using System.Collections;
using StorePack;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
namespace Userpack
{
    public abstract class AbsPermission
    {
        protected static Dictionary<AbsPermission, WeakReference<AbsPermission>> pool = ConcurrentDictionary(new ConditionalWeakTable<>());


        public bool equals(Object o)
        {
            return this == o || o != null && GetType().Name == o.GetType().Name;
        }


        public int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}