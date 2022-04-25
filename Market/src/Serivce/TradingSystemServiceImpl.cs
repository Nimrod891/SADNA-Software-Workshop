using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System;
using tradingSystem;

namespace Service.TradingSystemServiceImpl{
//this class offers all the functionality that serve the system
public class TradingSystemServiceImpl
{
    private TradingSystemImpl tradingSystemImpl;
    public TradingSystemServiceImpl(tradingServiceImpl)
    {
        this.tradingSystemImpl = tradingSystemImpl;
    }

    public string connect()
    {
        return tradingSystemImpl.connect();
    }

    
}
}