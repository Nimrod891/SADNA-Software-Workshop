using MarketLib.src.MarketSystemNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Purchase
{
    [TestClass]
    public class UnitTest1
    {
        MarketSystem marketsystem = MarketSystem.Instance;


        public void startup()
        {
           string id1 = marketsystem.connect();
            string id2 = marketsystem.connect();
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
