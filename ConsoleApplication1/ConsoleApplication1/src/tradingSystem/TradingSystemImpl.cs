using System;

namespace tradingSystem
{
	public class MarketSystenImpl
	{
		MarketSystem marketSystem;

	public MarketSystenImpl(MarketSystem marketSys)
	{
		this.marketSystem  = marketSys;
	}

	public string connect()
	{
		return this.marketSystem.connect();
	}


		
    public void register(string userName, string password)
	{
		this.marketSystem.register(userName, password);
	}

    public void exit(string userid)
    {
        this.marketSystem.exit(userid);
    }
	public void login(string connectID, string userName, string pass)  
	{
		marketSystem.login(connectID, userName, pass);
	}
	}
}