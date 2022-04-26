using System;
using System.Collections.Generic;
using Userpack;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Threading;

namespace tradingSystem
{
	
	public class TradingSystem
	{	
		private ConcurrentDictionary<int, User> connections;

		private ConcurrentDictionary<string, Subscriber> subscriber;

		private int subscribercounter;

		private int storeIdCounter =0; //Todo make thread safe fields.
		
		private int counterId=0; // maybe we will find a different way to make an id.

		private UserAuthentication auth;
		private int counterId=0; //
		public TradingSystem(UserAuthentication auth)
		{
			this.auth = auth;
			connections = new Dictionary<int, User>();
		}

		public string connect()
		 {
       //TODO: probebly change the way we get the id.
	  	 	int connectionId = Interlocked.Increment(ref id);
			connections.Add(connectionId, new User());
        	return connectionId;
    	}

		public void register( string username, string password)
		{
			auth.register(userName, password);//maybe need change
			subscriber.TryAdd(username , new Subscriber(Interlocked.Increment(ref storeIdCounter),username));
		}

		public void exit(string id)
		{
			User user =  connections[id];
			 //TODO : we will check the dynamic type if user 
			 // if it's a subscriber nothing will happen 
			 // if its a  visitor we will deltlete him from the list of connections.
		}
		 public void login(String connectionId, String userName, String password)
		 {
			 
       		User user = getUserByConnectionId(connectionId);
        	auth.authenticate(userName, password);
        	Subscriber subscriber = getSubscriberByUserName(userName);
       		subscriber.makeCart(user);
			connections.TryAdd(connectionId,subscriber);
        	//subscriber.set

		 }




	}
}
