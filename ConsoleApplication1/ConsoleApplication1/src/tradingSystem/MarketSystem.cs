using System;
using System.Collections.Generic;
using Userpack;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Threading;
using ConsoleApp4.authentication;
using ConsoleApplication1.Security;
using externalService;
using java.security.acl;
using java.util;
using StorePack;

namespace tradingSystem
{
	
	public class MarketSystem
	{	
		private ConcurrentDictionary<int, Vistor> connections;
		private ConcurrentDictionary<string, Subscriber> members;
		private ConcurrentDictionary<string, Store> stores; // key: store id

		private DeliverySystem deliverySystem;
		private PaymentSystem paymentSystem;
		private UserSecurity usersecure;
		private int storeIdCounter =0; //Todo make thread safe fields
		private int counterId=0; // maybe we will find a different way to make an id.
		


		public Subscriber getSubscriberByUserName(String userName) {
			Subscriber subscriber = members[userName];
			if (subscriber == null) throw new Exception(userName);
			return subscriber;
		}
		
		
		public MarketSystem()
		{
			connections = new ConcurrentDictionary<int, Vistor>();
			members = new ConcurrentDictionary<string, Subscriber>();
			stores = new ConcurrentDictionary<string, Store>();
			
		}

		
		public int connect()
		{
			int connectionId = Interlocked.Increment(ref counterId);
			connections.TryAdd(connectionId, new Vistor());
        	return connectionId;
    	}

		public void exit(int connectid)
		{
			Vistor v;
			connections.TryRemove(connectid,out v);
		}

		public void register(int connectionid,string username, string password)
		{
			if (connections.ContainsKey(connectionid)) //if you are a vistor
			{
				if (members.ContainsKey(username))//if this username already exists.
					throw new Exception("this username already exists");
				Subscriber member = new Subscriber(connectionid,username);
				members[username] = member;
				usersecure.storeUser(username,password);
			}
		}

		//when a user logs in we return his username to use as an identifier
		//when we will have a data base this will maybe look al different.
		public string login(int connection, string username, string password)
		{
			if (connections[connection] != null)
			{
				usersecure.verifyUser(username, password); //verify user info 
				connections[connection] = members[username]; //mabe will be in use, if its not just ignore.
				return username;
			}
			throw new Exception(); 
		}
		
		//the logouy the user from the system and will retun hum as a new vistor
		//in this function we infer that the username value in the presentation/service layer is
		//back to null.
		public void logout(int connectionId) {
			Vistor guest = new Vistor();
			connections[connectionId]= guest;
		}
		

	

		


	}
}
