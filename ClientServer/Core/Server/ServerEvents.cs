using Core.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server
{
    public partial class GameServer
    {
        public class NewAccountArgs : EventArgs
        {
            public NewAccountArgs(AccountInfo info)
            {

            }
        }
        public EventHandler<NewAccountArgs> NewAccountEvent { get; set; } = null;

        public void OnAccountLoggedEvent(NewAccountArgs args)
        {
            if (NewAccountEvent != null) 
            {
                NewAccountEvent?.Invoke(this, args);
            }

        }
    }
}
