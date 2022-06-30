using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.NotificationPackage
{
    public class ServerListener
    {

        private string username;

        private IServerObserver _server; //this is the websocket related to the username.

        public ServerListener(string username, IServerObserver server)
        {
            this.username = username;
            this._server = server;
        }

        public string Username { get => username; set => username = value; }
        public IServerObserver Server { get => _server; set => _server = value; }
    }
}
