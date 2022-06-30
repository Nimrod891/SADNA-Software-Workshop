using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace client.CostumClasses
{
    public class ServerFunctions
    {
       private static SendRequest request = new SendRequest();
        
        public static string connect()
        {
            
             return request.SendApi("connect", "");
            
        }

        public static string register(string connection,string username, string password)
        {
           return request.SendApi("register", $"/{connection}/{username}/{password}");
        }

        public static string login(string connection,string username, string password)
        {
            return request.SendApi("login", $"/{connection}/{username}/{password}");
        }

      /*  public static string showCart(string connection)
        {
            string ans = request.SendApi("showCart", $"/{connection}");
            if (request.isError)
                return ans;
            else
            {
                //
            }
        }*/
      /*
        public static string purchaseCart(string connection)
        {
            return SendRequest.SendApi("purchaseCart", $"/{connection}");
        }

        public static string storesInfo(string connection)
        {
            return  SendRequest.SendApi("storesInfo", $"/{connection}");
        }
      */



    }
}