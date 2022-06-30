using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.NotificationPackage
{
    //singleton
    public sealed class ListenerController 
    {
        private List<ServerListener> listeners = new List<ServerListener>();
        private static ListenerController instance = null;
        private static readonly object padlock = new object();

        ListenerController() { }

        public static ListenerController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ListenerController();
                    }
                    return instance;
                }
            }
        }

        public  void addListener(IServerObserver _server, string username)
        {
            ServerListener listener = new ServerListener(username, _server);
        }

        public  void removeListener(string username)
        {
            foreach (var listener in listeners)
            {
                if (listener.Username == username)
                {
                    listeners.Remove(listener);
                }
            }
        }
        public ServerListener findListener(string username)
        {
            foreach (var listener in listeners)
            {
                if (listener.Username == username)
                {
                    return listener;
                }
            }
            return null;
        }

    }
}
