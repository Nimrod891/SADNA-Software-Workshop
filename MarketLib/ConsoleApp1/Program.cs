using MarketLib.src.ExternalService.Payment;
using MarketLib.src.ExternalService.Supply;
using MarketLib.src.MarketSystemNS;
using MarketLib.src.StoreNS;
using MarketLib.src.StorePermission;
using MarketLib.src.UserP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        private const string server_domain = "https://localhost:44330/api";
        public static string SendApi(string method_name, string Parameters)
        {
            string service = "Market";
            string URI = string.Format("{0}/{1}/{2}{3}", server_domain, service, method_name, Parameters);
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URI);
                request.Method = "POST";
                String test = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    test = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }
                return test;
            }
            catch
            {
                return null;
            }
        }
        static void Main(string[] args)
        {

            MarketSystem m = new MarketSystem();
            string con = m.connect();
            m.register(con, "nimrod", "password");
             Subscriber s = m.login(con, "nimrod", "password");
            Console.WriteLine(s.IsAdmin);
            int nimStoreID = m.openNewStore("nimrod","nimStore").Result;
            
            Console.ReadLine();
          

        }
    }
}
