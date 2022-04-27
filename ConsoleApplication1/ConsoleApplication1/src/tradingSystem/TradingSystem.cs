using System;
using System.Collections.Generic;
using Userpack;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Threading;
using java.security.acl;
using java.util;
using StorePack;

namespace tradingSystem
{
	
	public class TradingSystem
	{	
		private ConcurrentDictionary<string, User> connections;

		private ConcurrentDictionary<string, Subscriber> subscriber;
		private ConcurrentDictionary<string, Store> stores; // key: store id

		private int subscribercounte =0;
		private int storeIdCounter =0; //Todo make thread safe fields
		private int counterId=0; // maybe we will find a different way to make an id.

		private UserAuthentication auth;
		public TradingSystem(UserAuthentication auth)
		{
			this.auth = auth;
			connections = new Dictionary<int, User>();
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
			 

		 Store store = new Store(id, storeName, "description", null, null, new Observable());

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
			 foreach (Subscriber s in this.subscriber.Values)
			 {
				 if (s.havePermission(managerPermission))
					 staff.Add(s);
			 }
			 return staff;
	}




	}
}
