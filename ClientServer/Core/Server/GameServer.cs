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
    public class SerialManager(ILogger? logger)
    {
        private ConcurrentDictionary<ulong, ISerializable> World { get; } = new ConcurrentDictionary<ulong, ISerializable>();
        public ulong LastID { get; private set; } = ulong.MaxValue;
        public void AddSerializable(ISerializable serializable)
        {
            if (!World.TryAdd(serializable.Serial.ID, serializable))
            {
                logger?.LogCritical($"{serializable.Serial.ID} is trying to add at world list");
                LastID = serializable.Serial.ID; //ONLY HERE
            }
            else
                World[serializable.Serial.ID] = serializable;


        }

        public ISerializable? GetSerializable(ulong serialID)
        {
            if (World.TryGetValue(serialID, out ISerializable? serializable))
            {
                return serializable;
            }
            else
            {
                return null;
            }
        }
    }

    public partial class GameServer
    {
        public SerialManager? SerialManager { get; }
        public AccountDB? AccountInterface { get; private set; } = null;
        public ILogger? ServerLogger { get; private set; } = null;
        public GameServer()
        {

            AccountInterface = new AccountDB(ServerLogger);
            SerialManager = new SerialManager(ServerLogger);
            /* Qui, vanno fatte un miliardo di cose */
        }

        public void AddAccountHashToDB(AccountInfo account)
        {
            AccountInterface?.Update(account);
        }
    }
}
