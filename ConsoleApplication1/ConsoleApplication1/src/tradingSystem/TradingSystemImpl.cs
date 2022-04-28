using System;

namespace tradingSystem
{
	public class TradingSystenImpl
	{
		TradingSystem tradingSystem;

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
		this.tradingSystem.register(userName, password);
	}

    public void exit(string userid)
    {
        this.tradingSystem.exit(userid);
    }
	public void login(string connectID, string userName, string pass)  
	{
		tradingSystem.login(connectID, userName, pass);
	}
	}
}