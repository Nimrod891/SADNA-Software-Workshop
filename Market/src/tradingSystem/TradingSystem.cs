using System;

namespace tradingSystem.TradingSystem
{
	
	public class TradingSystem
	{	

		private int storeIdCounter ; //Todo make thread safe fields.
		

		private int counterId=0; // maybe we will find a different way to make an id.


		private int counterId=0; //
		public TradingSystem()
		{
		}

		public string connect()
		 {
       //TODO: probebly change the way we get the id.
		string id = (this.counterId++).ToString();
        connections.put(connectionI, new User());
        return connectionId;
    	}
	}
}
