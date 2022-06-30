using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace client.CostumClasses
{
    public class SendRequest
    {
        private const string server_domain = "https://localhost:44330/api";
        public bool isError= false;


        public  string SendApi(string method_name, string Parameters)
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
                this.isError = false;
                return test;
            }
            catch(WebException msg)
            {
                this.isError = true;
                return msg.Message;

            }
        }
    }
}