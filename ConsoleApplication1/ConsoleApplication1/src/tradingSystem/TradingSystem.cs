using System;
using System.Collections.Generic;
using Userpack;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Threading;
using ConsoleApp4.authentication;
using externalService;
using java.security.acl;
using java.util;
using StorePack;

namespace tradingSystem
{
	
	public class TradingSystem
	{	
		private ConcurrentDictionary<string, User> connections;

		private ConcurrentDictionary<string, Subscriber> subscribers;
		private ConcurrentDictionary<string, Store> stores; // key: store id
		private  ConcurrentDictionary<Store, ICollection<int>> storesPurchasePolicies; // key: store, value: purchase policies
		private  ConcurrentDictionary<Store, ICollection<int>> storesDiscountPolicies; // key: store, value: discount policies
		
		private DeliverySystem deliverySystem;
		private PaymentSystem paymentSystem;
		private UserAuthentication auth;
		private int subscribercounte =0;
		private int storeIdCounter =0; //Todo make thread safe fields
		private int counterId=0; // maybe we will find a different way to make an id.
		
		public User getUserByConnectionId(string connectionId)  {
			User user = connections[connectionId];
			if (user == null) throw new Exception(connectionId);
			return user;
		}
		
		public Subscriber getSubscriberByUserName(String userName) {
			Subscriber subscriber = subscribers[userName];
			if (subscriber == null) throw new Exception(userName);
			return subscriber;
		}
		
		
		public TradingSystem(UserAuthentication auth)
		{
			this.auth = auth;
			connections = new ConcurrentDictionary<string, User>();
		}

		public string connect()
		 {
       //TODO: probebly change the way we get the id.
	  	 	string connectionId = Interlocked.Increment(ref counterId).ToString();
	        
			connections.TryAdd(connectionId, new User());
        	return connectionId;
    	}

		public void register( string username, string password)
		{
			auth.register(username, password);//maybe need change
			//subscriber.TryAdd(username , new Subscriber(Interlocked.Increment(ref ),username));
		}

		public void exit(string id)
		{
			User user =  connections[id];
			 //TODO : we will check the dynamic type if user 
			 // if it's a subscriber nothing will happen 
			 // if its a  visitor we will deltlete him from the list of connections.
		}
		public void login(string connectionId, string userName, string password)
		 {
			 
       		User user = getUserByConnectionId(connectionId);
        	auth.authenticate(userName, password);
        	Subscriber subscriber = getSubscriberByUserName(userName);
       		subscriber.makeCart(user);
			connections.TryAdd(connectionId,subscriber);
        	//subscriber.set

		 }
		 public void logout(string connectionId) 
		 {
			 User guest = new User();
			 connections.TryAdd(connectionId, guest);
		 }
		 
		 public int newStore(Subscriber subscriber, string storeName)
		 {

			 int id = Interlocked.Increment(ref storeIdCounter);
			 

		 Store store = new Store(id, storeName, "description", null, null);

		 foreach (Store s in stores.Values)
		 {
			 if(storeName.Equals(store.getName()))
				 throw new Exception();
		 }
		 stores.TryAdd(id.ToString(), store);
		 subscriber.addOwnerPermission(store);
//        observables.put(store, new Observable());
		 //store.

		 return id;
	}
		 public ICollection<Subscriber> getStoreStaff(Subscriber subscriber, Store store, ICollection<Subscriber> staff)
		 {
			 ArrayList p = new ArrayList();
			 p.add(AdminPermission.getInstance());
			 p.add(ManagerPermission.getInstance(store));
			 AbsPermission managerPermission = ManagerPermission.getInstance(store);
			 foreach (Subscriber s in this.subscribers.Values)
			 {
				 if (s.havePermission(managerPermission))
					 staff.Add(s);
			 }
			 return staff;
		 }	
		 
		 public Store getStore(int storeId) 
		 {

			 Store store = stores[storeId.ToString()];
			if (store == null)
				throw new Exception(storeId.ToString());
			 return store;
		 }
		 
		//  public Collection getItems(string keyWord, string productName, string category, string subCategory,
		// 	 double ratingItem, double ratingStore, double maxPrice, double minPrice) {
		//
		// 	 ICollection<string> items = new LinkedList<string>();
		// 	 ICollection<Product> itemsToAdd;
		//
		// 	 foreach (Store store in  stores.Values)
		// 	 {
		// 		 itemsToAdd = store.searchAndFilter(keyWord, productName, category, ratingItem, ratingStore, maxPrice, minPrice);
		// 		 foreach (Product p in itemsToAdd)
		// 		 items.Add("store: " + store.getId() + ", " + p.ToString());
		// 	 }
		//
		//
		// 	 return items;
		// // }
		


	}
}
