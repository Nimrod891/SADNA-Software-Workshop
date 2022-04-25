using System;

namespace tradingSystem
{
	public class TradingSystenImpl
	{
		TradingSystem tradingSys;

	public TradingSystenImpl(tradingSys)
	{
		this.tradingSystem  = tradingSys;
	}

	public string connect()
	{
		return this.tradingSystem.connect();
	}


		
    public void register(string userName, string password)
	{
		tradingSystem.register(userName, password);
	}

    public void exit(string userid)
    {
        this.tradingSys.exit(userid);
    }



	}
}