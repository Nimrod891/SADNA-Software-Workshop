using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.NotificationPackage
{
    public interface IServerObserver
    {
        void update(List<NotificationStrat> notification);
    }
}
