using java.util;
using java.util.concurrent;
using System.Collections.Generic;
using System.Collections;
using StorePack;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;


namespace Userpack
{
    public abstract class AbsPermission {

        protected static readonly ConcurrentHashMap pool =
            (ConcurrentHashMap)Collections.synchronizedMap(new WeakHashMap()); //ConcurrentHashMap<AbsPermission,WeakReference<AbsPermission>>

        
        public bool equals(object o) {
            return this == o || o != null && GetType() == o.GetType();
        }

        protected int hashCode() {
            return Objects.hash(GetType());
        }
    }

}