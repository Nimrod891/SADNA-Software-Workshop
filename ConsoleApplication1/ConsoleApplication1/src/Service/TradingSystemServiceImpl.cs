using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System;
using tradingSystem;

namespace Service.TradingSystemServiceImpl{
//this class offers all the functionality that serve the system
public class TradingSystemServiceImpl
{
    private  TradingSystenImpl trading;
    public TradingSystemServiceImpl(TradingSystenImpl tradingServiceImpl)
    {
       // this.tradingSystemImpl = tradingSystemImpl;
    }


    //will happen when someone joins the thread, will initialize him as a user(visitor).
    public string connect()
    {
       // return tradingSystemImpl.connect();
       return "";
    }

    //will happen when the vistor presses the X button. will erase him from the system.
    //if its a member this function wont do nothing
    public void exit(string userid)
    {
        //logger
        //tradingSystemImpl.exit(userid);
    }



    public void register(string userName, string password)  {
        //tradingSystemImpl.register(userName, password);
    }
 
    public void login(string connectID, string userName, string pass)  {
       // eventLog.writeToLogger("Login with userName: " + userName + ", password: *********");
       // tradingSystemImpl.login(connectID, userName, pass);
    }

    public void logout(String connectID)  {
        //eventLog.writeToLogger("Logout subscriber");
        //tradingSystemImpl.logout(connectID);
    }
}
}