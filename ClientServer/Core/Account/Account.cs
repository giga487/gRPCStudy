using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Account
{
    public interface IDataInfo
    {
        string UserName { get; set; }
        string Hash { get; set; }
    }

    public class AccountInfo: IDataInfo
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public List<Core.Player.Player> Characters { get; private set; } = new List<Core.Player.Player>();
    }
}
