using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using externalService.ConsoleApp4.authentication;

namespace ConsoleApp4.authentication
{
    public interface DeliverySystem
    {
        bool deliver(DeliveryData data);
    }

}