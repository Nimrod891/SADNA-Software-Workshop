using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace externalService
{
    namespace ConsoleApp4.authentication
    {
        public class DeliveryData {

            private String username;
            private String address;

            public String getUsername() {
                return username;
            }

            public String getAddress() {
                return address;
            }

            public DeliveryData(String username, String address) {
                this.username = username;
                this.address = address;
            }
        }

    }
}