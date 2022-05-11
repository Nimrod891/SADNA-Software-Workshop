using System;
using java.net;
using Service.TradingSystemServiceImpl;
using StorePack;
using Exception = java.lang.Exception;

namespace ConsoleApplication1.Tests.service;

public class MarketSystemServiceImplTest
{
    private MarketSystemServiceImpl m;
    private string username="someUser7654", pass="somePass1243";
    private int connectionId;
    
    void setUpTests()
    {
        m = new MarketSystemServiceImpl();
        connectionId = m.connect();
        m.register(username, pass);
    }
    
    bool loginTest() {
        string ans = m.login(connectionId, username, pass);
        return ans == username;
    }

    bool openNewStoreTest()
    {
        try
        {
            m.openNewStore(username, "someStore123");
        }
        catch (Exception ex)
        {
            Console.WriteLine("# TEST FAILED: "+ex.toString());
            return false;
        }

        return true;
    }

    bool showStaffInfoTest()
    {
        return null != m.showStaffInfo(username,"someStore123");
    }
    
    bool getStoreHistoryTest()
    {
        return null != m.getStoreHistory(username,"someStore123");
    }

    void endTests()
    {
        m.logout(username);
        m.exit(connectionId);
    }
}