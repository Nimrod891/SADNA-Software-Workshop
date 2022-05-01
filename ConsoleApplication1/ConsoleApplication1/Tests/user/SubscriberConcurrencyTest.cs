using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;
using ConsoleApplication1.Permmissions;
using ikvm.extensions;
using java.lang;
using java.util;
using Userpack;
using StorePack;
using Xunit;
using Moq;
using java.util.concurrent;
using ArrayList = System.Collections.ArrayList;

public class SubscriberConcurrencyTest {

     private Mock<HashSet<AbsPermission>> permissions1;
     private Mock<HashSet<AbsPermission>> permissions2;
     private static Mock<Store> store;
     private Mock<ConcurrentHashMap> itemsPurchased; //K :Store, V :Collection<Product>
     private Mock<ArrayList> purchaseHistory; // string

    [Fact]
    void testPermissionsLocks()  {
        TestPermissionsLocks(false);
    }

    [Fact]
    void testPermissionsLocks_reversedOrder() {
        TestPermissionsLocks(true);
    }

   [SuppressMessage("SynchronizeOnNonFinalField", "BusyWait")]
   private void TestPermissionsLocks(bool reverseOrder)  {

        // test a situation of 2 subscribers trying to appoint one another as store owners
        // the potentially problematic point is when trying to acquire each other's locks

        CountDownLatch latch = new CountDownLatch(2);

        Subscriber sub1 = new Subscriber(1, "Sub1", permissions1.Object, itemsPurchased.Object, purchaseHistory.Object);
        Subscriber sub2 = new Subscriber(2, "Sub2", permissions2.Object, itemsPurchased.Object, purchaseHistory.Object);

        Thread thread1 = new MyThread("Thread-1", sub1, sub2, latch);
        Thread thread2 = new MyThread("Thread-2", sub2, sub1, latch);

        // lock both permissions objects and try to appoint each other
        lock (permissions2) {
            if (reverseOrder) thread2.start(); else thread1.start();

            // busy wait for one of the threads to block
            while (thread1.getState() != java.lang.Thread.State.BLOCKED && thread2.getState() != java.lang.Thread.State.BLOCKED)
                Thread.sleep(1);

            if (reverseOrder) thread1.start(); else thread2.start();

            // busy wait for both threads to block
            while (thread1.getState() != Thread.State.BLOCKED || thread2.getState() != Thread.State.BLOCKED)
                Thread.sleep(1);
        }

        // wait for the threads to complete or deadlock
        while(!latch.await(1, TimeUnit.MILLISECONDS)) // 1 millisecond should suffice
            Assert.False(thread1.getState() == Thread.State.BLOCKED && thread2.getState() == Thread.State.BLOCKED, "Deadlock");

        // both threads completed without deadlock
    }

   private class MyThread : Thread {
       private  Subscriber subscriber;
       private  Subscriber target;
       private  CountDownLatch latch;

       public MyThread(string name, Subscriber subscriber, Subscriber target, CountDownLatch latch) : base(name){ 
       this.subscriber = subscriber;
       this.target = target;
       this.latch = latch;
   }

   
   public override void run() {
       try {
           subscriber.addOwnerPermission(target, store.Object);
       } catch (Exception ignored) {
       }
       latch.countDown();
   }
}
    }