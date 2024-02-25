using Core.Account;
using Core.DB;
using Core.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server
{
    public partial class GameServer
    {
        public AccountDB? AccountInterface { get; private set; } = null;

        public GameServer()
        {
            AccountInterface = new AccountDB();

            /* Qui, vanno fatte un miliardo di cose */
        }

        public void AddAccountHashToDB(AccountInfo account)
        {
            AccountInterface?.Update(account);
        }
    }
}
