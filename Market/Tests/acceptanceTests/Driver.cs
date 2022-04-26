

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using acceptanceTests.ServiceProxy;
namespace acceptanceTests{
public class Driver {

        private static PaymentSystem paymentSystem = new PaymentSystemMock();
        private static DeliverySystem deliverySystem = new DeliverySystemMock();

        public static PaymentSystem getPaymentSystem() {
            return paymentSystem;
        }

        public static DeliverySystem getDeliverySystem() {
            return deliverySystem;
        }

        public static TradingSystemService getService(String userName, String password) throws InvalidActionException {
            ServiceProxy proxy = new ServiceProxy();
            UserAuthentication auth = new UserAuthentication();
            auth.register(userName, password);
            ConcurrentHashMap<String, Subscriber> subscribers = new ConcurrentHashMap<>();
            AtomicInteger subscriberIdCounter = new AtomicInteger();
            Subscriber admin = new Subscriber(subscriberIdCounter.getAndIncrement(), userName);
            admin.addPermission(AdminPermission.getInstance());
            subscribers.put(userName, admin);
            TradingSystem build = new TradingSystemBuilder().setUserName(userName).setPassword(password)
                    .setSubscriberIdCounter(subscriberIdCounter).setSubscribers(subscribers).setAuth(auth).setPaymentSystem(paymentSystem).setDeliverySystem(deliverySystem).build();
            TradingSystemImpl trade = new TradingSystemImpl(build);
            TradingSystemServiceImpl real = new TradingSystemServiceImpl(trade);
            proxy.setReal(real);
            return proxy;
        }

    }
}