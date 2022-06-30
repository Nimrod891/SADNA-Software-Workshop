using MarketLib.src.NotificationPackage;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public class NotificationHub : Hub, IServerObserver
    {
        public void update()
        {
            throw new NotImplementedException();
        }

        public void update(List<NotificationStrat> notification)
        {
            throw new NotImplementedException();
        }
    }
}
