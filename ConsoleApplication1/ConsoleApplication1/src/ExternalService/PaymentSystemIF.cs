using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace externalService
{

    public interface PaymentSystem
    {
        bool pay(PaymentData data);

        void payBack(PaymentData data);
    }
}