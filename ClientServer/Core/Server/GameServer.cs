using Core.Account;
using Core.DB;
using Core.DB.Account;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server
{
    public partial class GameServer
    {
        public SerialManager? SerialManager { get; }
        public AccountDB? AccountInterface { get; private set; } = null;
        public Authentication? Auth { get; private set; } = null;
        public ILogger? ServerLogger { get; private set; } = null;
        public GameServer()
        {

            AccountInterface = new AccountDB(ServerLogger);
            SerialManager = new SerialManager(ServerLogger);
            Auth = new Authentication(AccountInterface);
            /* Qui, vanno fatte un miliardo di cose */
        }
    }
}
