using System;
using System.Collections.Generic;
using Xunit;
using tradingSystem;
using Userpack;

namespace ConsoleApplication1.Tests.tradingSystem
{
    
    public class TradingSystemTest
    {
        private UserAuthentication userAuthentication;
        private MarketSystem market;

        public void SetUp()
        {
            userAuthentication = new UserAuthentication();
            market = new MarketSystem(userAuthentication);
        }

        public void BreakDown()
        {
            userAuthentication = new UserAuthentication();
            market = new MarketSystem(userAuthentication);
        }
        [Fact]
        public void connect_GetConnectionID()
        {
            SetUp();
            var expected = market.connect();
            var actual = "1";
            Assert.Equal(actual,expected);
            BreakDown();
        }
        [Fact]
        public void register_RegisterAndOnSecondException()
        {
            SetUp();
            var username = "Alex";
            var password = "123";
            
            //register new account
            try { market.register(username, password); }
            catch(Exception e) { Assert.Fail("register didn't work properly"); }
            
            //register new account which already exists
            try { market.register(username, password);
                Assert.Fail("register find existing account"); }
            catch(Exception e) { }
            Assert.True(true, "passed");
            BreakDown();
        }
        [Fact]
        public void login_FindRegisteredAccAndLogin()
        {
            // register a user and check if login doesn't pop exceptions
            SetUp();
            var username = "Alex";
            var password = "123";
            var connections = market.getConnections();
            var visitor = market.getSubscriberByUserName(username);
            var connectionID = "";
            foreach(KeyValuePair<string, Vistor> entry in connections)
            {
                if (entry.Value == visitor)
                {
                    connectionID = entry.Key;
                }
            }
            try{ market.login( connectionID,  username, password);}
            catch(Exception e){Assert.Fail("login Unsuccesful");}
            Assert.True(true,"login Succesful");
            BreakDown();
        }
        [Fact]
        public void exit()
        {
            // delete user from connections when IS VISITOR exiting
            SetUp();
            
            var id = market.connect();
            Assert.True(market.getConnections().Keys.Contains(id));
            
            market.exit(id);
            Assert.False(market.getConnections().Keys.Contains(id));
            
            BreakDown();
        }
    }
}