using Core.Account;
using Core.DB;
using Core.DB.Account;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            string path1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(path1, "Config");
            LoadConfiguration loadConfiguration = new LoadConfiguration(path);

            SerialManager = new SerialManager(ServerLogger, loadConfiguration?.SerialConfig);
            AccountInterface = new AccountDB(ServerLogger, SerialManager, loadConfiguration?.DBConfig);
            Auth = new Authentication(AccountInterface);
            /* Qui, vanno fatte un miliardo di cose */
        }

        public void Save()
        {
            Stopwatch st = new Stopwatch();

            st.Start();
            SerialManager?.Save();
            AccountInterface?.Save();

            st.Stop();
            ServerLogger?.LogWarning($"Save done in {st.ElapsedMilliseconds}ms");
        }
    }
}
