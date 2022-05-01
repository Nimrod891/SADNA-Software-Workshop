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
	
	public class MarketSystem
	{	
		private ConcurrentDictionary<string, Vistor> connections;
		private ConcurrentDictionary<string, Subscriber> subscribers;
		private ConcurrentDictionary<string, Store> stores; // key: store id

		private DeliverySystem deliverySystem;
		private PaymentSystem paymentSystem;
		private int subscribercounte = 0;
		private int storeIdCounter =0; //Todo make thread safe fields
		private int counterId=0; // maybe we will find a different way to make an id.
		
		public Vistor getUserByConnectionId(string connectionId)  {
			Vistor vistor = connections[connectionId];
			if (vistor == null) throw new Exception(connectionId);
			return vistor;
		}

		public Subscriber getSubscriberByUserName(String userName) {
			Subscriber subscriber = subscribers[userName];
			if (subscriber == null) throw new Exception(userName);
			return subscriber;
		}
		
		
		public MarketSystem()
		{
			connections = new ConcurrentDictionary<string, Vistor>();
			subscribers = new ConcurrentDictionary<string, Subscriber>();
			stores = new ConcurrentDictionary<string, Store>();
			
		}

		public string connect()
		 {
       //TODO: probebly change the way we get the id.
	  	 	string connectionId = Interlocked.Increment(ref counterId).ToString();
			connections.TryAdd(connectionId, new Vistor());
        	return connectionId;
    	}

	

		


	}
}
