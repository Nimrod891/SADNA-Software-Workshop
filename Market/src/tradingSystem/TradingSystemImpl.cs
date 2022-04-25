using System;

namespace tradingSystem
{
	public class TradingSystenImpl
	{
		TradingSystem tradingSys;

	public TradingSystenImpl(TradingSystem tradingSys)
	{
		this.tradingSystem  = tradingSys;
	}

	public string connect()
	{
		return this.tradingSystem.connect();
	}


		
    public void register(string userName, string password)
	{
		this.tradingSys.register(userName, password);
	}

    public void exit(string userid)
    {
        this.tradingSys.exit(userid);
    }
	public void login(string connectID, string userName, string pass)  
	{
		tradingSystem.login(connectionId, userName, pass);
	}
	}
}